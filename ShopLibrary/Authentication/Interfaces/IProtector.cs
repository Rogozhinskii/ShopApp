using System.Security;

namespace ShopLibrary.Authentication.Interfaces
{
    /// <summary>
    /// Интерфейс для реализации защищеного доступа к приложению
    /// </summary>
    public interface IProtector
    {
        /// <summary>
        /// Производит регистрацию пользователей по введенным логину и пароль. Возвращает true если регистрация прошла успешно, 
        /// иначе false
        /// </summary>
        public Task<bool> Register(string userName, SecureString password);

        /// <summary>
        /// Возвращает true если пользователей найден в базе и пароль введен верно, иначе false
        /// </summary>       
        public Task<bool> LogIn(string userName, SecureString password);
    }
}
