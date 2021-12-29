using Prism.Regions;
using ShopLibrary.Models;
using ShopUI.Core.MVVM;
using ShopUI.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ShopUI.Modules.Products.ViewModels
{
    public class CustomersViewModel:ViewModelBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public CustomersViewModel(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Task.Run(() =>
            {
                Customers = new ObservableCollection<Customer>(_repositoryManager.CustomersRepository.GetAll());
            });
        }
    }
}
