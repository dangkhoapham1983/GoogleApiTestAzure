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
using BIVALE.DTO;

namespace BIVALE.ApiFunctions.AccountAdder
{
    [DependencyInjectionConfig(typeof(AutofacConfig))]
    public static class AccountAdder
    {
        [FunctionName("AccountAdder")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, 
                                                          TraceWriter log,
                                                          [Inject]IUserServices userServices)
        {
            log.Info("Received new account request");
            HttpResponseMessage response;
            try
            {
				var user = await req.Content.ReadAsAsync<UserDTO>();
                if (user != null)
                {
                    log.Info("Saving to database");
					userServices.InsertUser(user);
                    response = req.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    var errorMessage = "Failed to parse user";
                    log.Error(errorMessage);
                    response = req.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
                }
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
