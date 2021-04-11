using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Countries.DataAccessLayer
{
    public class CountryData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("countrycode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("subregion")]
        public string SubRegion { get; set; }

        [JsonPropertyName("intermediateregion")]
        public string IntermediateRegion { get; set; }

        [JsonPropertyName("regioncode")]
        public string RegionCode { get; set; }

        [JsonPropertyName("subregioncode")]
        public string SubRegionCode { get; set; }

        [JsonPropertyName("intermediateregioncode")]
        public string IntermediateRegionCode { get; set; }

        [JsonPropertyName("iso_3166-2")]
        public string IsoCode { get; set; }

        [JsonPropertyName("alpha2")]
        public string AlphaTwo { get; set; }

        [JsonPropertyName("alpha3")]
        public string AlphaThree { get; set; }
    }
}
