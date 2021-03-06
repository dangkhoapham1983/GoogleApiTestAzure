﻿using BIVALE.BLL.Interfaces;
using BIVALE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BIVALE.BLL.Generic
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal BILAVEEntities context;
        internal DbSet<TEntity> dbSet;

        public Repository(BILAVEEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            IQueryable<TEntity> query = dbSet;
            return query;
        }
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(params object[] id)
        {
            return dbSet.Find(id);
        }
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertAll(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void DeleteAll(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Upsert(TEntity entityToUpsert)
        {
            dbSet.AddOrUpdate(entityToUpsert);
        }

		public Task<TEntity> GetById(int id) => context.Set<TEntity>().FindAsync(id);

		public Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
			=> dbSet.FirstOrDefaultAsync(predicate);

		public async Task Add(TEntity entity)
		{
			context.Set<TEntity>().Add(entity);
			await context.SaveChangesAsync();
		}

		public Task UpdateEntity(TEntity entity)
		{
			context.Entry(entity).State = EntityState.Modified;
			return context.SaveChangesAsync();
		}

		public Task Remove(TEntity entity)
		{
			context.Set<TEntity>().Remove(entity);
			return context.SaveChangesAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAll()
		{
			return await context.Set<TEntity>().ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
		{
			return await context.Set<TEntity>().Where(predicate).ToListAsync();
		}

		public Task<int> CountAll() => context.Set<TEntity>().CountAsync();

		public Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate) => context.Set<TEntity>().CountAsync(predicate);
	}
}
