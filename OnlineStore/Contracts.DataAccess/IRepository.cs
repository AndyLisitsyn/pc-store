using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NonDomain;

namespace Contracts.DataAccess
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<List<TEntity>> GetListAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task<OperationResult> InsertAsync(TEntity entityToInsert, bool requireSave);
        Task<OperationResult> DeleteAsync(TEntity entityToDelete, bool requireSave);
        Task<OperationResult> UpdateAsync(TEntity entityToUpdate, bool requireSave);
        Task<OperationResult> SaveAsync();
    }
}
