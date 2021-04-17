using Countries.DataAccessLayer;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Countries.BusinessLogicLayer
{
    public class CountriesApi
    {

        public async Task<List<CountryData>> LookupAndList(string filter, string filterValue, string directoryPath,ILogger logger)
        {
            return await this.ProcessCountryRequest(filter, filterValue, directoryPath,logger);
        }

        public async Task<List<CountryData>> ProcessCountryRequest (string requestFilter, string filterValue, string directoryPath, ILogger logger)
        {
            CountriesBO countriesBusinessObject = new CountriesBO();
            try
            {
                var countryDataList = JsonSerializer.Deserialize<List<Country>>(File.ReadAllText(Path.Combine(directoryPath, "Countries.json")));
                logger.LogInformation($"3 deserialized total country json, and the count is{countryDataList.Count}");
                var graphCountryyList = JsonSerializer.Deserialize<List<GraphCountries>>(File.ReadAllText(Path.Combine(directoryPath, "GraphCountries.json")));
                logger.LogInformation($"4 deserialized total graph country json, and the count is{graphCountryyList.Count}");
                var restCountriesList = JsonSerializer.Deserialize<List<RestCountries>>(File.ReadAllText(Path.Combine(directoryPath, "RestCountries.json")));
                logger.LogInformation($"5 deserialized total rest country json, and the count is{restCountriesList.Count}");

                switch (requestFilter)
                {
                    case "name":
                        logger.LogInformation($"6 Requestfilter for data Query{requestFilter}");
                        return await countriesBusinessObject.MapData(countryDataList.Where(a => a.Name.Equals(filterValue)).ToList(), graphCountryyList.Where(b => b.Name.Equals(filterValue)).ToList(), restCountriesList.Where(c => c.Name.Equals(filterValue)).ToList(),logger);
                    case "alpha2Code":
                        logger.LogInformation($"6 Requestfilter for data Query{requestFilter}");
                        return await countriesBusinessObject.MapData(countryDataList.Where(a => a.AlphaTwo.Equals(filterValue)).ToList(), graphCountryyList.Where(b => b.Alpha2Code.Equals(filterValue)).ToList(), restCountriesList.Where(c => c.Alpha2Code.Equals(filterValue)).ToList(), logger);
                    case "alpha3Code":
                        logger.LogInformation($"6 Requestfilter for data Query{requestFilter}");
                        return await countriesBusinessObject.MapData(countryDataList.Where(a => a.AlphaThree.Equals(filterValue)).ToList(), graphCountryyList.Where(b => b.Alpha3Code.Equals(filterValue)).ToList(), restCountriesList.Where(c => c.Alpha3Code.Equals(filterValue)).ToList(), logger);
                    default: 
                        return await countriesBusinessObject.MapData(countryDataList, graphCountryyList, restCountriesList, logger);

                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed at ProcessCountryRequest error: {ex.Message} inner exception:{ex.InnerException}");
                return null;
            }
        }

        List<CountryData> FilterCompanies(List<CountryData> list, string propertyName, string query)
        {
            var pi = typeof(CountryData).GetProperties().Where(o => o.Name.ToLower().Contains(propertyName)).FirstOrDefault();

            query = query.ToLower();
            return list
                .Where(x => pi.GetValue(x).ToString().ToLower().Contains(query))
                .ToList();
        }

        List<CountryData> FilterCompany(IEnumerable<CountryData> unfilteredList, string query, params Func<CountryData, string>[] properties)
        {
            return unfilteredList.Where(x => properties.Any(c => c.Invoke(x) == query)).ToList();
        }

    }
}
