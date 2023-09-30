using Generator_Faktur.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Faktur.Application.Services
{
    public interface IExchangeService
    {
        Task<ExchangeRateSeries> GetEuroRate();
    }
}
