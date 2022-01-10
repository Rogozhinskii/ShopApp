using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using ShopLibrary.Entityes;
using ShopLibrary.Interfaces;
using ShopUI.Core;
using ShopUI.Core.MVVM;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ShopUI.Modules.Customers.ViewModels
{
    public class ProductsViewModel: ViewModelBase
    {       
        
        private readonly IEventAggregator _eventAggregator;
        private readonly IRepository<Product> _productRepository;

        /// <summary>
        /// событие для обновления данных в гриде
        /// </summary>
        public event EventHandler sourseUpdateEvent;
        private Customer _productsOwner;
        public ProductsViewModel(IRepository<Product> productsRepository, IDialogService dialogService,IEventAggregator eventAggregator)
        {            
            _dialogService = dialogService;           
            _eventAggregator = eventAggregator;
            _productRepository = productsRepository;            
            _eventAggregator.GetEvent<OnSelectedCustomerChanged>().Subscribe(OnSelectedCustomerChangedAsync);            
            _eventAggregator.GetEvent<OnCustomerEdited>().Subscribe(OnCustomerEdited);
            _productsViewSourse = new CollectionViewSource();
        }

        private void OnCustomerEdited()
        {
            _productsViewSourse.View.Refresh();
        }
       

        public ICollectionView ProductsView => _productsViewSourse?.View;
        public CollectionViewSource _productsViewSourse;


        /// <summary>
        /// Обновление записей в гриде при смене покупателя
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private void OnSelectedCustomerChangedAsync(Customer obj)
        {
            if(obj is null) return;
            _productsOwner=obj;            
            Products=obj.Products.ToObservableCollection();            
        }

        private ObservableCollection<Product> _products;
        /// <summary>
        /// Коллекция продуктов
        /// </summary>
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set 
            {
                if(SetProperty(ref _products, value))
                {
                    _productsViewSourse.Source = value;
                    RaisePropertyChanged(nameof(ProductsView));
                }
            }
        }

        private DelegateCommand<object> _editRecordCommand;

        /// <summary>
        /// Вызывает окно редактирования записи продукта
        /// </summary>
        public DelegateCommand<object> EditRecordCommand =>
           _editRecordCommand ??= _editRecordCommand = new DelegateCommand<object>(ExecuteEditRecordCommand);

        void ExecuteEditRecordCommand(object obj)
        {
            var firstSelectedItem = (obj as ObservableCollection<object>).Cast<Product>().FirstOrDefault();
            if (firstSelectedItem != null)
            {
                DialogParameters parameters = new()
                {
                    { CommonTypesPrism.EditableRecord, firstSelectedItem }
                };               
                _dialogService.Show(CommonTypesPrism.AddEditDialog, parameters, async result =>
                {
                    if (result.Result != ButtonResult.OK) return;
                    await _productRepository.UpdateAsync(firstSelectedItem);
                    _productsViewSourse.View.Refresh();                   
                });


            }

        }

        #region DelegateCommand<object> DeleteRecordCommand - удаление продукта
        private DelegateCommand<object> _deleteRecordCommand;
        /// <summary>
        /// удаляет запись с продуктом
        /// </summary>
        public DelegateCommand<object> DeleteRecordCommand =>
           _deleteRecordCommand ??= _deleteRecordCommand = new DelegateCommand<object>(ExecuteDeleteRecordCommand);

        private void ExecuteDeleteRecordCommand(object obj)
        {
            _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Visible);
            var itemList = (obj as ObservableCollection<object>).Cast<Product>().ToList();
            var msg = $"Вы уверены, что хотите удалить {itemList.Count} записей?";
            var parameters = new DialogParameters{
                { CommonTypesPrism.DialogMessage, msg }
            };
            _dialogService.Show(CommonTypesPrism.NotificationDialog, parameters, async r =>
            {
                if (r.Result != ButtonResult.OK) return;
                await _productRepository.RemoveRangeAsync(itemList);
                Products.RemoveRange(itemList);
            });
            _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Hidden);
        }
        #endregion

        #region DelegateCommand AddNewProduct добавление нового продукта
        private DelegateCommand _addNewProduct;
        /// <summary>
        /// Добавляет новую запись с продуктом
        /// </summary>
        public DelegateCommand AddNewProduct =>
           _addNewProduct ??= _addNewProduct = new(ExecuteAddNewProduct);

        void ExecuteAddNewProduct()
        {
            var newProduct = new Product { Customer = _productsOwner};
            var parameter = new DialogParameters
            {
                { CommonTypesPrism.EditableRecord, newProduct }
            };
            _dialogService.Show(CommonTypesPrism.AddEditDialog, parameter, async result =>
            {
                if (result.Result != ButtonResult.OK) return;
                Products.Add(await _productRepository.AddAsync(newProduct));
            });

        }
        #endregion



    }
}
