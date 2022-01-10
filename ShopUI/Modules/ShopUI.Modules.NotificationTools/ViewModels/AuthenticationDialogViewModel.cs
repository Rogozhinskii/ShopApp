using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using ShopUI.Core;
using ShopUI.Core.MVVM;
using ShopUI.Services.Interfaces;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// Диалоговое окна входа в приложение
    /// </summary>
    internal class AuthenticationDialogViewModel:DialogViewModel
    {        
        private readonly IAuthenticationService _autenticationService;
        private readonly IEventAggregator _eventAggregator;       
        
        /// <summary>
        /// Счетчик попыток входа
        /// </summary>
        private static int _currentLoginAttemptCount = 0;
        /// <summary>
        /// Максимальное количество попыток входа до закрытия приложения
        /// </summary>
        private static int _maxLoginAttemptCount=2;


        public AuthenticationDialogViewModel(IAuthenticationService autenticationService,IEventAggregator eventAggregator,IDialogService dialogService)
        {
            _autenticationService = autenticationService;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
        }

        #region Свойста

        #region Имя пользователя
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        #endregion

        #region Пароль
        private SecureString _password;
        public SecureString Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        #endregion


        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        #endregion


        private DelegateCommand _logInCommand;
        /// <summary>
        /// реализует вход в приложение
        /// </summary>
        public DelegateCommand LogInCommand =>
           _logInCommand ??= _logInCommand = new(async () => await ExecuteLogInCommand());
           

        async Task ExecuteLogInCommand()
        {            
            bool isSignedIf = false;
            if (!isSignedIf)
            { 
                if (_currentLoginAttemptCount < _maxLoginAttemptCount)
                {
                    try
                    {
                        _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Visible);                        
                        if (UserName is null || Password is null) { _currentLoginAttemptCount++; return; };
                        isSignedIf = await _autenticationService.LogInAsync(UserName, Password);
                        _eventAggregator.GetEvent<OnLongOperationEvent>().Publish(Visibility.Hidden);
                        if (!isSignedIf)
                        {
                            UserName = string.Empty;
                            Password = null;
                            ShowLoginFailedMessage();
                            _currentLoginAttemptCount++;
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowNotificationDialog(DialogType.ErrorDialog, ex.Message);                        
                    }
                                  
                }
            }
            DialogResult result = new DialogResult(ButtonResult.OK);
            result.Parameters.Add(CommonTypesPrism.logInResult, isSignedIf);            
            RaiseRequestClose(result);           
        }       

        private DelegateCommand _registerUserCommand;
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        public DelegateCommand RegisterUserCommand =>
           _registerUserCommand ??= _registerUserCommand = new(ExecuteRegisterUserCommand);
        void ExecuteRegisterUserCommand()
        {
            try{
                _dialogService.Show(CommonTypesPrism.RegisterUserDialog, null, async r =>
                {
                    r.Parameters.TryGetValue(CommonTypesPrism.UserName, out string userName);
                    r.Parameters.TryGetValue(CommonTypesPrism.SecurePassword, out SecureString password);
                    if(userName != null && password != null)
                    {
                        var registerResult = await _autenticationService.RegisterAsync(userName, password);
                        if (registerResult)
                            ShowNotificationDialog(DialogType.NotificationDialog,"Пользователь зарегистрирован!");
                    }
                });
            }
            catch (Exception ex)
            {
                ShowNotificationDialog(DialogType.ErrorDialog,ex.Message);
            }
           
        }

        private DelegateCommand<object> _passwordChangedCommand;
        /// <summary>
        /// Вызывается при изменении поля с паролем
        /// </summary>
        public DelegateCommand<object> PasswordChangedCommand =>
           _passwordChangedCommand ??= _passwordChangedCommand = new DelegateCommand<object>(ExecutePasswordChangedCommand);
        void ExecutePasswordChangedCommand(object obj)
        {            
            Password = ((PasswordBox)obj).SecurePassword;
            Password.MakeReadOnly();
        }

        private void ShowLoginFailedMessage()
        {
            Task.Run(async () =>
            {
                Message = "Не верный логин или пароль!";
                await Task.Delay(5000);
                Message = string.Empty;
            });
        }

       

    }
}
