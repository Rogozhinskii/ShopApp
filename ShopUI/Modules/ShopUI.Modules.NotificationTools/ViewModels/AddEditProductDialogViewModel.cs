using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Entityes;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// Диалоговое окна редактирвоания продукта
    /// </summary>
    internal class AddEditProductDialogViewModel:DialogViewModel
    {
        
        private Product _originalProduct;

        public string Email { get=>GetValue(_originalProduct?.Email); set=>SetValue(value); }
        public string Description { get=>GetValue(_originalProduct?.Description); set=>SetValue(value); }
        public int? ProductCode { get=> GetValue(_originalProduct?.ProductCode); set=>SetValue(value); }

        private DelegateCommand _saveChangesCommand;
        /// <summary>
        /// Выполняет сохранение изменений
        /// </summary>
        public DelegateCommand SaveChangesCommand =>
           _saveChangesCommand ??= _saveChangesCommand = new(ExecuteSaveChangesCommand);

        void ExecuteSaveChangesCommand()
        {
            SaveChanges(_originalProduct);
            DialogResult result=new DialogResult(ButtonResult.OK);           
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
            CancelChanges();
            var result=new DialogResult(ButtonResult.Cancel);
            RaiseRequestClose(result);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _originalProduct = parameters.GetValue<Product>(CommonTypesPrism.EditableRecord);
            var type = this.GetType();
            var props = type.GetProperties();
            foreach (var item in props)
            {
                RaisePropertyChanged(item.Name);
            }
        }

        public override void OnDialogClosed()
        {
            _originalProduct = null;           
        }



    }
}
