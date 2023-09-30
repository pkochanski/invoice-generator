using Generator_Faktur.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Faktur.Application.Services
{
    public class ExchangeRateService:IExchangeService
    {
        public async Task<ExchangeRateSeries> GetEuroRate()
        {
            var euroExchangeRate = DbContext.EuroExchangeRate;

            if(euroExchangeRate != null)
            {
                return euroExchangeRate;
            }

            using var context = new HttpClient();
            var result = await context.GetAsync("https://api.nbp.pl/api/exchangerates/rates/A/eur/");
            var stringResult = await result.Content.ReadAsStringAsync();
            DbContext.EuroExchangeRate = JsonConvert.DeserializeObject<ExchangeRateSeries>(stringResult);

            return DbContext.EuroExchangeRate; 
        }
    }
}
