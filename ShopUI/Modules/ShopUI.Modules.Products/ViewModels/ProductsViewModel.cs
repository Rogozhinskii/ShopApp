using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using ShopLibrary.Entityes;
using ShopLibrary.Interfaces;
using ShopUI.Core;
using ShopUI.Core.MVVM;
using ShopUI.Services;
using ShopUI.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopUI.Modules.Customers.ViewModels
{
    public class ProductsViewModel: ViewModelBase
    {
        private readonly IRepositoryManager _repositoryManager;       
        private readonly IEventAggregator _eventAggregator;
        private IRepository<Product> _productRepository;

        /// <summary>
        /// событие для обновления данных в гриде
        /// </summary>
        public event EventHandler sourseUpdateEvent;
        private Customer _productsOwner;
        public ProductsViewModel(IRepositoryManager repositoryManager, IDialogService dialogService,IEventAggregator eventAggregator)
        {
            _repositoryManager = repositoryManager;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _productRepository = _repositoryManager.GetRepository(RepositoryType.Products) as IRepository<Product>;
            Products = new ObservableCollection<Product>();
            _eventAggregator.GetEvent<OnSelectedCustomerChanged>().Subscribe(async (obj) =>await OnSelectedCustomerChangedAsync(obj));
            _eventAggregator.GetEvent<OnCustomerDeleted>().Subscribe(async (obj) => await OnCustomerDeleted(obj));
            _eventAggregator.GetEvent<OnCustomerEdited>().Subscribe(async (obj) => await OnCustomerEdited(obj));
        }

        private async Task OnCustomerEdited(Customer obj)
        {
            if (obj == null) return;
            try
            {
                Products.ToList().ForEach(p =>p.Email = obj.Email);
                //var updateResult = await _productRepository.UpdateMany(Products); //обновляем email
                sourseUpdateEvent.Invoke(this, new EventArgs());
            }
            catch (Exception ex){
                ShowNotificationDialog(DialogType.ErrorDialog, ex.Message);
                
            }
        }

        /// <summary>
        /// при удалении покупателя удаляются все его хранящиеся покупки
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private async Task OnCustomerDeleted(Customer obj)
        {
            if(obj == null) return;
            var parameters = new DialogParameters();
            try
            {
                //var deleteResult = await _productRepository.Delete(x => x.Email == obj.Email);
                //if (deleteResult)
                //{
                //    ShowNotificationDialog(DialogType.NotificationDialog, "Записи удалены");
                //}
            }
            catch (Exception ex)
            {
                ShowNotificationDialog(DialogType.ErrorDialog, ex.Message);
            }
           

        }
        /// <summary>
        /// Обновление записей в гриде при смене покупателя
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private async Task OnSelectedCustomerChangedAsync(Customer obj)
        {
            _productsOwner=obj;
            Products.Clear();
            //var uploadedProducts = await _productRepository.Select(x=>x.Email==_productsOwner.Email);
            //Products.AddRange(uploadedProducts);
            sourseUpdateEvent.Invoke(this, new EventArgs());
        }

        private ObservableCollection<Product> _products;
        /// <summary>
        /// Коллекция продуктов
        /// </summary>
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set {SetProperty(ref _products, value);}
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
                DialogParameters parameters = new();
                parameters.Add(CommonTypesPrism.EditableRecord, firstSelectedItem);
                _dialogService.Show(CommonTypesPrism.AddEditDialog, parameters, async result =>
                {
                    var editableRecord = result.Parameters.GetValue<Product>(CommonTypesPrism.EditableRecord);
                    if (editableRecord is null || editableRecord.Equals(firstSelectedItem)) return;
                    //var updateResult = await _productRepository.Update(editableRecord);
                    //if (updateResult)
                    //{
                    //    var index = Products.IndexOf(Products.Where(i => i.Id == editableRecord.Id).FirstOrDefault());
                    //    Products.RemoveAt(index);
                    //    Products.Insert(index, editableRecord);
                    //    sourseUpdateEvent.Invoke(this, new EventArgs());
                    //}
                });


            }

        }
        private DelegateCommand<object> _deleteRecordCommand;
        /// <summary>
        /// удаляет запись с продуктом
        /// </summary>
        public DelegateCommand<object> DeleteRecordCommand =>
           _deleteRecordCommand ??= _deleteRecordCommand = new DelegateCommand<object>(async (obj) => await ExecuteDeleteRecordCommand(obj));

        private async Task ExecuteDeleteRecordCommand(object obj)
        {
            _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Visible);
            var itemList = (obj as ObservableCollection<object>).Cast<Product>().ToList();
            foreach (var item in itemList)
            {
                //var result = await _productRepository.Delete(item);
                //if (result) Products.Remove(item);
            }
            _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Hidden);
        }


        private DelegateCommand _addNewProduct;
        /// <summary>
        /// Добавляет новую запись с продуктом
        /// </summary>
        public DelegateCommand AddNewProduct =>
           _addNewProduct ??= _addNewProduct = new(ExecuteAddNewProduct);

        void ExecuteAddNewProduct()
        {
            var parameter = new DialogParameters();
            parameter.Add(CommonTypesPrism.EmailParam, _productsOwner.Email);

            _dialogService.Show(CommonTypesPrism.AddEditDialog, parameter, async result =>
            {
                var newRecord = result.Parameters.GetValue<Product>(CommonTypesPrism.EditableRecord);
                if (newRecord is null) return;
                //int newRecordId = await _productRepository.Insert(newRecord);
                //newRecord.Id= newRecordId;
                //if (newRecordId>0) Products.Add(newRecord);
                sourseUpdateEvent.Invoke(this, new EventArgs());
            });

        }

    }
}
