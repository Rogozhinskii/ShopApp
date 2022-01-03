using ShopLibrary.DAO.interfaces;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly DbProviderFactory _providerFactory;
        protected readonly string _conectionString;
        public string ConnectionString => _conectionString;
        public ConnectionState ConnectionState => _connection.State;

        public IDbDataAdapter DataAdapter => _dataAdapter;
        public IDbDataAdapter _dataAdapter;

        protected IDbConnection _connection;

        public Repository(DbProviderFactory factory, string connectionString)
        {
            _providerFactory = factory ?? throw new ArgumentNullException(nameof(factory));
            _conectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));  
            OpenConnection();
        }
                

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

        protected void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }


        protected IDataParameter GetParameter(string paramName, DbType dbType, object value)
        {
            IDataParameter parameter = _providerFactory.CreateParameter();
            parameter.ParameterName = $"@{paramName}";
            parameter.DbType = dbType;
            parameter.Value = value;
            return parameter;
        }


             

        public virtual T Find(object value)
        {
            throw new NotImplementedException();
        }
               
        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual bool Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool InsertMany(IEnumerable<T> entities)
        {
            bool result = false;
            foreach (T item in entities)
            {
                result = Insert(item);
                if (!result)
                    throw new InvalidOperationException($"Can`t insert data to DB. Object {item.GetType()}");
            }
            return result;
        }

        public virtual bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> Select()
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> Select(string fieldName, object value)
        {
            throw new NotImplementedException();
        }
    }
}
