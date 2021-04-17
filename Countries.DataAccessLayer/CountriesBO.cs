using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Countries.DataAccessLayer
{
    public class CountriesBO
    {
        public async Task<List<CountryData>> MapData(List<Country> countryList, List<GraphCountries> graphCountryList, List<RestCountries> restCountry, ILogger logger)
        {
            try
            {
                if (countryList.Count > 0 && graphCountryList.Count > 0 && restCountry.Count > 0)
                {
                    var joinedCountry = countryList.Join(graphCountryList, c => c.Name, g => g.Name, (country, graphCounrty) =>
                         new
                         {
                             Name = country.Name,
                             RegionCode = country.RegionCode,
                             SubRegion = graphCounrty.Subregion,
                             SubRegionCode = country.SubRegionCode,
                             IsoCode = country.IsoCode,
                             IntermediateRegion = country.IntermediateRegion,
                             IntermediateRegionCode = country.IntermediateRegionCode,
                             AlphaThree = country.AlphaThree,
                             AlphaTwo = country.AlphaTwo,
                             CountryCode = country.CountryCode,
                             NativeName = graphCounrty.NativeName,
                             Area = graphCounrty.Area,
                             Population = graphCounrty.Population,
                             PopulationDensity = graphCounrty.PopulationDensity,
                             Capital = graphCounrty.Capital,
                             Demonym = graphCounrty.Demonym,
                             Gini = graphCounrty.Gini,
                             Location = graphCounrty.Location,
                             NumericCode = graphCounrty.NumericCode,
                             OfficialLanguages = graphCounrty.OfficialLanguages,
                             Currencies = graphCounrty.Currencies,
                             RegionalBlocs = graphCounrty.RegionalBlocs,
                             Flag = graphCounrty.Flag,
                             TopLevelDomains = graphCounrty.TopLevelDomains,
                             CallingCodes = graphCounrty.CallingCodes,
                             AlternativeSpellings = graphCounrty.AlternativeSpellings
                         });
                    logger.LogInformation($"7 country and graphcountry join data is succesful");
                    return joinedCountry.Join(restCountry, j => j.Name, r => r.Name, (jCountry, rCountry) =>
                             new CountryData
                             {
                                 Name = jCountry.Name,
                                 //Region = jCountry.Region,
                                 RegionCode = jCountry.RegionCode,
                                 Subregions = jCountry.SubRegion,
                                 SubRegionCode = jCountry.SubRegionCode,
                                 IsoCode = jCountry.IsoCode,
                                 IntermediateRegion = jCountry.IntermediateRegion,
                                 IntermediateRegionCode = jCountry.IntermediateRegionCode,
                                 AlphaThree = jCountry.AlphaThree,
                                 AlphaTwo = jCountry.AlphaTwo,
                                 CountryCode = jCountry.CountryCode,
                                 NativeName = jCountry.NativeName,
                                 Area = jCountry.Area,
                                 Population = jCountry.Population,
                                 PopulationDensity = jCountry.PopulationDensity,
                                 Capital = jCountry.Capital,
                                 Demonym = jCountry.Demonym,
                                 Gini = jCountry.Gini,
                                 Location = jCountry.Location,
                                 NumericCode = jCountry.NumericCode,
                                 OfficialLanguages = jCountry.OfficialLanguages,
                                 Currencies = jCountry.Currencies,
                                 RegionalBlocs = jCountry.RegionalBlocs,
                                 Flag = jCountry.Flag,
                                 TopLevelDomains = jCountry.TopLevelDomains,
                                 CallingCodes = jCountry.CallingCodes,
                                 AlternativeSpellings = jCountry.AlternativeSpellings,
                                 TopLevelDomain = rCountry.TopLevelDomain,
                                 AltSpellings = rCountry.AltSpellings,
                                 Timezones = rCountry.Timezones,
                                 Borders = rCountry.Borders,
                                 Cioc = rCountry.Cioc
                             }).ToList();

                }

                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed at Mapping Country Data error: {ex.Message} inner exception:{ex.InnerException}");
                return null;
            }
        }
    }
}
