using Autofac;
using AzureFunctions.Autofac.Configuration;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using BIVALEApiFunctions.Interfaces;
using BIVALEApiFunctions.Models;
using BusinessLogicLayer.Generic;
using BusinessLogicLayer.Services;

namespace BIVALEApiFunctions.Configs
{
    public class AutofacConfig
    {
        public AutofacConfig()
        {
            DependencyInjection.Initialize(builder =>
            {
                //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
				builder.RegisterType<UserServices>().As<IUserServices>();
				builder.RegisterType<Dog>().As<IAnimal>();
			});
        }
    }
}
