using Autofac;
using AzureFunctions.Autofac.Configuration;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using UnitOfWorkExample.Interfaces;
using UnitOfWorkExample.Models;

namespace UnitOfWorkExample.Configs
{
    public class AutofacConfig
    {
        public AutofacConfig()
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
				builder.RegisterType<Dog>().As<IAnimal>();
			});
        }
    }
}
