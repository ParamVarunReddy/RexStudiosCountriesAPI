using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;

namespace RexStudios.DynamicsCRM.Actions
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("Dynamics CRM Action inbound integration Processed a request.");
            string requestBody = await req.Content.ReadAsStringAsync();
            log.LogInformation($"Received Inbound Message {requestBody}");

            RemoteExecutionContext remoteExecutionContext = DeserializeJsonString<RemoteExecutionContext>(requestBody);

            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(remoteExecutionContext.MessageName)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {remoteExecutionContext.MessageName}. This HTTP triggered function executed successfully.";

            return responseMessage != null
                    ? new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(responseMessage)
                    }
                    : new HttpResponseMessage(System.Net.HttpStatusCode.NoContent)
                    {
                        Content = new StringContent("Record Not Inserted")
                    };
        }

        /// <summary>
        /// Function to deserialize JSON string using DataContractJsonSerializer
        /// </summary>
        /// <typeparam name="RemoteContextType">RemoteContextType Generic Type</typeparam>
        /// <param name="jsonString">string jsonString</param>
        /// <returns>Generic RemoteContextType object</returns>
        public static RemoteContextType DeserializeJsonString<RemoteContextType>(string jsonString)
        {
            //create an instance of generic type object
            RemoteContextType obj = Activator.CreateInstance<RemoteContextType>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString));
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            obj = (RemoteContextType)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
    }
}

