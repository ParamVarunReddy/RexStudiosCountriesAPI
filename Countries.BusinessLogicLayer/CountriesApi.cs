using Countries.DataAccessLayer;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;


namespace Countries.BusinessLogicLayer
{
    public class CountriesApi
    {

        public async Task<List<CountryData>> LookupAndList(string filter, string filterValue,string directoryPath)
        {
            var t = await this.GetList(filter, filterValue, directoryPath);
            return t;
        }

        public async Task<List<CountryData>> GetList(string requestFilter, string filterValue,string directoryPath )
        {
            CountryData countryData = new CountryData();
            List<CountryData> listCountry = new List<CountryData>();
            string countryDataPath = Path.Combine(directoryPath, "Countries.json");
            var countryDataList = JsonSerializer.Deserialize<List<Country>>(File.ReadAllText(countryDataPath));
            if (countryDataList.Count > 0)
            {
                var country = countryDataList.Where(country => country.Name == filterValue).ToList();
                foreach(var c in country)
                {
                    countryData.Name = c.Name;
                    countryData.Region = c.Region;
                    countryData.RegionCode = c.RegionCode;
                    countryData.SubRegion = c.SubRegion;
                    countryData.SubRegionCode = c.SubRegionCode;
                    countryData.IsoCode = c.IsoCode;
                    countryData.IntermediateRegion = c.IntermediateRegion;
                    countryData.IntermediateRegionCode = c.IntermediateRegionCode;
                    countryData.AlphaThree = c.AlphaThree;
                    countryData.AlphaTwo = c.AlphaTwo;
                    countryData.CountryCode = c.CountryCode;

                    listCountry.Add(countryData);
                }
            }

            return listCountry;

        }



    }
}
