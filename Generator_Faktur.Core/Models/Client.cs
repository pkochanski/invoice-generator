using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Generator_Faktur.Core.Models
{
    public class Client:INotifyPropertyChanged
    {
        private int _id;
        public int Id { get { return _id; } set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
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

        private string _VatEU { get; set; }
        public string VatEU
        {
            get { return _VatEU; }
            set
            {
                if(value != _VatEU)
                {
                    _VatEU = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
