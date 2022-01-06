using Prism.Services.Dialogs;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    public class NotificationDialogViewModel: ErrorNotificationViewModel
    {
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>(CommonTypesPrism.DialogMessage);
        }
    }
}
