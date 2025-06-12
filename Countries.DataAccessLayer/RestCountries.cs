using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Countries.DataAccessLayer
{
    public class RestCountries
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("topLevelDomain")]
        public List<string> TopLevelDomain { get; set; }

        [JsonPropertyName("alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonPropertyName("alpha3Code")]
        public string Alpha3Code { get; set; }

        [JsonPropertyName("callingCodes")]
        public List<string> CallingCodes { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("subregion")]
        public string Subregion { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("latlng")]
        public List<double> Latlng { get; set; }

        [JsonPropertyName("demonym")]
        public string Demonym { get; set; }

        [JsonPropertyName("area")]
        public double? Area { get; set; }

        [JsonPropertyName("gini")]
        public double? Gini { get; set; }

        [JsonPropertyName("timezones")]
        public List<string> Timezones { get; set; }

        [JsonPropertyName("borders")]
        public List<string> Borders { get; set; }

        [JsonPropertyName("nativeName")]
        public string NativeName { get; set; }

        [JsonPropertyName("numericCode")]
        public string NumericCode { get; set; }

        [JsonPropertyName("currencies")]
        public List<Currency> Currencies { get; set; }

        [JsonPropertyName("languages")]
        public List<Language> Languages { get; set; }

        [JsonPropertyName("translations")]
        public Translations Translations { get; set; }

        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        [JsonPropertyName("regionalBlocs")]
        public List<RegionalBlocs> RegionalBlocs { get; set; }

        [JsonPropertyName("cioc")]
        public string Cioc { get; set; }
    }
    public class Currency
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }

    public class Language
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

    public class Translations
    {
        [JsonPropertyName("de")]
        public string De { get; set; }

        [JsonPropertyName("es")]
        public string Es { get; set; }

        [JsonPropertyName("fr")]
        public string Fr { get; set; }

        [JsonPropertyName("ja")]
        public string Ja { get; set; }

        [JsonPropertyName("it")]
        public string It { get; set; }

        [JsonPropertyName("br")]
        public string Br { get; set; }

        [JsonPropertyName("pt")]
        public string Pt { get; set; }

        [JsonPropertyName("nl")]
        public string Nl { get; set; }

        [JsonPropertyName("hr")]
        public string Hr { get; set; }

        [JsonPropertyName("fa")]
        public string Fa { get; set; }
    }

    public class RegionalBlocs
    {
        [JsonPropertyName("acronym")]
        public string Acronym { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("otherAcronyms")]
        public List<string> OtherAcronyms { get; set; }

        [JsonPropertyName("otherNames")]
        public List<string> OtherNames { get; set; }
    }
}
