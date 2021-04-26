using System;
using System.Collections.Generic;
using System.Text;

namespace RexStudios.DynamicsCRM.Actions.Common
{
    /// <summary>
    /// Base class for all options
    /// </summary>
    public class ApiOptions
    {
        /// <summary>
        /// Api url to retrieve token
        /// </summary>
        public string TokenApiUrl { get; set; }

        /// <summary>
        /// Api url to access service
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Api BaseUrl
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Key to access Apis
        /// </summary>
        public string APIMSubscriptionKey { get; set; }
    }
}
