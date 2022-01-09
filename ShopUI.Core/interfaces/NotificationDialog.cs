using Prism.Services.Dialogs;

namespace ShopUI.Core.interfaces
{
    public interface NotificationDialog
    {
        /// <summary>
        /// Показывает соответвующее DialogType диалоговое окно, принимает отображаемое сообщение
        /// </summary>
        /// <param name="dialogType"></param>
        /// <param name="message"></param>
        public void ShowNotificationDialog(DialogType dialogType, string message);
    }
}
