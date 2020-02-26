using BIVALE.BLL.Interfaces;
using BIVALE.DAL.Models;
using System;
using System.Collections;
using System.Configuration;

namespace BIVALE.BLL.Generic
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private BILAVEEntities context;

        private Hashtable repositories = new Hashtable();
        public UnitOfWork()
        {
			//context = DependencyInjector.Retrieve<BILAVEEntities>(ConfigurationManager.ConnectionStrings["BILAVEEntities"].ConnectionString);
			context = new BILAVEEntities(ConfigurationManager.ConnectionStrings["BILAVEEntities"].ConnectionString);
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
