using System;
using System.IO;
using System.Threading.Tasks;
using Countries.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Countries.BusinessLogicLayer;
using System.Text.Json;
using System.Collections.Generic;

namespace CountriesApi
{
    public static class RetreiveRequest
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "countries/list")] HttpRequest req,
            ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation("CountryRetrieveRequest processed a new request:");
            #region process filter query parameters
            string filter;
            string filterValue;
            string countryCode = req.Query["countryCode"];
            string region = req.Query["region"];
            string regionCode = req.Query["regionCode"];
            string subRegion = req.Query["subRegion"];
            string subRegionCode = req.Query["subRegionCode"];
            string name = req.Query["name"];

            if (!String.IsNullOrEmpty(countryCode))
            {
                filter = $"countrycode";
                filterValue = $"{countryCode}";
            }
            else if (!String.IsNullOrEmpty(region))
            {
                filter = $"region";
                filterValue = $"{region}";
            }
            else if (!String.IsNullOrEmpty(name))
            {
                filter = $"name";
                filterValue = $"{name}";
            }
            else if (!String.IsNullOrEmpty(regionCode))
            {
                filter = $"regioncode ";
                filterValue = $"{regionCode}";
            }
            else if (!String.IsNullOrEmpty(subRegion))
            {
                filter = $"subregion";
                filterValue = $"{subRegion}";
            }
            else if (!String.IsNullOrEmpty(subRegionCode))
            {
                filter = $"subregioncode";
                filterValue = $"{subRegionCode}";
            }
            else
            {
                filter = req.Query["filter"];
                filterValue = string.Empty;
            }
            #endregion process filter query parameters

            try
            {
                var requestUrl = req.GetDisplayUrl();
                var countriesList = await GetCountriesList(filter, filterValue, executionContext, log);
                if (countriesList != null)
                {
                    log.LogInformation($"Successfully retrieved Service Request List with filter: {filter}.");
                }
                else
                {
                    return new NotFoundObjectResult($"Failed to retrieve Service Request List with filter: ({filter}).");
                }
                return new OkObjectResult(JsonSerializer.Serialize<List<CountryData>>(countriesList));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Failed to retrieve Service Request List with filter ({filter}).");
            }
        }

        private static async Task<List<CountryData>> GetCountriesList(string filter, string filterValue,ExecutionContext executionContext, ILogger log)
        {
            var countriesApi = new Countries.BusinessLogicLayer.CountriesApi();
            string directoryPath = Path.Combine(executionContext.FunctionDirectory);
            var t = await countriesApi.LookupAndList(filter,filterValue, directoryPath);

            return t;
        }
    }
}

