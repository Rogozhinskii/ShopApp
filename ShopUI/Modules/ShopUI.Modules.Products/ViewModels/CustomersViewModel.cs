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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ShopUI.Modules.Products.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;
        private IRepository<Product> _productRepository;

        public event EventHandler sourseUpdateEvent;

        public CustomersViewModel(IRepositoryManager repositoryManager, IEventAggregator eventAggregator,IDialogService dialogService)
        {
            _repositoryManager = repositoryManager;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            Products = new ObservableCollection<Product>();
            
        }

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    RaisePropertyChanged(nameof(Products));
        //}

        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); 
}
        }
        
        private DelegateCommand _selectionChanged;

        public DelegateCommand SelectionChanged =>
           _selectionChanged ??= _selectionChanged = new(ExecuteSelectionChanged);

        async void ExecuteSelectionChanged()
        {
            Products.Clear();            
            var uploadedProducts=await _productRepository.Select("email", SelectedCustomer.Email);
            Products.AddRange(uploadedProducts); 
            RaisePropertyChanged(nameof(Products));
        }


        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { SetProperty(ref _selectedCustomer, value);}
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value);}
        }


        private DelegateCommand<object> _editRecordCommand;

        public DelegateCommand<object> EditRecordCommand =>
           _editRecordCommand ??= _editRecordCommand = new DelegateCommand<object>(async (obj)=>await ExecuteEditRecordCommand(obj));

        async Task ExecuteEditRecordCommand(object obj)
        {            
            var firstSelectedItem=(obj as ObservableCollection<object>).Cast<Product>().FirstOrDefault();
            if(firstSelectedItem != null)
            {
                DialogParameters parameters = new();
                parameters.Add(CommonTypesPrism.recordForEdit, firstSelectedItem);
                _dialogService.Show(CommonTypesPrism.AddEditDialog, parameters,async result =>
                {
                    await _productRepository.Update(firstSelectedItem);
                    sourseUpdateEvent.Invoke(this, new EventArgs());
                });
                

            }
            
        }



        private DelegateCommand<object> _deleteRecordCommand;

        public DelegateCommand<object> DeleteRecordCommand =>
           _deleteRecordCommand ??= _deleteRecordCommand = new DelegateCommand<object>(async (obj)=>await ExecuteDeleteRecordCommand(obj));

        private async Task ExecuteDeleteRecordCommand(object obj)
        {
            _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Visible);
            var itemList = (obj as ObservableCollection<object>).Cast<Product>().ToList();
            foreach (var item in itemList){
                var result = await _productRepository.Delete(item);
                if (result) Products.Remove(item);
            }            
            _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Hidden);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Task.Run(async() =>{
                _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Visible);
                var repo=_repositoryManager.GetRepository(RepositoryType.Customers) as IRepository<Customer>; 
                _productRepository= _repositoryManager.GetRepository(RepositoryType.Products) as IRepository<Product>;
                Customers = new ObservableCollection<Customer>(await repo.Select());                
                _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Hidden);

            });
        }
    }
}
