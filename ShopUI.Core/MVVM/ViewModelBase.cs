﻿using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ShopUI.Core.MVVM
{
    public class ViewModelBase : BindableBase, INavigationAware, IConfirmNavigationRequest
    {
        protected IDialogService _dialogService;
        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        => true;

        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }

        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }

        protected void ShowNotificationDialog(DialogType dialogType, string message)
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
