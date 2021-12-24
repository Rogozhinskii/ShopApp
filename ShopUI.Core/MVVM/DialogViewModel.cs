using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ShopUI.Core.MVVM
{
    public class DialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "";

        public event Action<IDialogResult> RequestClose;
        public virtual void RaiseRequestClose(IDialogResult result)
        {
            RequestClose?.Invoke(result);
        }

        public virtual bool CanCloseDialog() =>
            true;

        public virtual void OnDialogClosed() { }

        public virtual void OnDialogOpened(IDialogParameters parameters) { }
    }
}
