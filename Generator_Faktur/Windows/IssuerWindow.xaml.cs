using Generator_Faktur.ViewModels;
using System.Text;
using System.Windows;

namespace Generator_Faktur.Windows
{
    /// <summary>
    /// Interaction logic for IssuerWindow.xaml
    /// </summary>
    public partial class IssuerWindow : Window
    {
        private IssuerViewModel _viewModel;

        public IssuerWindow()
        {
            _viewModel = new IssuerViewModel();
            DataContext = _viewModel;
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.Focus();
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
