using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using System;
using System.Collections;
using System.Configuration;

namespace BusinessLogicLayer.Generic
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private GoogleApiEntities context;

        private Hashtable repositories = new Hashtable();
        public UnitOfWork()
        {
			//context = DependencyInjector.Retrieve<GoogleApiEntities>(ConfigurationManager.ConnectionStrings["GoogleApiEntities"].ConnectionString);
			context = new GoogleApiEntities(ConfigurationManager.ConnectionStrings["GoogleApiEntities"].ConnectionString);
		}
        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!repositories.Contains(typeof(T)))
            {
                repositories.Add(typeof(T), new Repository<T>(context));
            }
            return (IRepository<T>)repositories[typeof(T)];
        }      

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
