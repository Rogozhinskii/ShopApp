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
        private IRepository<Product> _productRepository;

        public event EventHandler sourseUpdateEvent;

        public CustomersViewModel(IRepositoryManager repositoryManager, IEventAggregator eventAggregator,IDialogService dialogService)
        {
            _repositoryManager = repositoryManager;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            Products = new ObservableCollection<Product>();
            
        }

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
            sourseUpdateEvent.Invoke(this, new EventArgs());
        }


        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { SetProperty(ref _selectedCustomer, value);}
        }


        private DelegateCommand<object> _editRecordCommand;

        public DelegateCommand<object> EditRecordCommand =>
           _editRecordCommand ??= _editRecordCommand = new DelegateCommand<object>(ExecuteEditRecordCommand);

        void ExecuteEditRecordCommand(object obj)
        {            
            var firstSelectedItem=(obj as ObservableCollection<object>).Cast<Product>().FirstOrDefault();
            if(firstSelectedItem != null)
            {
                DialogParameters parameters = new();
                parameters.Add(CommonTypesPrism.EditableRecord, firstSelectedItem);
                _dialogService.Show(CommonTypesPrism.AddEditDialog, parameters,async result =>
                {
                    var editableRecord = result.Parameters.GetValue<Product>(CommonTypesPrism.EditableRecord);                    
                    if (editableRecord is null || editableRecord.Equals(firstSelectedItem)) return;
                    var updateResult=await _productRepository.Update(editableRecord);
                    if (updateResult){
                        var index=Products.IndexOf(Products.Where(i => i.Id == editableRecord.Id).FirstOrDefault());
                        Products.RemoveAt(index);
                        Products.Insert(index, editableRecord);                        
                        sourseUpdateEvent.Invoke(this, new EventArgs());
                    }
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


        private DelegateCommand _addNewProduct;

        public DelegateCommand AddNewProduct =>
           _addNewProduct ??= _addNewProduct = new(ExecuteAddNewProduct);

        void ExecuteAddNewProduct()
        {
            var parameter = new DialogParameters();
            parameter.Add(CommonTypesPrism.EmailParam,SelectedCustomer.Email);
            _dialogService.Show(CommonTypesPrism.AddEditDialog,parameter, async result => 
            {
                var newRecord = result.Parameters.GetValue<Product>(CommonTypesPrism.EditableRecord);
                if (newRecord is null) return;
                bool insertResult=await _productRepository.Insert(newRecord);
                if(insertResult) Products.Add(newRecord);
                sourseUpdateEvent.Invoke(this, new EventArgs());
            });
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
