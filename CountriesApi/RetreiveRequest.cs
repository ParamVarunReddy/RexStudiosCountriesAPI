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
        [FunctionName("RetreiveRequest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "countries/list")] HttpRequest req,
            ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation("CountryRetrieveRequest processed a new request:");
            #region process filter query parameters
            string filter;
            string filterValue;
            string countryCode = req.Query["countryCode"];
            string name = req.Query["name"];

            if (!String.IsNullOrEmpty(countryCode))
            {
                filter = $"countrycode";
                filterValue = $"{countryCode}";
            }
            else if (!String.IsNullOrEmpty(name))
            {
                filter = $"name";
                filterValue = $"{name}";
            }
            else
            {
                filter = req.Query["filter"];
                filterValue = string.Empty;
            }
            #endregion process filter query parameters

            try
            {
                log.LogInformation($"1:Get Country(s) by {filter}: {filterValue}");
                var countriesList = await GetCountriesList(filter, filterValue, executionContext, log);
                if (countriesList != null)
                {
                    log.LogInformation($"Successfully retrieved countries List with filter: {filter}.");
                }
                else
                {
                    return new NotFoundObjectResult($"Failed to retrieve countries List with filter: ({filter}).");
                }
                return new OkObjectResult(JsonSerializer.Serialize<List<CountryData>>(countriesList));
            }
            catch (Exception ex)
            {
                log.LogError($"Failed at Get Countries error: {ex.Message} inner exception:{ex.InnerException}");
                return new BadRequestObjectResult($"Failed to retrieve countries List with filter ({filter}).");
            }
        }

        private static async Task<List<CountryData>> GetCountriesList(string filter, string filterValue, ExecutionContext executionContext, ILogger log)
        {
            try
            {
                var countriesApi = new Countries.BusinessLogicLayer.CountriesApi();
                string directoryPath = Path.Combine(executionContext.FunctionDirectory);
                log.LogInformation($"2: Get Directory Path of the Json file {directoryPath}");
                return await countriesApi.LookupAndList(filter, filterValue, directoryPath, log);
            }
            catch (Exception ex)
            {
                log.LogError($"Failed at Get CountriesList error: {ex.Message} inner exception:{ex.InnerException}");
                return null;
            }

        }
    }
}

