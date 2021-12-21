﻿using Prism.Services.Dialogs;
using ShopUI.Core.Mvvm;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    internal class AuthenticationDialogViewModel:DialogViewModel
    {
        private readonly IDialogService _dialogService;

        public AuthenticationDialogViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
    }
}
