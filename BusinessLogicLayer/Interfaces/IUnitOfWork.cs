using System;

namespace BusinessLogicLayer.Interfaces
{
	public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Save();
		new void Dispose();
    }
}
