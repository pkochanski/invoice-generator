using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Faktur.Application.DTOs
{
    public class InvoiceDTO
    {
        public ClientDTO Client { get; set; }
        public IssuerDTO Issuer { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceShortDate { get; }
        public string City { get; set; }
        public double Units { get; set; }
        public string Currency { get; }
        public double InvoiceUnitValue { get; set; }
        public double InvoiceNetValue { get; set; }
        public double InvoiceBruttoValue { get; set; }
        public string VatRate { get; }
        public DateTime Date { get; set; }

        public InvoiceDTO(DateTime date, double invoiceBruttoValue, double invoiceNetValue, double invoiceUnitValue, 
            double units, string city, string invoiceNumber, IssuerDTO issuer, ClientDTO client)
        {
            Date = date;
            InvoiceBruttoValue = invoiceBruttoValue;
            InvoiceNetValue = invoiceNetValue;
            InvoiceUnitValue = invoiceUnitValue;
            Units = units;
            City = city;
            InvoiceNumber = invoiceNumber;
            Issuer = issuer;
            Client = client; 
            VatRate = Client.LocalClient ? "23%" : "Odwrotne obciążenie / reverse charge";
            Currency = Client.LocalClient ? "PLN" : "euro";
            InvoiceShortDate = Date.ToShortDateString();
        }
    }
}
