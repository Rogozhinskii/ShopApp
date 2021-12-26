using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Authentication.Interfaces;
using ShopUI.Core;
using ShopUI.Core.MVVM;
using ShopUI.Services.Interfaces;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    internal class AuthenticationDialogViewModel:DialogViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IAuthenticationService _protector;
        private static int _currentLoginAttemptCount = 0;
        private static int _maxLoginAttemptCount=2;


        public AuthenticationDialogViewModel(IDialogService dialogService, IAuthenticationService protector)
        {
            _dialogService = dialogService;
            _protector = protector;
        }


        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private SecureString _password;

        public SecureString Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }


        private DelegateCommand _logInCommand;

        public DelegateCommand LogInCommand =>
           _logInCommand ??= _logInCommand = new(ExecuteLogInCommand);

        async void ExecuteLogInCommand()
        {
            IDialogResult result = new DialogResult();
            bool isSignedIf = false;
            if (!isSignedIf)
            {
                if (_currentLoginAttemptCount < _maxLoginAttemptCount)
                {
                    if (UserName is null || Password is null) return;
                    isSignedIf = await _protector.LogIn(UserName, Password);
                    if (!isSignedIf)
                    {
                        UserName = string.Empty;
                        Password = null;
                        ShowLoginFailedMessage();
                        _currentLoginAttemptCount++;
                        return;
                    }
                }

            }
            result.Parameters.Add(CommonTypesPrism.logInResult, isSignedIf);
            RaiseRequestClose(result);           
        }


       

        private DelegateCommand _registerUserCommand;

        public DelegateCommand RegisterUserCommand =>
           _registerUserCommand ??= _registerUserCommand = new(ExecuteRegisterUserCommand);

        void ExecuteRegisterUserCommand()
        {

        }

        private DelegateCommand<object> _passwordChangedCommand;

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
                ErrorMessage = "Не верный логин или пароль!";
                await Task.Delay(5000);
                ErrorMessage = string.Empty;
            });
        }

        public override bool CanCloseDialog() => 
            true;




    }
}
