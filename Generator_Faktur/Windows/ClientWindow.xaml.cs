using Generator_Faktur.Core.Models;
using Generator_Faktur.ViewModels;
using System.Text;
using System.Windows;

namespace Generator_Faktur.Windows
{
    /// <summary>
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private ClientViewModel _viewModel;

        public ClientWindow()
        {
            _viewModel = new ClientViewModel();
            DataContext = _viewModel;
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.Focus();
        }

        public ClientWindow(Client client)
        {
            _viewModel = new ClientViewModel(client);
            DataContext = _viewModel;
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    


    
}
