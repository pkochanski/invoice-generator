using Generator_Faktur.Core.Models;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Generator_Faktur.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set
            {
                if (_adress != value)
                {
                    _adress = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _NIP;
        public string NIP
        {
            get { return _NIP; }
            set
            {
                if (_NIP != value)
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
                if (value != _VatEU)
                {
                    _VatEU = value;
                    OnPropertyChanged();
                }
            }
        }

        public ClientViewModel()
        {

        }

        public ClientViewModel(Client client)
        {
            NIP = client.NIP;
            Adress = client.Adress;
            Name = client.Name;
            _id = client.Id;
            VatEU = client.VatEU;
        }

        public void Save()
        {
            if (_id != 0)
            {
                EditClient();
            }
            else
            {
                AddNewClient();
            }
        }

        private void EditClient()
        {
            var client = DbContext.Clients.FirstOrDefault(x => x.Id == _id);
            if (client != null)
            {
                client.Name = Name;
                client.Adress = Adress;
                client.NIP = NIP;
            }
            else
            {
                AddNewClient();
            }
        }

        private void AddNewClient()
        {
            DbContext.Clients.Add(new Client
            {
                Name = Name,
                NIP = NIP,
                Adress = Adress,
                Id = DbContext.Clients.Count + 1
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
