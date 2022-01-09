using System.Security;

namespace ShopUI.Services.Interfaces
{
    /// <summary>
    /// Интерфейс для регистрации в входа из UI
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Производит регистрацию пользователей по введенным логину и пароль. Возвращает true если регистрация прошла успешно, 
        /// иначе false
        /// </summary>
        public Task<bool> RegisterAsync(string userName, SecureString password,CancellationToken token=default);

        /// <summary>
        /// Возвращает true если пользователей найден в базе и пароль введен верно, иначе false
        /// </summary>       
        public Task<bool> LogInAsync(string userName, SecureString password, CancellationToken token = default);
    }
}
