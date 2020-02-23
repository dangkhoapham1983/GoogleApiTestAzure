using Autofac;
using AzureFunctions.Autofac.Configuration;
using BusinessLogicLayer.Interfaces;
using BIVALEApiFunctions.Interfaces;
using BIVALEApiFunctions.Models;
using BusinessLogicLayer.Services;

namespace BIVALEApiFunctions.Configs
{
    public class AutofacConfig
    {
        public AutofacConfig()
        {
            DependencyInjection.Initialize(builder =>
            {
				builder.RegisterType<UserServices>().As<IUserServices>();
				builder.RegisterType<Dog>().As<IAnimal>();
			});
        }
    }
}
