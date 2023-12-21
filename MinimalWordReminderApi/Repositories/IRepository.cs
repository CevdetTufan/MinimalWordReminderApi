using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace MinimalWordReminderApi.Repositories
{
	public interface IRepository<TEntity, TKey> where TEntity : class
	{
		bool IsExist(Expression<Func<TEntity, bool>> filter);
		Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);
		TEntity GetById(TKey id);
		Task<TEntity> GetByIdAsync(TKey id);
		IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
		Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
		TEntity Insert(TEntity entity);
		Task InsertAsync(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		void Delete(TKey id);

		int SaveChanges();
		Task<int> SaveChangesAsync();
	}
}
