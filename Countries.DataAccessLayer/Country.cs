using System;
using System.Text.Json.Serialization;

namespace Countries.DataAccessLayer
{
    public class Country
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("alpha-2")]
        public string AlphaTwo { get; set; }

        [JsonPropertyName("alpha-3")]
        public string AlphaThree { get; set; }

        [JsonPropertyName("country-code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("iso_3166-2")]
        public string IsoCode { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("sub-region")]
        public string SubRegion { get; set; }

        [JsonPropertyName("intermediate-region")]
        public string IntermediateRegion { get; set; }

        [JsonPropertyName("region-code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("sub-region-code")]
        public string SubRegionCode { get; set; }

        [JsonPropertyName("intermediate-region-code")]
        public string IntermediateRegionCode { get; set; }

    }
}
