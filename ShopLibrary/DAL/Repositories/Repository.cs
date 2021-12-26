using ShopLibrary.DAO.interfaces;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly DbProviderFactory _providerFactory;
        protected readonly string _conectionString;
        protected IDbConnection _connection;

        public Repository(DbProviderFactory factory, string connectionString)
        {
            _providerFactory = factory ?? throw new ArgumentNullException(nameof(factory));
            _conectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
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



        public virtual bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual T Find(object value)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetAll()
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
            throw new NotImplementedException();
        }

        public virtual bool Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
