using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Faktur.Application.Policies
{
    public class PolishTranslationPolicy:ITranslationPolicy
    {
        public string NoHeader { get; set; } = "L.p.";
        public string DescriptionHeader { get; set; } = "Nazwa";
        public string UnitHeader { get; set; } = "J.m.";
        public string QtyHeader { get; set; } = "Ilość";
        public string UnitPriceHeader { get; set; } = "Cena jednostkowa netto";
        public string NetValueHeader { get; set; } = "Wartość netto";
        public string TotalHeader { get; set; } = "Razem";
        public string ServiceDescription { get; set; } = "Usługi informatyczne";
        public string QtyHour { get; set; } = "godz.";
        public string QtyPc { get; set; } = "szt";
        public string SellMonth { get; set; } = "Miesiąc sprzedaży:";
        public string VatRateHeader { get; set; } = "Według stawki VAT";
        public string InWords { get; set; } = "Słownie:";
        public string ToPay { get; set; } = "Do zapłaty";
        public string ReverseCharge { get; set; } = "Odwrotne obciążenie";
        public string ReverseChargeDescription { get; set; } = "Obciążenie odwrotne / Reverse Charge \n Usługi podlegające odwrotnemu obciążeniu podatkiem VAT rozliczane przez odbiorcę zgodnie z art. 196 dyrektywy Rady 2006 / 112 / WE \n Services subject to the reverse charge VAT to be accounted for by the recipient as per Article 196 of Council Directive 2006/112/EC";
        public string IssuerHeader { get; set; } = "Sprzedawca";
        public string BuyerHeader { get; set; } = "Nabywca";
        public string PaymentMethod { get; set; } = "przelew";
        public string PaymentDueDate { get; set; } = "30 dni";
        public string InvoiceNumber { get; set; } = "FAKTURA VAT nr";
        public string DateAndPlace { get; set; } = "Miejscowość i data wystawienia faktury";
        public string BankAccount { get; set; } = "Rachunek:";
        public string PaymentMethodHeader { get; set; } = "Sposób zapłaty:";
        public string PaymentDueDateHeader { get; set; } = "Termin zapłaty:";

        public bool CanBeApplied(bool english)
        {
            return !english;
        }
    }
}
