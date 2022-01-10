using EventAggregator.Core;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using ShopLibrary.Entityes;
using ShopLibrary.Interfaces;
using ShopUI.Core;
using ShopUI.Core.MVVM;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ShopUI.Modules.Products.ViewModels
{
    /// <summary>
    /// ViewModel для работы с покупателями
    /// </summary>
    public class CustomersViewModel : ViewModelBase
    {        
        private readonly IEventAggregator _eventAggregator;       
        private IRepository<Customer> _customersRepository;

        public CustomersViewModel(IRepository<Customer> customersRepository,IEventAggregator eventAggregator,IDialogService dialogService)
        {
            _customersRepository = customersRepository;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _customersViewSource = new CollectionViewSource()
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Customer.Surname), ListSortDirection.Ascending)
                }
            };
            _customersViewSource.Filter += OnCustomerFiltered;
        }


        /// <summary>
        /// Обрабатывает фильтрацию коллекцию покупателей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnCustomerFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Customer customer) || string.IsNullOrEmpty(FilterText)) return;
            if (customer.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) return;
            if (customer.Surname.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) return;
            e.Accepted = false;
        }

        public ICollectionView CostomersView => _customersViewSource?.View;
        public CollectionViewSource _customersViewSource;

        #region ObservableCollection<Customers>: коллекция покупателей

        private ObservableCollection<Customer> _customers;
        /// <summary>
        /// коллекция покупателей
        /// </summary>
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                if (SetProperty(ref _customers, value))
                {
                    _customersViewSource.Source = value;                    
                    RaisePropertyChanged(nameof(CostomersView));
                }
            }
        }

        #endregion

        #region SelectedCustomer-выбранный покупатель
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {                
                if (SetProperty(ref _selectedCustomer, value))
                {
                    _eventAggregator.GetEvent<OnSelectedCustomerChanged>().Publish(SelectedCustomer);
                    _deleteCustomerCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region DelegateCommand EditCustomerCommand -вызов диалогового окна редактирования и сохранение изменений
        private DelegateCommand _editCustomerCommand;
        /// <summary>
        /// Вызывает окна редактирования покупателя
        /// </summary>
        public DelegateCommand EditCustomerCommand =>
           _editCustomerCommand ??= _editCustomerCommand = new(ExecuteEditCustomerCommand);
        private void ExecuteEditCustomerCommand()
        {
            if (SelectedCustomer == null) return;
            DialogParameters parameters = new();           
            parameters.Add(CommonTypesPrism.CustomerParam, SelectedCustomer);
            _dialogService.ShowDialog(CommonTypesPrism.AddEditCustomerDialog, parameters, async result =>
            {
                if (result.Result != ButtonResult.OK) return;                           
                await _customersRepository.UpdateAsync(SelectedCustomer);
                _customersViewSource.View.Refresh();
                _eventAggregator.GetEvent<OnCustomerEdited>().Publish();

            });
            

        }
        #endregion

        #region DelegateCommand<Customer> DeleteCustomerCommand команда удаления выбранного покупателя
        private DelegateCommand<Customer> _deleteCustomerCommand;
        /// <summary>
        /// Удаляет выбранного покупателя
        /// </summary>
        public DelegateCommand<Customer> DeleteCustomerCommand =>
           _deleteCustomerCommand ??= _deleteCustomerCommand = new DelegateCommand<Customer>(ExecuteDeleteCustomerCommand, CanExecuteDeleteCustomerCommand);

        private bool CanExecuteDeleteCustomerCommand(Customer arg) => arg is not null || SelectedCustomer is not null;

        private void ExecuteDeleteCustomerCommand(Customer obj)
        {
            var customerForDelete = obj ?? SelectedCustomer;
            var msg = $"Вы уверены, что хотите удалить покупателя: {customerForDelete.Surname} {customerForDelete.Name} {customerForDelete.Patronymic}?";
            var parameters = new DialogParameters();
            parameters.Add(CommonTypesPrism.DialogMessage, msg);
            _dialogService.Show(CommonTypesPrism.NotificationDialog, parameters, async r =>
            {
                if (r.Result != ButtonResult.OK) return;
                await _customersRepository.RemoveAsync(customerForDelete);
                Customers.Remove(customerForDelete);
                if (ReferenceEquals(customerForDelete, SelectedCustomer))
                    SelectedCustomer = null;
            });

        }
        #endregion

        #region DelegateCommand AddNewCustomerCommand -команда создания нового покупателя
        private DelegateCommand _addNewCustomerCommand;
        /// <summary>
        /// добавляет нового покупателя
        /// </summary>
        public DelegateCommand AddNewCustomerCommand =>
           _addNewCustomerCommand ??= _addNewCustomerCommand = new(ExecuteAddNewCustomerCommand);
        void ExecuteAddNewCustomerCommand()
        {
            DialogParameters parameters = new();
            try
            {
                var newCustomer = new Customer();
                parameters.Add(CommonTypesPrism.CustomerParam, newCustomer);
                _dialogService.ShowDialog(CommonTypesPrism.AddEditCustomerDialog, parameters, async result =>
                {
                    if (result.Result == ButtonResult.Cancel || result.Result == ButtonResult.None) return;
                    Customers.Add(await _customersRepository.AddAsync(newCustomer));
                    SelectedCustomer = newCustomer;
                });
            }
            catch (Exception ex)
            {
                ShowNotificationDialog(DialogType.ErrorDialog, ex.Message);
            }

        }
        #endregion

        #region FilterText - поле для фильтрации
        private string _filterText;
        /// <summary>
        /// Поле для фильтрации
        /// </summary>
        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); _customersViewSource.View.Refresh(); }
        }

        #endregion

        /// <summary>
        /// Обновляет коллекию покупателей
        /// </summary>
        /// <returns></returns>
        private async Task UpdateData(){
            Customers = (await _customersRepository.Items.ToArrayAsync()).ToObservableCollection();                     
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Visible);
            await UpdateData();
            _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Hidden);            
        }
    }
}
