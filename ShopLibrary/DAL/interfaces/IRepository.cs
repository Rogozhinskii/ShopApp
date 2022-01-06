using System.Data;

namespace ShopLibrary.DAO.interfaces
{
    public interface IRepository<T> 
    {
        /// <summary>
        /// Возвращает строку подключения, на основе которой созданно хранилище
        /// </summary>
        public string ConnectionString { get;}

        /// <summary>
        /// Возвращает состояние соединения
        /// </summary>
        public ConnectionState ConnectionState { get; }

        /// <summary>
        /// Выполняет выборку всех записей из источнкиа данных
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<T>> Select();
        /// <summary>
        /// Выполняет обновление записи в источнике данных
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> Update(T entity);
        /// <summary>
        /// Выполняет удаление записи из источника данных 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> Delete(T entity);

        /// <summary>
        /// Выполняет вставку новой записи в источник данных
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<int> Insert(T entity);
        /// <summary>
        /// Выполняет множественную запись в источник данных
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Task<bool> InsertMany(IEnumerable<T> entities);
        /// <summary>
        /// Выполняет множественное обновление записей в источнике данных
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task<bool> UpdateMany(IEnumerable<T> entities);
        
    }
}
