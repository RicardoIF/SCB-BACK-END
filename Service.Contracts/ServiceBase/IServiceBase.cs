using Entities.Models.Base;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query;
using Shared.DataTransferObjects.Base;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.ServiceBase
{
    public interface IServiceBase<T, TViewModel, CreateModel, UpdateModel> where T : class, IBaseModel, new() where TViewModel : class, IDtoBase, new()
    {
        Task<IEnumerable<ExpandoObject>> GetAllAsync<RequestParam>(RequestParam parameters, bool trackChanges, params Expression<Func<T, object>>[]? includeProperties)
            where RequestParam : RequestParameters, new();
        Task<(IEnumerable<ExpandoObject> models, MetaData metaData)> GetPagedListAsync<RequestParam>(
            RequestParam parameters, bool trackChanges, params Expression<Func<T, object>>[]? includeProperties)
            where RequestParam : RequestParameters, new();
        IQueryable<T> FilterData<RequestParam>(RequestParam parameters, bool trackChanges, params Expression<Func<T, object>>[]? includeProperties)
            where RequestParam : RequestParameters, new();
        Task<TViewModel> GetByIdAsync(int id, bool trackChanges, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<TViewModel> AddAsync(CreateModel model);
        Task UpdateAsync(int id, UpdateModel model, bool trackChanges);
        Task PartialUpdate(int id, JsonPatchDocument<TViewModel> model, bool trackChanges);
        Task DeleteAsync(int id, bool trackChanges);
    }
}
