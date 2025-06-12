using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;
using RexStudios.DynamicsCRM.Actions.Common;
using RexStudios.DynamicsCRM.Actions.Http;

namespace RexStudios.DynamicsCRM.Actions
{
    public abstract class BaseService
    {
        /// <summary>
        /// Vantage Options
        /// </summary>
        protected readonly ApiOptions _options;

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// HttpService
        /// </summary>
        protected readonly IHttpService _httpService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="httpService"></param>
        /// <param name="logger"></param>
        protected BaseService(ApiOptions options, IHttpService httpService, ILogger logger)
        {
            _httpService = httpService;
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// Base method to get the vanatage headers
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, string> Getheaders() => new Dictionary<string, string>()
                {
                    {Constants.COUNTRIESAPI_SECRET_KEY, _options.APIMSubscriptionKey },
                };
    }
}
