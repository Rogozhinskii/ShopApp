using ShopLibrary.Models;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class UsersRepository : Repository<User>
    {
        public UsersRepository(DbProviderFactory factory, string connectionString)
            : base(factory, connectionString) { }

       

        public override User Find(object value)
        {
            User user = new();
            if (value != null){
                OpenConnection();
                string userName = value.ToString();
                string sqlCommand = $"Select * from {TableNames.UserTable} where userName=@userName";
                using var cmd = GetCommand();
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
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    CloseConnection();
                }
            }
            
            return user;
        }

        

        public override bool Insert(User entity)
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

        //public override bool InsertMany(IEnumerable<User> entities)
        //{
        //    bool result = false;
        //    foreach (User item in entities)
        //    {
        //        result=Insert(item);
        //        if (!result)
        //            throw new InvalidOperationException($"Can`t insert data to DB. Object {item.Name}");
        //    }
        //    return result;
        //}
    }
}
