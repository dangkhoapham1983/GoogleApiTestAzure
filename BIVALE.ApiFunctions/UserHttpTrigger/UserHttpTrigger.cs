using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunctions.Autofac;
using BIVALE.BLL.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using BIVALE.ApiFunctions.Configs;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using BIVALE.DTO;

namespace BIVALE.ApiFunctions.UserHttpTrigger
{
    [DependencyInjectionConfig(typeof(AutofacConfig))]
    public static class UserHttpTrigger
	{
        [FunctionName("User")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, 
                                                          TraceWriter log,
                                                          [Inject]IUserServices userServices)
        {
			var tcs = new TaskCompletionSource<IEnumerable<UserDTO>>();
			log.Info("Received new account request");
            HttpResponseMessage response = null;
            try
            {
				log.Info("Getting from database");
				await Task.Run(() =>
				{
					var myObj = userServices.GetUsers();
					var jsonToReturn = JsonConvert.SerializeObject(myObj);
					response = new HttpResponseMessage(HttpStatusCode.OK)
					{
						Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
					};
				});
				
			}
            catch (Exception e)
            {
                log.Error(e.Message, e);
                response = req.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            return response;
        }
    }
}
