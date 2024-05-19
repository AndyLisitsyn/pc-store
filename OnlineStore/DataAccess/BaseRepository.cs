using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using NonDomain;

namespace DataAccess
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public Task<List<TEntity>> GetListAsync()
        {
            return _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<OperationResult> InsertAsync(TEntity entityToInsert, bool requireSave)
        {
            var operationStatus = new OperationResult
            {
                IsSuccess = true
            };

            try
            {
                _dbSet.Add(entityToInsert);
            }
            catch (Exception ex)
            {
                operationStatus = OperationResult.CreateFromException("Error inserting " + typeof(TEntity) + ".", ex);
            }

            if (operationStatus.IsSuccess && requireSave)
            {
                operationStatus = await SaveAsync();
            }

            return operationStatus;
        }

        public async Task<OperationResult> DeleteAsync(TEntity entityToDelete, bool requireSave)
        {
            var operationStatus = new OperationResult
            {
                IsSuccess = true
            };

            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }
            catch (Exception ex)
            {
                operationStatus = OperationResult.CreateFromException("Error deleting " + typeof(TEntity) + ".", ex);
            }

            if (operationStatus.IsSuccess && requireSave)
            {
                operationStatus = await SaveAsync();
            }

            return operationStatus;
        }

        public async Task<OperationResult> UpdateAsync(TEntity entityToUpdate, bool requireSave)
        {
            var operationStatus = new OperationResult
            {
                IsSuccess = true
            };

            try
            {
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                operationStatus = OperationResult.CreateFromException("Error updating " + typeof(TEntity) + ".", ex);
            }

            if (operationStatus.IsSuccess && requireSave)
            {
                operationStatus = await SaveAsync();
            }

            return operationStatus;
        }

        public async Task<OperationResult> SaveAsync()
        {
            var changedEntities = _context.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added ||
                                e.State == EntityState.Modified);
            var operationStatus = new OperationResult 
            { 
                IsSuccess = true 
            };

            try
            {
                foreach (var e in changedEntities)
                {
                    var vc = new ValidationContext(e.Entity, null, null);
                    Validator.ValidateObject(e.Entity, vc, true);
                }
                var res = await _context.SaveChangesAsync();
                operationStatus.RecordsAffected = res;
                operationStatus.IsSuccess = operationStatus.RecordsAffected > 0;
            }
            catch (ValidationException ex)
            {
                operationStatus = OperationResult.CreateFromException("Entity validation failed.", ex);
            }
            catch (Exception ex)
            {
                operationStatus = OperationResult.CreateFromException("Save operation failed.", ex);
            }

            return operationStatus;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
