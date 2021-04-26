using Microsoft.Extensions.Logging;
using RexStudios.DynamicsCRM.Actions.Common;
using RexStudios.DynamicsCRM.Actions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RexStudios.DynamicsCRM.Actions
{
    public class CountriesServiceCrm:BaseService,ICountryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VantageServiceCrm"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="VantageOptions"/>.</param>
        /// <param name="httpService">The httpService<see cref="IHttpService"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        public CountriesServiceCrm(ApiOptions options, IHttpService httpService, ILogger logger)
            : base(options, httpService, logger)
        {
        }

        /// <summary>
        /// This method is for getting contact details for given ID.
        /// </summary>
        /// <param name="contactId">.</param>
        /// <returns>.</returns>
        public async Task<Modals.Countries> GetCountryByName(string query)
        {
            try
            {
                var url = _options.ApiUrl + "?" + query;
                _logger.LogInformation($"1: Get Countries URL: {url}");
                var response = await _httpService.GetObjectAsync<Modals.Countries>(url, Getheaders());
                _logger.LogInformation($"2: Get Countries from Api Using Name: {url}");

                if (response.IsSuccessful)
                {
                    return response?.Result;
                }
                else
                {
                    _logger.LogInformation($"Error: GetCountriesByName : { response.Message  }");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: GetCountriesByName : { ex.Message  }");
                return null;
            }
        }
    }
}
