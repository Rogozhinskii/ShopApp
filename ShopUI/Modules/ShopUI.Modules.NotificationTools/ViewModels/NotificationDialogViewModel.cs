using Prism.Services.Dialogs;
using ShopUI.Core;


namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// Модель диалогового окна уведомлений
    /// </summary>
    public class NotificationDialogViewModel: ErrorNotificationViewModel
    {
        
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>(CommonTypesPrism.DialogMessage);
        }
    }
}
