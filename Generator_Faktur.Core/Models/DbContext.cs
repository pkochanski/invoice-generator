using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Generator_Faktur.Core.Models
{
    public static class DbContext
    {
        public static ObservableCollection<Client> Clients = new ObservableCollection<Client> { 
            new Client {
                Name="Billennium S.A.",
                Adress="00-667 Warszawa, ul. Koszykowa 61",
                NIP="5252259585",
                Id = 1
            },
            new Client
            {
                Name="Oodit",
                Adress="Anderlechtlaan 181, 1066HM Amsterdam",
                VatEU="NL859127916B01",
                Id = 2
            }
        };

        public static Issuer Issuer = new Issuer
        {
            Name="Piotr Kochański",
            Adress= "87-312 Pokrzydowo,Pokrzydowo 226",
            BankAccountNumber = "82 1050 1807 1000 0097 2287 4915",
            NIP = "7393889757",
            VatEU = "PL7393889757",
            EuroBankAccountNumber = "PL 22 1050 1807 1000 0097 6499 3201"
        };

        public static ExchangeRateSeries EuroExchangeRate;
    }
}
