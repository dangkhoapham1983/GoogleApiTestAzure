using Autofac;
using AzureFunctions.Autofac.Configuration;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Services;
using BIVALE.GoogleClient.Interfaces;
using BIVALE.GoogleClient.Services;

namespace BIVALE.ApiFunctions.Configs
{
    public class AutofacConfig
    {
        public AutofacConfig()
        {
            DependencyInjection.Initialize(builder =>
            {
				builder.RegisterType<UserServices>().As<IUserServices>();
				builder.RegisterType<HistoryServices>().As<IHistoryServices>();
				builder.RegisterType<GoogleServices>().As<IGoogleServices>();
			});
        }
    }
}
