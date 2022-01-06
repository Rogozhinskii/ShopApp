using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Authentication;
using ShopUI.Core;
using ShopUI.Core.MVVM;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// Модель регистрации пользователей
    /// </summary>
    public class RegisterUserDialogViewModel:DialogViewModel
    {
        public override string Title => "Регистрация пользователя";
        
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private SecureString _firstPassword;
        /// <summary>
        /// пароль пользователя
        /// </summary>
        public SecureString FirstPassword
        {
            get { return _firstPassword; }
            set { SetProperty(ref _firstPassword, value); }
        }

        private SecureString _secondPassword;
        /// <summary>
        /// второе поле под пароль для сравнения введенных паролей
        /// </summary>
        public SecureString SecondPassword
        {
            get { return _secondPassword; }
            set { SetProperty(ref _secondPassword, value); }
        }

        private DelegateCommand<object> _firstPasswordChangedCommand;
        public DelegateCommand<object> FirstPasswordChangedCommand =>
           _firstPasswordChangedCommand ??= _firstPasswordChangedCommand = new DelegateCommand<object>(ExecuteFirstPasswordChangedCommand);
        void ExecuteFirstPasswordChangedCommand(object obj)
        {
            FirstPassword = ((PasswordBox)obj).SecurePassword;
            FirstPassword.MakeReadOnly();
        }

        private DelegateCommand<object> _secondPasswordChangedCommand;
        public DelegateCommand<object> SecondPasswordChangedCommand =>
           _secondPasswordChangedCommand ??= _secondPasswordChangedCommand = new DelegateCommand<object>(ExecuteSecondPasswordChangedCommand);
        void ExecuteSecondPasswordChangedCommand(object obj)
        {
            SecondPassword = ((PasswordBox)obj).SecurePassword;
            SecondPassword.MakeReadOnly();
        }

        private DelegateCommand _registerNewUserCommand;
        public DelegateCommand RegisterNewUserCommand =>
           _registerNewUserCommand ??= _registerNewUserCommand = new(ExecuteRegisterNewUserCommand);
        void ExecuteRegisterNewUserCommand()
        {
            if (string.IsNullOrEmpty(UserName))
                ShowLoginFailedMessage();                
            if (FirstPassword.GetPasswordAsString() == SecondPassword.GetPasswordAsString())
            {
                var dialogResult = new DialogResult();
                dialogResult.Parameters.Add(CommonTypesPrism.UserName,UserName);
                dialogResult.Parameters.Add(CommonTypesPrism.SecurePassword,FirstPassword);
                RaiseRequestClose(dialogResult);
            }
            else{
                ShowPasswordsAreNotEqualMessage();
            }

        }

        private void ShowLoginFailedMessage()
        {
            Task.Run(async () =>
            {
                Message = "Логин не может быть пустым";
                await Task.Delay(2000);
                Message = string.Empty;
            });
        }
        private void ShowPasswordsAreNotEqualMessage()
        {
            Task.Run(async () =>
            {
                Message = "Введенные пароли не совпадают!";
                await Task.Delay(2000);
                Message = string.Empty;
            });
        }


    }
}
