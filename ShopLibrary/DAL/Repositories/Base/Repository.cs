using ShopLibrary.DAO.interfaces;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        /// <summary>
        /// Для доступа к методам создания объектов работы с БД поставщиков, реализующих источник данных
        /// </summary>
        protected readonly DbProviderFactory _providerFactory;
        protected readonly string _conectionString;

        /// <summary>
        /// Возвращает строку подключения, на основе которой созданно хранилище
        /// </summary>
        public string ConnectionString => _conectionString;

        /// <summary>
        /// Возвращает состояние соединения
        /// </summary>
        public ConnectionState ConnectionState => _connection.State;
        /// <summary>
        /// Подключение в источнику данных
        /// </summary>
        protected IDbConnection _connection;

        public Repository(DbProviderFactory factory, string connectionString)
        {
            _providerFactory = factory ?? throw new ArgumentNullException(nameof(factory));
            _conectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));  
            OpenConnection();
        }
                
        /// <summary>
        /// Открывает подключение или создает подключение в зависимости от его состояния
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        protected void OpenConnection()
        {            
            if (_conectionString == null || _connection is null || _connection.State == ConnectionState.Closed)
            {
                _connection = _providerFactory.CreateConnection();
                if (_connection == null)
                    throw new ArgumentNullException(nameof(_connection));
                _connection.ConnectionString = _conectionString;
                _connection.Open();
            }
        }

        /// <summary>
        /// Возвращает экзампляр команды к источнику данных, параметризированный sql командой
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        protected DbCommand GetCommand(string sqlCommand)
        {
            var cmd = GetCommand();
            cmd.CommandText = sqlCommand;
            return (DbCommand)cmd;
        }

        protected IDbCommand GetCommand()
        {
            IDbCommand cmd = _providerFactory.CreateCommand();
            cmd.Connection = _connection;
            return cmd;
        }

        /// <summary>
        /// Закрывает соединение с источником данных
        /// </summary>
        protected void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        /// <summary>
        /// Возвращает параметр команды к источнику данных
        /// </summary>
        /// <param name="paramName">имя параметра</param>
        /// <param name="dbType">тип значения </param>
        /// <param name="value">значение</param>
        /// <returns></returns>
        protected IDataParameter GetParameter(string paramName, DbType dbType, object value)
        {
            IDataParameter parameter = _providerFactory.CreateParameter();
            parameter.ParameterName = $"@{paramName}";
            parameter.DbType = dbType;
            parameter.Value = value ?? (object)DBNull.Value;
            return parameter;
        }

        /// <summary>
        /// Выполняет множественную запись в источник данных
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public virtual async Task<bool> InsertMany(IEnumerable<T> entities)
        {
            bool result = false;
            foreach (T item in entities)
            {
                int newRecordId =await Insert(item);
                if (newRecordId == 0)
                    throw new InvalidOperationException($"Can`t insert data to DB. Object {item.GetType()}");
            }
            return result;
        }

        /// <summary>
        /// Выполняет обновление записи в источнике данных
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Выполняет удаление записи из источника данных 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<bool> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Выполняет выборку всех записей из источнкиа данных
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<List<T>> Select()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Выполняет вставку новой записи в источник данных
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<int> Insert(T entity)
        {
            throw new NotImplementedException();
        }

    }
}
