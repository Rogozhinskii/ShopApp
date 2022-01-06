using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Models;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// Диалоговое окна редактирвоания продукта
    /// </summary>
    internal class AddEditProductDialogViewModel:DialogViewModel
    {
        /// <summary>
        /// исходный экземпляр продукта
        /// </summary>
        private Product _originalRecord;
        private Product _product;
        /// <summary>
        /// Редактируемый объект продукта
        /// </summary>
        public Product Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }       

        private DelegateCommand _saveChangesCommand;
        /// <summary>
        /// Выполняет сохранение изменений
        /// </summary>
        public DelegateCommand SaveChangesCommand =>
           _saveChangesCommand ??= _saveChangesCommand = new(ExecuteSaveChangesCommand);

        void ExecuteSaveChangesCommand()
        {
            DialogResult result=new DialogResult();
            result.Parameters.Add(CommonTypesPrism.EditableRecord, Product);
            RaiseRequestClose(result);
        }

        private DelegateCommand _cancelCommand;
        /// <summary>
        /// Отменяет изменения и закрывает диалоговое окно
        /// </summary>
        public DelegateCommand CancelCommand =>
           _cancelCommand ??= _cancelCommand = new(ExecuteCancelCommand);

        void ExecuteCancelCommand()
        {
            var result=new DialogResult();
            result.Parameters.Add(CommonTypesPrism.EditableRecord, _originalRecord);
            RaiseRequestClose(result);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            parameters.TryGetValue(CommonTypesPrism.EditableRecord,out _originalRecord);
            parameters.TryGetValue(CommonTypesPrism.EmailParam,out string email);
            if (_originalRecord != null){
                Product = (Product)_originalRecord.Clone();
            }
            else{
                if (email != null)
                    Product = new Product { Email = email };
                else
                    Product = new Product();
            }

        }

        public override void OnDialogClosed()
        {
            Product = null;           
        }



    }
}
