using ShopUI.Modules.Customers.ViewModels;
using System.Windows.Controls;

namespace ShopUI.Modules.Customers.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private ProductsViewModel _vm;
        public ProductsView()
        {
            InitializeComponent();
            _vm = (ProductsViewModel)_productsList.DataContext;
            _vm.sourseUpdateEvent += ProductsCollectionUpdated;
        }

        private void ProductsCollectionUpdated(object sender, System.EventArgs e)
        {
            _productsList.Items.Refresh();
        }
    }
}
