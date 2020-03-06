using AzureFunctions.Autofac;
using BIVALE.ApiFunctions.Configs;
using BIVALE.BLL.Interfaces;
using BIVALE.GoogleClient.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static BIVALE.GoogleClient.Extend.GoogleExtension;

namespace BIVALE.ApiFunctions.HistoryHttpTrigger
{
    [DependencyInjectionConfig(typeof(AutofacConfig))]
    public static class HistoryHttpTrigger
    {
        [FunctionName("History")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req,
                                                          TraceWriter log,
                                                          [Inject]IHistoryServices historyServices,
                                                           [Inject]IUserServices userServices,
                                                          [Inject]IGoogleServices googleServices)
        {
            log.Info("Received new parametters request");
            HttpResponseMessage response = null;
            try
            {
                string access_token = req.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "access_token").Value;
                string start_date = req.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "start_date").Value;
                string end_date = req.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "end_date").Value;
                string start_time = req.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "start_time").Value;
                string end_time = req.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "end_time").Value;
                string smart_gateway_id = req.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "smart_gateway_id").Value;
                string category = req.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "category").Value;

                User us = null;
                if (!string.IsNullOrEmpty(access_token))
                {
                    us = await googleServices.GetUserInfo(new Token { AccessToken = access_token });
                }

                if (us != null)
                {
                    // Get History 
                    log.Info("Getting from database");
                    var userObj = userServices.GetUserByEmailAddress(us.Email);
                    if (userObj == null)
                    {
                        response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent("User not found!", Encoding.UTF8, "application/json")
                        };
                    }
                    else
                    {
                        var myObj = historyServices.GetHistoriesByConditions(userObj, 
                            start_date, end_date, start_time, end_time, smart_gateway_id, category);
                        var jsonToReturn = JsonConvert.SerializeObject(myObj);
                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
                        };
                    }
                }
                else
                {
                    var jsonToReturn = JsonConvert.SerializeObject(us);
                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
                    };
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
