using Core.ActionFilters;
using Entities.Exceptions;
using Entities.Models.Base;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.ServiceBase;
using Shared.DataTransferObjects.Base;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Presentation.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public abstract class BaseController<TEntity, TDto, TCreateDto, TUpdateDto, TParameter> : ControllerBase
        where TEntity : class, IBaseModel, new()
        where TDto : class, IDtoBase, new()
        where TParameter : RequestParameters, new()
    {
        protected readonly IServiceBase<TEntity, TDto, TCreateDto, TUpdateDto> _service;
        protected BaseController(IServiceBase<TEntity, TDto, TCreateDto, TUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] TParameter parameters)
        {
            try
            {
                (IEnumerable<ExpandoObject> models, MetaData metaData) list_paginate = new() { models = new List<ExpandoObject>(), metaData = new MetaData()};
                IEnumerable<ExpandoObject> list = new List<ExpandoObject>();

                if (parameters.PageNumber is > 0 && parameters.PageSize is > 0)
                {
                    list_paginate = await _service.GetPagedListAsync(parameters, trackChanges: false);
                    var metadata = new MetaData
                    {
                        TotalCount = list_paginate.metaData.TotalCount,
                        PageSize = list_paginate.metaData.PageSize,
                        CurrentPage = list_paginate.metaData.CurrentPage,
                        TotalPages = list_paginate.metaData.TotalPages
                    };
                    var apiResponse = new ApiResponse<IEnumerable<ExpandoObject>>(list_paginate.models)
                    {
                        Meta = metadata,
                        Message = "",
                        StatusCode = HttpStatusCode.OK,
                        Success = true
                    };
                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(list_paginate.metaData));
                    return Ok(apiResponse);
                }
                else
                {
                    list = await _service.GetAllAsync(parameters, trackChanges: false);
                    var apiResponse = new ApiResponse<IEnumerable<ExpandoObject>>(list)
                     {
                        Meta = new MetaData(),
                        Message = "",
                        StatusCode = HttpStatusCode.OK,
                        Success = true
                    };
                    return Ok(apiResponse);
                }
            }
            catch (Exception)
            {
                return BadRequest(Respuesta("Error: No se pudieron recuperar los registros", false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("{id}")]
        [ActionName("GetEntityById")]
        public virtual async Task<IActionResult> GetEntityById(int id)
        {
            try
            {
                var model = await _service.GetByIdAsync(id, trackChanges: false);
                return Ok(new ApiResponse<TDto>
                {
                    Data = model,
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                });
            }
            catch(Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return BadRequest(Respuesta($"Error: No se encontro el registro", false, HttpStatusCode.BadRequest, ex));
                }
                return BadRequest(Respuesta("Error: Ha ocurrido un error recuperando el registro", false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public virtual async Task<IActionResult> Create([FromBody] TCreateDto model)
        {
            if (model == null)
                return BadRequest(Respuesta("Error: No puede tratar de añadir un registro vacio", false, HttpStatusCode.BadRequest));

            try
            {
                var modelCreate = await _service.AddAsync(model);
                var result = new ApiResponse<TDto>()
                {
                    Data = modelCreate,
                    Success = true,
                    Message = "Registro Agregado de forma exitosa!!",
                    StatusCode = HttpStatusCode.Created
                };
                return CreatedAtAction("GetEntityById", new { id = modelCreate.Id }, result);
            }
            catch(Exception)
            {
                return BadRequest(Respuesta("Error: Ha ocurrido un error agregando el nuevo registro", false, HttpStatusCode.InternalServerError));
            }

        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public virtual async Task<IActionResult> Update(int id, [FromBody] TUpdateDto model)
        {
            if (model == null)
                return BadRequest(Respuesta("Error: No puede tratar de añadir un registro vacio", false, HttpStatusCode.BadRequest));
        
            try
            {
                await _service.UpdateAsync(id, model, trackChanges: true);
                var result = new ApiResponse<TDto>()
                {
                    Data = null,
                    Message = "Registro Actualizado de forma exitosa!!",
                    StatusCode = HttpStatusCode.OK,
                    Success = true
                };
                return Ok(result);
            }
            catch(Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return BadRequest(Respuesta($"Error: No se encontro el registro", false, HttpStatusCode.BadRequest, ex));
                }

                return BadRequest(Respuesta($"Error: Ha ocurrido un error actualizando el registro", false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPatch("update/{id}")]
        public virtual async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<TDto> jsonPatch)
        {
            if (jsonPatch == null)
                return BadRequest(Respuesta("Error: El cuerpo o json no puede ser nulo", false, HttpStatusCode.BadRequest));

            try
            {
                await _service.PartialUpdate(id, jsonPatch, false);
                var result = new ApiResponse<TDto>()
                {
                    Data = null,
                    Message = "Registro Actualizado de forma exitosa!!",
                    StatusCode = HttpStatusCode.OK,
                    Success = true
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return BadRequest(Respuesta($"Error: No se encontro el registro", false, HttpStatusCode.BadRequest, ex));
                }

                return BadRequest(Respuesta($"Error: Ha ocurrido un error actualizando el registro", false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest(Respuesta("ERROR!: El id del registro a modificar no puede estar nulo ni ser 0", true, HttpStatusCode.BadRequest));
            try
            {
                await _service.DeleteAsync(id, trackChanges: false);
                var result = new ApiResponse<TDto>()
                {
                    Data = null,
                    Message = "Registro eliminado de forma exitosa!!",
                    StatusCode = HttpStatusCode.OK,
                    Success = true
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return BadRequest(Respuesta($"Error: No se encontro el registro", false, HttpStatusCode.BadRequest, ex));
                }

                return BadRequest(Respuesta($"Error: Ha ocurrido un error eliminando el registro", false, HttpStatusCode.InternalServerError));
            }
        }
        protected OperationResult Respuesta(string mensaje, bool Success, HttpStatusCode statusRequestCode, Exception ex = null)
        {
            return new ApiResponse<TDto>()
            {
                StatusCode = statusRequestCode,
                Message = mensaje + (
            (ex != null) ?
            ("\n Mensaje de respuesta - - -> \n" + ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message))
            : ""),
                Success = Success
            };

        }

    }
}
