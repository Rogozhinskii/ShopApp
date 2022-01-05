using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using ShopUI.Core.MVVM;
using ShopUI.Services;
using ShopUI.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopUI.Modules.Products.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;
       


        public CustomersViewModel(IRepositoryManager repositoryManager, IEventAggregator eventAggregator,IDialogService dialogService)
        {
            _repositoryManager = repositoryManager;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        private DelegateCommand<Customer> _selectionChanged;
        public DelegateCommand<Customer> SelectionChanged =>
           _selectionChanged ??= _selectionChanged = new((customer)=>ExecuteSelectionChanged(customer));

        void ExecuteSelectionChanged(Customer customer)
        {
            if (customer == null) return;
            _eventAggregator.GetEvent<OnSelectedCustomerChanged>().Publish(customer);            
        }


        //private Customer _selectedCustomer;
        //public Customer SelectedCustomer
        //{
        //    get { return _selectedCustomer; }
        //    set { SetProperty(ref _selectedCustomer, value);}
        //}

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Task.Run(async() =>{
                _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Visible);
                var repo=_repositoryManager.GetRepository(RepositoryType.Customers) as IRepository<Customer>; 
                Customers = new ObservableCollection<Customer>(await repo.Select());                
                _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Hidden);

            });
        }
    }
}
