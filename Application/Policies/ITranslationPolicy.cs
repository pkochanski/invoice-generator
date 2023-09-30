using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Faktur.Application.Policies
{
    public interface ITranslationPolicy
    {
        bool CanBeApplied(bool english);
        string NoHeader { get; set; }
        string DescriptionHeader { get; set; }
        string UnitHeader { get; set; }
        string QtyHeader { get; set; }
        string UnitPriceHeader { get; set; }
        string NetValueHeader { get; set; }
        string TotalHeader { get; set; }
        string ServiceDescription { get; set; }
        string QtyHour { get; set; }
        string QtyPc { get; set; }
        string SellMonth { get; set; }
        string VatRateHeader { get; set; }
        string InWords { get; set; }
        string ToPay { get; set; }
        string ReverseCharge { get; set; }
        string ReverseChargeDescription { get; set; }
        string IssuerHeader { get; set; }
        string BuyerHeader { get; set; }
        string PaymentMethod { get; set; }
        string PaymentDueDate { get; set; }
        string InvoiceNumber { get; set; }
        string DateAndPlace { get; set; }
        string BankAccount { get; set; }
        string PaymentMethodHeader { get; set; }
        string PaymentDueDateHeader { get; set; }

    }
}
