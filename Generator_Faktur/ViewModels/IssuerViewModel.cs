using Generator_Faktur.Core.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Generator_Faktur.ViewModels
{
    public class IssuerViewModel: INotifyPropertyChanged
    {
        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _adress { get; set; }
        public string Adress
        {
            get { return _adress; }
            set
            {
                if (value != _adress)
                {
                    _adress = value;
                    OnPropertyChanged();
                }
            }
        }
        public string _NIP { get; set; }
        public string NIP
        {
            get { return _NIP; }
            set
            {
                if (value != _NIP)
                {
                    _NIP = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _bankAccountNumber;
        public string BankAccountNumber
        {
            get { return _bankAccountNumber; }
            set
            {
                if (value != _bankAccountNumber)
                {
                    _bankAccountNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IssuerViewModel()
        {
            Name = DbContext.Issuer?.Name;
            BankAccountNumber = DbContext.Issuer?.BankAccountNumber;
            NIP = DbContext.Issuer?.NIP;
            Adress = DbContext.Issuer?.Adress;

        }

        public void Save()
        {
            DbContext.Issuer = new Issuer
            {
                Adress = _adress,
                BankAccountNumber = _bankAccountNumber,
                Name = _name,
                NIP = _NIP
            };
        }
    }
}
