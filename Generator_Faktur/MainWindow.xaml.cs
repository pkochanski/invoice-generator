
using Generator_Faktur.Core.Models;
using Generator_Faktur.ViewModels;
using Generator_Faktur.ViewModels.Locators;
using Generator_Faktur.Windows;
using System.Text;
using System.Windows;

namespace Generator_Faktur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FormViewModel _viewModel;
        

        public MainWindow()
        {
            _viewModel = IocKernel.Get<FormViewModel>();
            DataContext = _viewModel;
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.Client == null)
            {
                MessageBox.Show("Wbierz klienta", "Błąd generowania", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(!DbContext.Issuer.AllDataFiled()){

                MessageBox.Show("Uzupełnij swoje dane", "Błąd generowania", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _viewModel.MakePDFInvoice();
            }
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            var clientsWindow = new ClientsWindow();
            clientsWindow.Show();
        }

        private void MyDataButton_Click(object sender, RoutedEventArgs e)
        {
            var issuerWindow = new IssuerWindow();
            issuerWindow.Show();
        }
    }
    


    
}
