using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureFunctions.Autofac;
using BIVALEApiFunctions.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using BIVALEApiFunctions.Configs;

namespace BIVALEApiFunctions.AnimalsHttpTrigger
{
    [DependencyInjectionConfig(typeof(AutofacConfig))]
    public static class AnimalNoisesHttpTrigger
    {
        [FunctionName("AnimalNoisesHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, TraceWriter log, [Inject]IAnimal animal)
        {
            log.Info("C# HTTP trigger function processed a request.");
			HttpResponseMessage response = null;
			await Task.Run(() =>
			{
				response = req.CreateResponse(HttpStatusCode.OK, animal.MakeNoise());
			});

			return response;
		}
    }
}
