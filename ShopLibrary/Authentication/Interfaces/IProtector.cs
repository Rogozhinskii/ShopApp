using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool Register(string userName, string password);

        /// <summary>
        /// Возвращает true если пользователей найден в базе и пароль введен верно, иначе false
        /// </summary>       
        public bool LogIn(string userName, string password);
    }
}
