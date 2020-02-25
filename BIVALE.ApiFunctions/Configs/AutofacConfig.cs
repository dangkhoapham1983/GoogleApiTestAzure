using Autofac;
using AzureFunctions.Autofac.Configuration;
using BIVALE.BLL.Interfaces;
using BIVALE.ApiFunctions.Interfaces;
using BIVALE.ApiFunctions.Models;
using BIVALE.BLL.Services;

namespace BIVALE.ApiFunctions.Configs
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
