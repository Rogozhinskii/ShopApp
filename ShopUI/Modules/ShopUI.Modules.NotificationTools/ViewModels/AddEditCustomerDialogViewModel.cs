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
        /// <summary>
        /// исходный экземпляр покупателя
        /// </summary>
        private Customer _originalCustomer;
        private Customer _currentCustomer;

        /// <summary>
        /// Текущий редактируемый покупатель
        /// </summary>
        public Customer CurrentCustomer
        {
            get { return _currentCustomer; }
            set { SetProperty(ref _currentCustomer, value); }
        }

        private DelegateCommand _saveChangesCommand;
        /// <summary>
        /// Выполняет сохранение изменений
        /// </summary>
        public DelegateCommand SaveChangesCommand =>
           _saveChangesCommand ??= _saveChangesCommand = new(ExecuteSaveChangesCommand);
        void ExecuteSaveChangesCommand()
        {
            DialogResult dialogResult = new();
            dialogResult.Parameters.Add(CommonTypesPrism.CustomerParam,CurrentCustomer);
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
            var result = new DialogResult();
            result.Parameters.Add(CommonTypesPrism.CustomerParam, _originalCustomer);
            RaiseRequestClose(result);
           
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            parameters.TryGetValue(CommonTypesPrism.CustomerParam, out _originalCustomer);
            if (_originalCustomer != null)
            {
                //CurrentCustomer = (Customer)_originalCustomer.Clone();
            }
        }


    }
}
