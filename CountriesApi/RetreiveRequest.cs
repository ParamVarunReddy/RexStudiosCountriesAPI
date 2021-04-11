using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CountriesApi
{
    public static class RetreiveRequest
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "countries/filter")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CountryRetrieveRequest processed a new request:");
            #region process filter query parameters
            string filter;
            string countryCode = req.Query["countryCode"];
            string region = req.Query["region"];
            string regionCode = req.Query["regionCode"];
            string subRegion = req.Query["subRegion"];
            string subRegionCode = req.Query["subRegionCode"];

            if (!String.IsNullOrEmpty(countryCode))
            {
                filter = $"_customerid_value eq '{countryCode}'";
            }
            else if (!String.IsNullOrEmpty(region))
            {
                filter = $"ulx_externalincidentid eq '{region}'";
            }
            else if (!String.IsNullOrEmpty(regionCode))
            {
                filter = $"_ulx_relatedmatterid_value eq '{regionCode}'";
            }
            else if (!String.IsNullOrEmpty(subRegion))
            {
                filter = $"ulx_servicerequestnumber eq '{subRegion}'";
            }
            else if (!String.IsNullOrEmpty(subRegionCode))
            {
                filter = $"ulx_servicerequestnumber eq '{subRegionCode}'";
            }
            else
            {
                filter = req.Query["filter"];
            }
            #endregion process filter query parameters

            
            return new OkObjectResult(responseMessage);
        }
    }
}

