using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MinimalWordReminderApi.Repositories
{
	public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
	{
		private readonly MinimalDbContext context;
		private readonly DbSet<TEntity> dbSet;

		public Repository(MinimalDbContext context)
		{
			this.context = context;
			dbSet = context.Set<TEntity>();
		}
		public void Delete(TEntity entity)
		{
			if (context.Entry(entity).State == EntityState.Detached)
			{
				dbSet.Attach(entity);
			}
			dbSet.Remove(entity);
		}

		public void Delete(TKey id)
		{
			TEntity entity = dbSet.Find(id);
			Delete(entity);
		}

		public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
		{
			if (filter != null)
			{
				return dbSet.Where(filter);
			}
			return dbSet;
		}

		public bool IsExist(Expression<Func<TEntity, bool>> filter)
		{
			return dbSet.Any(filter);
		}

		public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter)
		{
			return await dbSet.AnyAsync(filter);
		}

		public TEntity GetById(TKey id)
		{
			return dbSet.Find(id);
		}

		public void Insert(TEntity entity)
		{
			dbSet.Add(entity);
		}

		public void Update(TEntity entity)
		{
			dbSet.Attach(entity);
			context.Entry(entity).State = EntityState.Modified;
		}

		public async Task<TEntity> GetByIdAsync(TKey id)
		{
			return await dbSet.FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
		{
			if (filter != null)
			{
				return await dbSet.Where(filter).ToListAsync();
			}
			return dbSet;
		}

		public async Task InsertAsync(TEntity entity)
		{
			await dbSet.AddAsync(entity);
		}
	}
}
