using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Config;

namespace BIVALEApiFunctions
{
	public class Startup : IExtensionConfigProvider
	{
		public void Initialize(ExtensionConfigContext context)
		{
			string connectionString = Environment.GetEnvironmentVariable("GoogleApiEntities");
			//builder.Services.AddDbContext<TodoContext>(
				//options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
		}
	}
}
