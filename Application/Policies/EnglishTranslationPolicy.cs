using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Faktur.Application.Policies
{
    public class EnglishTranslationPolicy : ITranslationPolicy
    {
        public string NoHeader { get; set; } = "L.p. / No";
        public string DescriptionHeader { get; set; } = "Nazwa / Description";
        public string UnitHeader { get; set; } = "J.m. / Unit";
        public string QtyHeader { get; set; } = "Ilość / Qty";
        public string UnitPriceHeader { get; set; } = "Cena jedn. netto / Unit price";
        public string NetValueHeader { get; set; } = "Wartość netto / net value";
        public string TotalHeader { get; set; } = "Razem / Total";
        public string ServiceDescription { get; set; } = "Web application develpment";
        public string QtyHour { get; set; } = "godz. / hour";
        public string QtyPc { get; set; } = "szt / pc";
        public string SellMonth { get; set; } = "Miesiąc sprzedaży / month of sell:";
        public string VatRateHeader { get; set; } = "Według stawki VAT / According to VAT rate";
        public string InWords { get; set; } = "Słownie / In words:";
        public string ToPay { get; set; } = "Do zapłaty / To pay";
        public string ReverseCharge { get; set; } = "Odwrotne obciążenie / reverse charge";
        public string ReverseChargeDescription { get; set; } = "Obciążenie odwrotne / Reverse Charge \n Usługi podlegające odwrotnemu obciążeniu podatkiem VAT rozliczane przez odbiorcę zgodnie z art. 196 dyrektywy Rady 2006 / 112 / WE \n Services subject to the reverse charge VAT to be accounted for by the recipient as per Article 196 of Council Directive 2006/112/EC";
        public string IssuerHeader { get; set; } = "Sprzedawca / Seller";
        public string BuyerHeader { get; set; } = "Nabywca / Buyer";
        public string PaymentMethod { get; set; } = "przelew / transfer";
        public string PaymentDueDate { get; set; } = "15 dni / days";
        public string InvoiceNumber { get; set; } = "FAKTURA VAT nr / Invoice";
        public string DateAndPlace { get; set; } = "Miejscowość i data wystawienia faktury / Place and date of invoice:";
        public string BankAccount { get; set; } = "Rachunek / Bank account:";
        public string PaymentMethodHeader { get; set; } = "Sposób zapłaty / Payment method:";
        public string PaymentDueDateHeader { get; set; } = "Termin zapłaty / Payment due date:";

        public bool CanBeApplied(bool english)
        {
            return english;
        }
    }
}
