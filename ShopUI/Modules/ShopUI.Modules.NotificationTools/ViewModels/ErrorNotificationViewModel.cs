using Prism.Commands;
using Prism.Services.Dialogs;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    public class ErrorNotificationViewModel : DialogViewModel
    {
        public ErrorNotificationViewModel() { }
        
        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private DelegateCommand _closeDialogCommand;

        public DelegateCommand CloseDialogCommand =>
           _closeDialogCommand ??= _closeDialogCommand = new(ExecuteCloseDialogCommand);

        void ExecuteCloseDialogCommand()
        {
            DialogResult result =new(ButtonResult.OK);
            RaiseRequestClose(result);

        }



        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>(CommonTypesPrism.message);
        }
    }

    
    
}
