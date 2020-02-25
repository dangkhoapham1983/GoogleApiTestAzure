using System;

namespace BIVALE.BLL.Interfaces
{
	public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Save();
		new void Dispose();
    }
}
