using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Countries.DataAccessLayer
{
    public class GraphCountries
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
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
        public Region Region { get; set; }
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

    public partial class Currency
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
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

    public class CountryModal
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nativeName")]
        public string NativeName { get; set; }

        [JsonPropertyName("alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonPropertyName("alpha3Code")]
        public string Alpha3Code { get; set; }

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
        public Subregion Subregion { get; set; }

        [JsonPropertyName("officialLanguages")]
        public List<OfficialLanguage> OfficialLanguages { get; set; }

        [JsonPropertyName("currencies")]
        public List<Currency> Currencies { get; set; }

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
    }

    public class Data
    {
        [JsonPropertyName("Country")]
        public List<CountryModal> Country { get; set; }
    }
}
