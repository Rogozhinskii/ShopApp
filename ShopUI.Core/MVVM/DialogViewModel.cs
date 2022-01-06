using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ShopUI.Core.MVVM
{
    public class DialogViewModel : BindableBase, IDialogAware
    {
        public virtual string Title => "";
        protected IDialogService _dialogService;

        public event Action<IDialogResult> RequestClose;
        public virtual void RaiseRequestClose(IDialogResult result)
        {
            RequestClose?.Invoke(result);
        }

        public virtual bool CanCloseDialog() =>
            true;

        public virtual void OnDialogClosed() { }

        public virtual void OnDialogOpened(IDialogParameters parameters) { }

        protected void ShowNotificationDialog(DialogType dialogType,string message)
        {
            var parameters = new DialogParameters();
            parameters.Add(CommonTypesPrism.DialogMessage, message);
            switch (dialogType)
            {
                case DialogType.NotificationDialog:
                    _dialogService.Show(CommonTypesPrism.NotificationDialog, parameters, null);
                    break;
                case DialogType.ErrorDialog:
                    _dialogService.Show(CommonTypesPrism.ErrorNotification, parameters, null);
                    break;
            }           
        }
    }
}
