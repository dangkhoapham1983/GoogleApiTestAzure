using Autofac;
using AzureFunctions.Autofac.Configuration;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using BIVALEApiFunctions.Interfaces;
using BIVALEApiFunctions.Models;

namespace BIVALEApiFunctions.Configs
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
