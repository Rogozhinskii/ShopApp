using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Models;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    public class AddEditCustomerDialogViewModel:DialogViewModel
    {
        private Customer _originalCustomer;
        private Customer _currentCustomer;
        public Customer CurrentCustomer
        {
            get { return _currentCustomer; }
            set { SetProperty(ref _currentCustomer, value); }
        }

        private DelegateCommand _saveChangesCommand;
        public DelegateCommand SaveChangesCommand =>
           _saveChangesCommand ??= _saveChangesCommand = new(ExecuteSaveChangesCommand);
        void ExecuteSaveChangesCommand()
        {
            DialogResult dialogResult = new();
            dialogResult.Parameters.Add(CommonTypesPrism.CustomerParam,CurrentCustomer);
            RaiseRequestClose(dialogResult);
        }

        private DelegateCommand _cancelCommand;
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
                CurrentCustomer = (Customer)_originalCustomer.Clone();
            }
        }


    }
}
