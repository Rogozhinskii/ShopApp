using ShopUI.Modules.Products.ViewModels;
using System.Windows.Controls;

namespace ShopUI.Modules.Products.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class CustomersView : UserControl
    {
        private CustomersViewModel _vm;
        public CustomersView()
        {            
            InitializeComponent();
            _vm = (CustomersViewModel)_productsList.DataContext;
            _vm.sourseUpdateEvent += _vm_sourseUpdateEvent;
        }

        private void _vm_sourseUpdateEvent(object sender, System.EventArgs e)
        {
            _productsList.Items.Refresh();
        }

    }
}
