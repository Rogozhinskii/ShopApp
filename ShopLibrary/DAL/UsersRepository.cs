using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL
{
    internal class UsersRepository : IRepository<User>
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _conectionString;
        private IDbConnection _connection;

        public UsersRepository(DbProviderFactory factory,string connectionString)
        {
            _providerFactory = factory ?? throw new ArgumentNullException(nameof(factory));
            _conectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private void OpenConnection()
        {
            if(_conectionString == null ||_connection is null || _connection.State == ConnectionState.Closed)
            {
                _connection=_providerFactory.CreateConnection();
                if(_connection==null)
                    throw new ArgumentNullException(nameof(_connection));
                _connection.ConnectionString = _conectionString;
                _connection.Open();
            }
        }

        private IDbCommand GetCommand()
        {
            IDbCommand cmd =_providerFactory.CreateCommand();
            cmd.Connection = _connection;
            return cmd;
        }

        private void CloseConnection() 
        { 
            if(_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

       
        private IDataParameter GetParameter(string paramName,DbType dbType,object value)
        {
            IDataParameter parameter = _providerFactory.CreateParameter();
            parameter.ParameterName = $"@{paramName}";
            parameter.DbType = dbType;
            parameter.Value = value;
            return parameter;
        }

        public User Find(object value)
        {
            OpenConnection();
            string userName=value.ToString();
            User user=new();
            string sqlCommand = $"Select * from {TableNames.UserTable} where userName=@userName";
            using (var cmd = GetCommand())
            {
                cmd.CommandText = sqlCommand;
                cmd.Parameters.Add(GetParameter(nameof(userName), DbType.String, userName));
                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new()
                        {
                            Name = reader["userName"].ToString(),
                            Salt = reader["salt"].ToString(),
                            SaltedHashedPassword = reader["saltedHashedPassword"].ToString()
                        };
                       
                    }                    
                }
                catch (Exception){

                    throw;
                }
                finally{
                    CloseConnection();
                }

            }
            return user;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            OpenConnection();
            string sql = $"Insert Into {TableNames.UserTable} (userName,salt,saltedHashedPassword)" +
                        $"values(@userName,@salt,@saltedHashedPassword)";
            using(var cmd = GetCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.Add(GetParameter("userName", DbType.String, entity.Name));
                cmd.Parameters.Add(GetParameter("salt", DbType.String, entity.Salt));
                cmd.Parameters.Add(GetParameter("saltedHashedPassword", DbType.String, entity.SaltedHashedPassword));
                try{
                    var query = cmd.ExecuteNonQuery();
                    if (query != 0)
                        return true;
                }               
                catch (Exception ex){
                    CloseConnection();
                    throw new InvalidOperationException($"Can`t insert data to DB. Object",ex.InnerException);                   
                }
            }

            return false;
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        
        public bool InsertMany(IEnumerable<User> entities)
        {
            bool result = false;
            foreach (User item in entities)
            {
                result=Insert(item);
                if (!result)
                    throw new InvalidOperationException($"Can`t insert data to DB. Object {item.Name}");
            }
            return result;
        }
    }
}
