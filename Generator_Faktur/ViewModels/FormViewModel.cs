using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Generator_Faktur.Application.Services;
using Generator_Faktur.Core.Models;
using Generator_Faktur.Application.DTOs;
using System.Threading.Tasks;

namespace Generator_Faktur.ViewModels
{
    public class FormViewModel: INotifyPropertyChanged
    {
        private readonly IPdfService _pdfService;

        public FormViewModel(IPdfService pdfService)
        {
            this._pdfService = pdfService;
        }

        private string _issuerBankAccountNumber;
        private string _issuerIdentifier;
        private string _clientIndentifier;
        private double _invoiceNetValue;
        private double _units;
        public double Units
        {
            get { return _units; }
            set
            {
                if (_units != value)
                {
                    _units = value;
                    InvoiceNetValue = InvoiceUnitValue * value;
                    OnPropertyChanged();
                }
            }
        }
        public double InvoiceNetValue
        {
            get { return _invoiceNetValue; }
            set
            {
                if (_invoiceNetValue != value)
                {
                    _invoiceNetValue = value;
                    InvoiceBruttoValue = Client?.VatEU is null ? value * 1.23:value;
                    OnPropertyChanged();
                }
            }
        }

        private double _invoiceUnitValue;
        public double InvoiceUnitValue
        {
            get { return _invoiceUnitValue; }
            set
            {
                if (_invoiceUnitValue != value)
                {
                    _invoiceUnitValue = value;
                    InvoiceNetValue = value * Units;
                    OnPropertyChanged();
                }
            }
        }

        private double _invoiceBruttoValue;
        public double InvoiceBruttoValue
        {
            get { return _invoiceBruttoValue; }
            set
            {
                if (_invoiceBruttoValue != value)
                {
                    _invoiceBruttoValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _invoiceNumber;
        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                if (_invoiceNumber != value)
                {
                    _invoiceNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    InvoiceNumberPart = $"PL/{value.Year}/";
                    OnPropertyChanged();
                }
            }
        }

        private string _invoiceNumberPart = "PL/YYYY/";

        public string InvoiceNumberPart
        {
            get { return _invoiceNumberPart; }
            set
            {
                if (_invoiceNumberPart != value)
                {
                    _invoiceNumberPart = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _city;

        public string City
        {
            get { return _city; }
            set
            {
                if(_city != value)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _clientId;

        public int ClientId
        {
            get { return _clientId; }
            set
            {
                if (_clientId != value)
                {
                    _clientId = value;
                    this.Client = DbContext.Clients.FirstOrDefault(x => x.Id == value);
                    _issuerBankAccountNumber = Client.VatEU is null ? DbContext.Issuer.BankAccountNumber : DbContext.Issuer.EuroBankAccountNumber;
                    _issuerIdentifier = Client.VatEU is null ? $"NIP: {DbContext.Issuer.NIP}" : $"VAT EU: {DbContext.Issuer.VatEU}";
                    _clientIndentifier = Client.VatEU is null ? $"NIP: {Client.NIP}" : $"VAT EU: {Client.VatEU}";
                    InvoiceBruttoValue = Client?.VatEU is null ? InvoiceNetValue * 1.23 : InvoiceNetValue;
                    OnPropertyChanged();
                }
            }
        }

        private Client _client;

        public Client Client
        {
            get { return _client; }
            set
            {
                if (_client != value)
                {
                    _client = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Client> _clients = DbContext.Clients;
        public ObservableCollection<Client> Clients
        {
            get { 
                return _clients; 
            }
            set
            {
                _clients = DbContext.Clients;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task MakePDFInvoice()
        {
            var clientDto = new ClientDTO
            {
                Adress = _client.Adress,
                Identifier = _clientIndentifier,
                LocalClient = _client.VatEU is null,
                Name = _client.Name
            };
            var issuerDto = new IssuerDTO
            {
                Name = DbContext.Issuer.Name,
                Adress = DbContext.Issuer.Adress,
                BankAccountNumber = _issuerBankAccountNumber,
                Identifier = _issuerIdentifier
            };
            var invoiceNumber = $"{InvoiceNumberPart}{InvoiceNumber}";
            var dto = new InvoiceDTO(Date,InvoiceBruttoValue,InvoiceNetValue,InvoiceUnitValue,Units,City,invoiceNumber, issuerDto, clientDto);


            var pdf = await _pdfService.MakePdfInvoice(dto);
            var filename = _pdfService.SavePdf(pdf,Date);
            _pdfService.OpenPdf(filename);
        }



    }
}
