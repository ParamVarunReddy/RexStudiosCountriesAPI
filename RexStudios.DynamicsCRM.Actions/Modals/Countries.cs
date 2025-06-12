using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RexStudios.DynamicsCRM.Actions.Modals
{
    public class Countries
    {
    
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nativeName")]
        public string NativeName { get; set; }

        [JsonPropertyName("countrycode")]
        public string CountryCode { get; set; }

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

        [JsonPropertyName("area")]
        public double? Area { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("populationDensity")]
        public double? PopulationDensity { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("demonym")]
        public string Demonym { get; set; }

        [JsonPropertyName("gini")]
        public double? Gini { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("numericCode")]
        public string NumericCode { get; set; }

        [JsonPropertyName("subregion")]
        public Subregion Subregions { get; set; }

        [JsonPropertyName("officialLanguages")]
        public List<OfficialLanguage> OfficialLanguages { get; set; }

        [JsonPropertyName("currencies")]
        public List<Currencies> Currencies { get; set; }

        [JsonPropertyName("regionalBlocs")]
        public List<RegionalBloc> RegionalBlocs { get; set; }

        [JsonPropertyName("flag")]
        public Flag Flag { get; set; }

        [JsonPropertyName("topLevelDomains")]
        public List<TopLevelDomain> TopLevelDomains { get; set; }

        [JsonPropertyName("callingCodes")]
        public List<CallingCode> CallingCodes { get; set; }

        [JsonPropertyName("alternativeSpellings")]
        public List<AlternativeSpelling> AlternativeSpellings { get; set; }

        [JsonPropertyName("topLevelDomain")]
        public List<string> TopLevelDomain { get; set; }

        [JsonPropertyName("altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonPropertyName("timezones")]
        public List<string> Timezones { get; set; }

        [JsonPropertyName("borders")]
        public List<string> Borders { get; set; }

        [JsonPropertyName("cioc")]
        public string Cioc { get; set; }
    }

    public class CallingCode
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class AlternativeSpelling
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Flag
    {
        [JsonPropertyName("emoji")]
        public string Emoji { get; set; }

        [JsonPropertyName("emojiUnicode")]
        public string EmojiUnicode { get; set; }

        [JsonPropertyName("svgFile")]
        public string SvgFile { get; set; }
    }

    public class TopLevelDomain
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class OtherAcronym
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class OtherName
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class RegionalBloc
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("acronym")]
        public string Acronym { get; set; }

        [JsonPropertyName("otherAcronyms")]
        public List<OtherAcronym> OtherAcronyms { get; set; }

        [JsonPropertyName("otherNames")]
        public List<OtherName> OtherNames { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

    public class Region
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Subregion
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region")]
        public Region Regions { get; set; }
    }

    public class OfficialLanguage
    {
        [JsonPropertyName("iso639_1")]
        public string Iso6391 { get; set; }

        [JsonPropertyName("iso639_2")]
        public string Iso6392 { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nativeName")]
        public string NativeName { get; set; }
    }

    public class Currencies
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }

}
