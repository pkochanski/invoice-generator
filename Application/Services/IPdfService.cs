using Generator_Faktur.Application.DTOs;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Faktur.Application.Services
{
    public interface IPdfService
    {
        Task<PdfDocument> MakePdfInvoice(InvoiceDTO dto);
        string SavePdf(PdfDocument document, DateTime date);
        void OpenPdf(string filename);
    }
}
