﻿using DataAccessLayer.Helpers;
using DataAccessLayer.Interfaces;
using System;
using System.Collections;

namespace DataAccessLayer.Models
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private GoogleApiTestEntities context;

        private Hashtable repositories = new Hashtable();
        public UnitOfWork()
        {
            context = new GoogleApiTestEntities();
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
