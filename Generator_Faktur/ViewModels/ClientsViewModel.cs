using Generator_Faktur.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Generator_Faktur.ViewModels
{
    public class ClientsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Client> _clients = DbContext.Clients;
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = DbContext.Clients;
                OnPropertyChanged();
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                if (value != _selectedClient)
                {
                    _selectedClient = value;
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
