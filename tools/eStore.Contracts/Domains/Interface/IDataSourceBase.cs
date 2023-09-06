﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Contracts.Domains.Interface
{
    public interface IDataSourceBase<T, K> where T : EntityBase<K>
    {
        IQueryable<T> GetAll(bool isAsNoTracking = false, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IQueryable<T> ExecuteSqlQuery(string query);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task InsertAsync(T entity);
        Task InsertIfNotExistAsync(Expression<Func<T, Guid>> identifierExpression, List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAsync(K id);
        Task<T> FindByIdAsync(K id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task UpdateAsync(T entity);
        Task UpdateArrange(IEnumerable<T> entities);
        Task Add(T entity);
        Task AddAsync(T entity);
        Task Save();
        T FindById(K id, params Expression<Func<T, object>>[] includeProperties);
        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Update(T entity, params string[] propertiesToExclude);
    }
    public interface IDataSourceBase<T, K, TContext> : IDataSourceBase<T, K>
    where T : EntityBase<K>
    where TContext : DbContext
    {
    }
}
