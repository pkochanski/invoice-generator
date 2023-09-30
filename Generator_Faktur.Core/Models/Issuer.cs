using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Generator_Faktur.Core.Models
{
    public class Issuer : INotifyPropertyChanged
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
        private string _NIP { get; set; }
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
        private string _euroBankAccountNumber;
        public string EuroBankAccountNumber
        {
            get { return _euroBankAccountNumber; }
            set
            {
                if (value != _euroBankAccountNumber)
                {
                    _euroBankAccountNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _VatEU { get; set; }
        public string VatEU
        {
            get { return _VatEU; }
            set
            {
                if (value != _VatEU)
                {
                    _VatEU = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AllDataFiled()
        {
            return _bankAccountNumber != null && _NIP != null && _name != null && _adress != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
