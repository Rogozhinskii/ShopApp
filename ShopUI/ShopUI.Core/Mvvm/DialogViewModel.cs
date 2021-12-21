using Prism.Services.Dialogs;
using System;

namespace ShopUI.Core.Mvvm
{
    public class DialogViewModel : ViewModelBase, IDialogAware
    {
        public string Title => "";

        public event Action<IDialogResult> RequestClose;

        public virtual bool CanCloseDialog() =>
            true;

        public virtual void OnDialogClosed() {}

        public virtual void OnDialogOpened(IDialogParameters parameters) { }
    }
}
