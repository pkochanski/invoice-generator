using Generator_Faktur.ViewModels;
using System.Text;
using System.Windows;

namespace Generator_Faktur.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        private ClientsViewModel _viewModel = new ClientsViewModel();

        public ClientsWindow()
        {
            DataContext = _viewModel;
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            var addClientWindow = new ClientWindow();
            addClientWindow.Show();
        }

        private void ListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var addClientWindow = new ClientWindow(_viewModel.SelectedClient);
            addClientWindow.Show();
        }
    }
    


    
}
