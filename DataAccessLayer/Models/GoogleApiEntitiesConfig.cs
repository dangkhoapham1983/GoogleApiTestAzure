using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.DAL.Models
{
	[DbConfigurationType(typeof(BIVALIEConfiguration))]
	partial class GoogleApiEntities
	{
		public GoogleApiEntities(string connection)
			: base(ConfigurationManager.ConnectionStrings["GoogleApiEntities"].ConnectionString)
		{
			Configuration.LazyLoadingEnabled = true;
			Configuration.ProxyCreationEnabled = true;
		}
	}

	public class BIVALIEConfiguration : DbConfiguration
	{
		public BIVALIEConfiguration()
		{
			SetProviderServices("System.Data.EntityClient",	SqlProviderServices.Instance);
			SetDefaultConnectionFactory(new SqlConnectionFactory());
		}
	}
}
