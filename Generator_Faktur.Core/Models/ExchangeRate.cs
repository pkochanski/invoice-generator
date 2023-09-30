using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Faktur.Core.Models
{
    public class ExchangeRateSeries
    {
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public IEnumerable<CurrencyRate> Rates { get; set; }
    }

    public class CurrencyRate
    {
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public string Mid { get; set; }
    }
}
