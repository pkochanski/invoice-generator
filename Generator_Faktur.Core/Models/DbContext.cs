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
                Name="xxxxxxxx",
                Adress="xxxxxxxxxx",
                NIP="xxxxxxxxxxxxxxx",
                Id = 1
            },
            new Client
            {
                Name="xxxx",
                Adress="xxxxxxxxxxxxxx",
                VatEU="x",
                Id = 2
            }
        };

        public static Issuer Issuer = new Issuer
        {
            Name="Piotr Kochański",
            Adress= "xxxxxxxxxxxxxxx",
            BankAccountNumber = "0000000000000000000000000",
            NIP = "xxxxxxxxxxxx",
            VatEU = "xxxxxxxxxx",
            EuroBankAccountNumber = "PL 0000000000000000000000"
        };

        public static ExchangeRateSeries EuroExchangeRate;
    }
}
