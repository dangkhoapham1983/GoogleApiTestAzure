using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureFunctions.Autofac;
using BIVALE.ApiFunctions.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using BIVALE.ApiFunctions.Configs;
using BIVALE.GoogleClient.Services;
using BIVALE.GoogleClient;

namespace BIVALE.ApiFunctions.AnimalsHttpTrigger
{
    [DependencyInjectionConfig(typeof(AutofacConfig))]
    public static class AnimalNoisesHttpTrigger
    {
        [FunctionName("AnimalNoisesHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, TraceWriter log, [Inject]IAnimal animal)
        {
            log.Info("C# HTTP trigger function processed a request.");
			HttpResponseMessage response = null;
			string code = req.GetQueryNameValuePairs()
				.FirstOrDefault(q => q.Key == "code")
				.Value;

			dynamic data = await req.Content.ReadAsAsync<object>();
			code = code ?? data?.text;
			GoogleService a = new GoogleService();
			var result = new UserGoogle();
			if (string.IsNullOrEmpty(code))
			{
				result = a.Validate();
			}
			else
			{
				result = await a.CodeValidate(code);
			}
			
			await Task.Run(() =>
			{
				response = req.CreateResponse(HttpStatusCode.OK, result);
			});

			return response;
		}
    }
}
