using Generator_Faktur.Application.DTOs;
using Generator_Faktur.Application.Policies;
using Generator_Faktur.Core.Extentions;
using Generator_Faktur.Core.Helpers;
using Generator_Faktur.Core.Models;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfTable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Generator_Faktur.Application.Services
{
    public class PdfService : IPdfService
    {
        private readonly IExchangeService _exchangeService;

        public PdfService(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        public async Task<PdfDocument> MakePdfInvoice(InvoiceDTO dto)
        {
            var euroExchangeRate = await _exchangeService.GetEuroRate();
            var translations = GetTranslationPolicy(dto.Client.LocalClient);
            var pdf = new PdfDocument();
            var page = pdf.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var headFont = new XFont("Courier", 16, XFontStyle.Bold);
            var mainFont = new XFont("Courier", 11, XFontStyle.Regular);
            gfx.DrawString($"{translations.InvoiceNumber} {dto.InvoiceNumber}", headFont, XBrushes.Black, new XRect(0, PdfDefaultValues.DefaultBottomMargin, page.Width, page.Height), XStringFormats.TopCenter);

            gfx.DrawString($"{translations.DateAndPlace} {dto.City} {dto.InvoiceShortDate}", mainFont, XBrushes.Black, new XRect(0, headFont.Height + PdfDefaultValues.DefaultBottomMargin, page.Width, page.Height), XStringFormats.TopCenter);
            var issuerTableWith = 240;
            var issuerTableHeight = 120;
            var issuerTable = new Table
            {
                GFX = gfx,
                Width = issuerTableWith,
                ColumnsCount = 1,
                Rows = new List<TableRow> { new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                Content=new string[]{ DbContext.Issuer.Name} },new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                Content=new string[]{ DbContext.Issuer.Adress } },new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                Content=new string[]{ $"{translations.BankAccount} {dto.Issuer.BankAccountNumber}" } },new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                Content=new string[]{ dto.Issuer.Identifier} } },
                Header = new TableRow
                {
                    Content = new string[] { translations.IssuerHeader },
                    Height = PdfDefaultValues.DefaultRowHeight
                },
                ColumnsWidths = new double[] { 1 }

            };

            var buyerTable = new Table
            {
                GFX = gfx,
                Height = issuerTableHeight,
                Width = issuerTableWith,
                ColumnsCount = 1,
                Rows = new List<TableRow> { new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                Content=new string[]{ dto.Client.Name} },new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                Content=new string[]{ dto.Client.Adress} },new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                Content=new string[]{ dto.Client.Identifier } } },
                Header = new TableRow
                {
                    Content = new string[] { translations.BuyerHeader },
                    Height = PdfDefaultValues.DefaultRowHeight
                },
                ColumnsWidths = new double[] { 1 }
            };

            var paymentDueDateTable = new Table
            {
                GFX = gfx,
                Height = 50,
                Width = 240,
                ColumnsCount = 2,
                Rows = new List<TableRow> { new TableRow {
                    Height = PdfDefaultValues.DefaultRowHeight,
                    Content = new string[]{translations.PaymentMethodHeader, translations.PaymentMethod }
                }, new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                    Content = new string[]{translations.PaymentDueDateHeader, translations.PaymentDueDate }
                } },
                ColumnsWidths = new double[] { 3, 2 }

            };

            var detailsTable = new Table
            {
                GFX = gfx,
                Height = issuerTableHeight,
                Width = 555,
                Header = new TableRow
                {
                    Height = 0,
                    Content = new string[] { translations.NoHeader, translations.DescriptionHeader, translations.UnitHeader, translations.QtyHeader, $"{translations.UnitPriceHeader} ({dto.Currency})", $"{translations.NetValueHeader} ({dto.Currency})", "VAT [%]", "VAT", $"{translations.TotalHeader} ({dto.Currency})" }
                },
                ColumnsCount = 10,
                ColumnsWidths = new double[] { 1.7, 5, 2.3, 3, 5, 5, dto.Client.LocalClient ? 2 : 5, 5, 5 },
                Rows = new List<TableRow> { new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                    Content = new string[]{"1",translations.ServiceDescription,$"{(dto.Units > 1 ? translations.QtyHour : translations.QtyPc) }",$"{dto.Units}",string.Format("{0:0.00}",dto.InvoiceUnitValue), string.Format("{0:0.00}", dto.InvoiceNetValue), $"{dto.VatRate}", string.Format("{0:0.00}", dto.InvoiceBruttoValue - dto.InvoiceNetValue), string.Format("{0:0.00}", dto.InvoiceBruttoValue) } }
                }

            };
            var dateOfIssueTable = new Table
            {
                GFX = gfx,
                Width = 150,
                ColumnsCount = 1,
                ColumnsWidths = new double[] { 1 },
                Rows = new List<TableRow> { new TableRow {
                    Height = PdfDefaultValues.DefaultRowHeight,
                    Content = new string[]{translations.SellMonth }
                }, new TableRow { Height = PdfDefaultValues.DefaultRowHeight,
                    Content = new string[]{dto.Date.ToString("MM.yyyy")}
                } }

            };

            var vatSummaryTable = new Table
            {
                GFX = gfx,
                Width = page.Width / 2,
                ColumnsWidths = new double[] { 1, 1, 1, 1 },
                Header = new TableRow
                {
                    Height = PdfDefaultValues.DefaultRowHeight,
                    Content = new string[] { translations.VatRateHeader, translations.NetValueHeader, "VAT", translations.TotalHeader }
                },
                Rows = new List<TableRow>
                {
                    new TableRow{
                        Height = PdfDefaultValues.DefaultRowHeight,
                        Content = new string[]{  $"{dto.VatRate}", string.Format("{0:0.00}", dto.InvoiceNetValue), string.Format("{0:0.00}", dto.InvoiceBruttoValue - dto.InvoiceNetValue), string.Format("{0:0.00}", dto.InvoiceBruttoValue) }
                    }
                },
                Summary = new TableRow
                {
                    Height = PdfDefaultValues.DefaultRowHeight,
                    Content = new string[] { translations.TotalHeader, string.Format("{0:0.00}", dto.InvoiceNetValue), string.Format("{0:0.00}", dto.InvoiceBruttoValue - dto.InvoiceNetValue), string.Format("{0:0.00}", dto.InvoiceBruttoValue) }
                }
            };

            var numberToText = dto.Client.LocalClient ? ((int)dto.InvoiceBruttoValue).NumberToText() : $"{((int)dto.InvoiceBruttoValue).NumberToText()} / {((int)dto.InvoiceBruttoValue).NumberToWordsEng()}";
            var summaryTable = new Table
            {
                GFX = gfx,
                Width = page.Width / 2,
                ColumnsCount = 2,
                ColumnsWidths = new double[] { 6, 2 },
                Rows = new List<TableRow> { new TableRow {
                    Content = new string[]{translations.InWords,translations.ToPay},
                    Height=PdfDefaultValues.DefaultRowHeight
                },
                new TableRow{
                    Content = new string[]{numberToText,string.Format("{0:0.00}", dto.InvoiceBruttoValue) },
                    Height=PdfDefaultValues.DefaultRowHeight
                } }
            };


            var exchangeTable = new Table
            {
                GFX = gfx,
                Width = page.Width / 2,
                ColumnsCount = 1,
                ColumnsWidths = new double[] { 1 },
                Rows = new List<TableRow>
                {
                    new TableRow
                    {
                        Content = new string[] {$"1 EUR = {euroExchangeRate.Rates.FirstOrDefault().Mid} PLN"},
                        Height = PdfDefaultValues.DefaultRowHeight
                    }
                }
            };

            issuerTable.Draw(20, 2 * PdfDefaultValues.DefaultBottomMargin + headFont.Height + mainFont.Height);
            buyerTable.Draw(page.Width - 20 - buyerTable.Width, 2 * PdfDefaultValues.DefaultBottomMargin + headFont.Height + mainFont.Height);
            paymentDueDateTable.Draw(20, issuerTable.Y + issuerTable.Height + PdfDefaultValues.DefaultBottomMargin);
            dateOfIssueTable.Draw(425, paymentDueDateTable.Y + paymentDueDateTable.Height + PdfDefaultValues.DefaultBottomMargin);
            detailsTable.Draw(20, dateOfIssueTable.Y + dateOfIssueTable.Height + PdfDefaultValues.DefaultBottomMargin);
            vatSummaryTable.Draw(page.Width - 20 - vatSummaryTable.Width, detailsTable.Y + detailsTable.Height + PdfDefaultValues.DefaultBottomMargin);
            summaryTable.Draw(20, vatSummaryTable.Y + vatSummaryTable.Height + PdfDefaultValues.DefaultBottomMargin);

            if (!dto.Client.LocalClient)
            {
                AddEUTables(page, gfx, mainFont, summaryTable, exchangeTable, euroExchangeRate);
            }

            return pdf;
        }

        public void OpenPdf(string filename)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };
            process.Start();
        }

        public string SavePdf(PdfDocument document, DateTime date)
        {
            var filename = $"{DbContext.Issuer.Name?.Trim()}{date:MM.yyyy}.pdf";
            document.Save(filename);
            document.Close();
            return filename;
        }

        private void AddEUTables(PdfPage page, XGraphics gfx, XFont mainFont, Table summaryTable, Table exchangeTable, ExchangeRateSeries exchangeRates)
        {
            var tf = new XTextFormatter(gfx);
            tf.DrawString("Obciążenie odwrotne / Reverse Charge \n Usługi podlegające odwrotnemu obciążeniu podatkiem VAT rozliczane przez odbiorcę zgodnie z art. 196 dyrektywy Rady 2006 / 112 / WE \n Services subject to the reverse charge VAT to be accounted for by the recipient as per Article 196 of Council Directive 2006/112/EC",
                mainFont,
                XBrushes.Black,
                new XRect(5, summaryTable.Y + summaryTable.Height + PdfDefaultValues.DefaultBottomMargin, page.Width, page.Height),
                XStringFormats.TopLeft);

            exchangeTable.Draw(20, summaryTable.Y + summaryTable.Height + PdfDefaultValues.DefaultBottomMargin * 10);

            tf.DrawString($"NBP kurs z dnia: {exchangeRates.Rates.FirstOrDefault().EffectiveDate}, tabela: {exchangeRates.Rates.FirstOrDefault().No}",
                mainFont,
                XBrushes.Black,
                new XRect(5, exchangeTable.Y + exchangeTable.Height + PdfDefaultValues.DefaultBottomMargin, page.Width, page.Height),
                XStringFormats.TopLeft);
        }

        

        private ITranslationPolicy GetTranslationPolicy(bool isLocalClient)
        {
            var filter = new TypeFilter(TranslationInterfaceFilter);
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.FindInterfaces(filter, typeof(ITranslationPolicy)).Length >= 1))
            {
                var policy = (ITranslationPolicy)Assembly.GetExecutingAssembly().CreateInstance(type.FullName, false);
                if (policy.CanBeApplied(!isLocalClient))
                {
                    return policy;
                }
            }

            return null;
        }

        private static bool TranslationInterfaceFilter(Type typeObj, Object criteriaObj)
        {
            return typeObj.ToString() == criteriaObj.ToString();
        }
    }
}
