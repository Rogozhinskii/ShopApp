using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using ShopUI.Core;
using ShopUI.Core.MVVM;
using ShopUI.Services;
using ShopUI.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace ShopUI.Modules.Products.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IEventAggregator _eventAggregator;       
        private IRepository<Customer> _customersRepository;

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

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { SetProperty(ref _selectedCustomer, value); 
                if(SelectedCustomer is not null)
                    _eventAggregator.GetEvent<OnSelectedCustomerChanged>().Publish(SelectedCustomer);
            }
        }

        private DelegateCommand _editCustomerCommand;
        public DelegateCommand EditCustomerCommand =>
           _editCustomerCommand ??= _editCustomerCommand = new(ExecuteEditCustomerCommand);
        private void ExecuteEditCustomerCommand()
        {
            if(SelectedCustomer == null) return;
            DialogParameters parameters = new();
            parameters.Add(CommonTypesPrism.CustomerParam, SelectedCustomer);
            _dialogService.ShowDialog(CommonTypesPrism.AddEditCustomerDialog, parameters, async result =>
             {
                 var editableCustomer = result.Parameters.GetValue<Customer>(CommonTypesPrism.CustomerParam);
                 if (editableCustomer is null || editableCustomer.Equals(SelectedCustomer)) return;
                 var updateResult = await _customersRepository.Update(editableCustomer);
                 if (updateResult)
                 {
                    await UpdateData();
                 }
             });
        }

        private DelegateCommand _deleteCustomerCommand;
        public DelegateCommand DeleteCustomerCommand =>
           _deleteCustomerCommand ??= _deleteCustomerCommand = new(async()=> await ExecuteDeleteCustomerCommandAsync());
        async Task ExecuteDeleteCustomerCommandAsync()
        {
            if(SelectedCustomer != null){
                var deleteResult=await _customersRepository.Delete(SelectedCustomer);
                if (deleteResult)
                {
                    _eventAggregator.GetEvent<OnCustomerDeleted>().Publish(SelectedCustomer);
                    Customers.Remove(SelectedCustomer);
                }
                    
            }
        }

        private DelegateCommand _addNewCustomerCommand;
        public DelegateCommand AddNewCustomerCommand =>
           _addNewCustomerCommand ??= _addNewCustomerCommand = new(ExecuteAddNewCustomerCommand);
        void ExecuteAddNewCustomerCommand()
        {
            DialogParameters parameters = new();
            try
            {
                parameters.Add(CommonTypesPrism.CustomerParam, new Customer());
                _dialogService.ShowDialog(CommonTypesPrism.AddEditCustomerDialog, parameters, async result =>
                {
                    var editableCustomer = result.Parameters.GetValue<Customer>(CommonTypesPrism.CustomerParam);
                    if (editableCustomer is null) return;
                    var newCustomerId = await _customersRepository.Insert(editableCustomer);
                    if (newCustomerId > 0)
                    {
                        await UpdateData();
                    }
                });
            }
            catch (Exception ex){
                ShowNotificationDialog(DialogType.ErrorDialog, ex.Message);
            }
            
        }



        private async Task UpdateData(){
            Customers = new(await _customersRepository.Select());            
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Task.Run(async() =>{
                _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Visible);
                _customersRepository = _repositoryManager.GetRepository(RepositoryType.Customers) as IRepository<Customer>;
                await UpdateData();               
                _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Hidden);
            });
        }
    }
}
