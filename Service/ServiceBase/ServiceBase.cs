using AutoMapper;
using Contracts.Interfaces;
using Entities.Models.Base;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects.Base;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Entities.Exceptions;
using Service.Contracts.ServiceBase;
using Contracts.Interfaces.RepositoryBase;
using Microsoft.EntityFrameworkCore.Query;
using Contracts.Interfaces.DataShaper;
using Microsoft.AspNetCore.JsonPatch;

namespace Service.ServiceBase
{
    public class ServiceBase<TModel, TViewModel, CreateModel, UpdateModel>
        : IServiceBase<TModel, TViewModel, CreateModel, UpdateModel>
        where TModel : class, IBaseModel, new() where TViewModel : class, IDtoBase, new()
    {
        private readonly IRepositoryBase<TModel> _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<TViewModel> _dataShaper;

        public ServiceBase(IRepositoryBase<TModel> repository, ILoggerManager logger,
            IMapper mapper, IDataShaper<TViewModel> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        public virtual async Task<IEnumerable<ExpandoObject>> GetAllAsync<RequestParam>(RequestParam parameters, bool trackChanges, 
            params Expression<Func<TModel, object>>[]? includeProperties) where RequestParam : RequestParameters, new()
        {
            IEnumerable<TModel> models = await _repository.FindAll(trackChanges, includeProperties)
                .Where(g => g.Estatus.Equals(true)).ToListAsync();
            var modelsDto = _mapper.Map<IEnumerable<TViewModel>>(models);
            var shapedData = _dataShaper.ShapeData(modelsDto, parameters.Fields);
            return shapedData;
        }

        public virtual async Task<(IEnumerable<ExpandoObject> models, MetaData metaData)> GetPagedListAsync<RequestParam>(
            RequestParam parameters, bool trackChanges, params Expression<Func<TModel, object>>[]? includeProperties)
            where RequestParam : RequestParameters, new()
        {
            var filterModels = FilterData(parameters, trackChanges, includeProperties);
            var pagedList = PagedList<TModel>.ToPagedList(filterModels, parameters.PageNumber, parameters.PageSize);
            var modelsDto = _mapper.Map<IEnumerable<TViewModel>>(pagedList);
            var shapedData = _dataShaper.ShapeData(modelsDto, parameters.Fields);
            return (models: shapedData, metaData: pagedList.MetaData);
        }

        public virtual async Task<TViewModel> GetByIdAsync(int id, bool trackChanges, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null)
        {
            var model = await ModelExist(id, trackChanges, include);
            var modelDto = _mapper.Map<TViewModel>(model);
            return modelDto;
        }

        public async Task<TViewModel> AddAsync(CreateModel model)
        {
            var entity = _mapper.Map<TModel>(model);
            _repository.Create(entity);
            await _repository.SaveAsync();

            var modelToReturn = _mapper.Map<TViewModel>(entity);
            return modelToReturn;
        }

        public async Task UpdateAsync(int id, UpdateModel model, bool trackChanges)
        {
            var entity = await ModelExist(id, trackChanges);
            _mapper.Map(model, entity);
            await _repository.SaveAsync();
        }

        public async Task PartialUpdate(int id, JsonPatchDocument<TViewModel> model, bool trackChanges)
        {
            var entity = _mapper.Map<TViewModel>(await ModelExist(id, trackChanges));
            model.ApplyTo(entity);
            var modelToUpdate = _mapper.Map<TModel>(entity);
            _repository.Update(modelToUpdate);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id, bool trackChanges)
        {
            var entity = await ModelExist(id, trackChanges);
            _repository.Delete(entity);
            await _repository.SaveAsync();
        }

        private async Task<TModel> ModelExist(int id, bool trackChanges, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null)
        {
            var model = await _repository.FindByCondition(g => g.Id.Equals(id), trackChanges, null, include).SingleOrDefaultAsync();
            if (model is null)
                throw new IdNotFoundException(id);

            return model;
        }

        private static bool HasMethod(IQueryable<TModel> objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }

        public IQueryable<TModel> FilterData<RequestParam>(RequestParam parameters, bool trackChanges, params Expression<Func<TModel, object>>[]? includeProperties)
            where RequestParam : RequestParameters, new()
        {
            IQueryable<TModel> listMetadata = _repository.FindAll(trackChanges, includeProperties)
                .Where(g => g.Estatus.Equals(true));
            if (HasMethod(listMetadata, "Search") && parameters.SearchTerm != null)
            {
                var method = listMetadata.GetType().GetMethod("Search");
                listMetadata = (IQueryable<TModel>)method.Invoke(listMetadata, new object[] { parameters.SearchTerm });
            }

            if (HasMethod(listMetadata, "Sort") && parameters.OrderBy != null)
            {
                var method = listMetadata.GetType().GetMethod("Sort");
                listMetadata = (IQueryable<TModel>)method.Invoke(listMetadata, new object[] { parameters.OrderBy });
            }

            return listMetadata;
        }

    }
}
