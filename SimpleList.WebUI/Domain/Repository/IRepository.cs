using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleList.WebUI.Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        /*
        void Add(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        */


        // Below are asynchronous methods
      
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
