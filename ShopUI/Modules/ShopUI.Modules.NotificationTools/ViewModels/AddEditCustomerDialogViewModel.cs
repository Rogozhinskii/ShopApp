using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Entityes;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// диалоговое окна редактирования покупателя
    /// </summary>
    public class AddEditCustomerDialogViewModel:DialogViewModel
    {

        private Customer _originalCustomer;

        public string Name { get=>GetValue(_originalCustomer?.Name); set=>SetValue(value); }
        public string Surname { get=>GetValue(_originalCustomer?.Surname); set=> SetValue(value); }
        public string Patronymic { get=>GetValue(_originalCustomer?.Patronymic); set=> SetValue(value); }
        public string Email { get=>GetValue(_originalCustomer?.Email); set=> SetValue(value); }
        public string PhoneNumber { get=>GetValue(_originalCustomer?.PhoneNumber); set=> SetValue(value); }

        
        #region


        private DelegateCommand _saveChangesCommand;
        /// <summary>
        /// Выполняет сохранение изменений
        /// </summary>
        public DelegateCommand SaveChangesCommand =>
           _saveChangesCommand ??= _saveChangesCommand = new(ExecuteSaveChangesCommand);
        void ExecuteSaveChangesCommand()
        {
            SaveChanges(_originalCustomer);
            DialogResult dialogResult = new(ButtonResult.OK);            
            RaiseRequestClose(dialogResult);
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
            var result = new DialogResult(ButtonResult.Cancel);
            RaiseRequestClose(result);           
        }
        #endregion
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _originalCustomer = parameters.GetValue<Customer>(CommonTypesPrism.CustomerParam); 
            var type=this.GetType();
            var props=type.GetProperties();
            foreach (var item in props)
            {
                RaisePropertyChanged(item.Name);
            }
        }

    }
}
