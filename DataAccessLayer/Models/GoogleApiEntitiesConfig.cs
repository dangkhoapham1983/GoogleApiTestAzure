using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	[DbConfigurationType(typeof(MyConfiguration))]
	partial class GoogleApiEntities
	{
		public GoogleApiEntities(string connection)
			: base(ConfigurationManager.ConnectionStrings["GoogleApiEntities"].ConnectionString)
		{
		}
	}

	public class MyConfiguration : DbConfiguration
	{
		public MyConfiguration()
		{
			SetProviderServices("System.Data.EntityClient",	SqlProviderServices.Instance);
			SetDefaultConnectionFactory(new SqlConnectionFactory());
		}
	}
}
