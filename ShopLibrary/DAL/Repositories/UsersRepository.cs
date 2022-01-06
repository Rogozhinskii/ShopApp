using ShopLibrary.Models;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class UsersRepository : Repository<User>
    {
        public UsersRepository(DbProviderFactory factory, string connectionString)
            : base(factory, connectionString) { }

        public override async Task<List<User>> Select()
        {
            OpenConnection();
            var users = new List<User>();
            string sql = $"Select * from {TableConstants.UserTable}";
            using(var cmd = GetCommand(sql))
            {
                try
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            User user = new()
                            {
                                Name = reader["userName"].ToString(),
                                Salt = reader["salt"].ToString(),
                                SaltedHashedPassword = reader["saltedHashedPassword"].ToString()
                            };
                            users.Add(user);
                        }
                    }
                   
                }
                catch (InvalidOperationException ex)
                {
                    CloseConnection();
                    throw new InvalidOperationException($"Can select user. {ex.Message}");
                }
                catch (Exception ex)
                {
                    CloseConnection();
                    throw new Exception($"Can select user.{ex.Message}");
                }
            }
            return users;
        }


        

        public override async Task<int> Insert(User entity)
        {
            OpenConnection();
            string sql = $"Insert Into {TableConstants.UserTable} (userName,salt,saltedHashedPassword)" +
                        $"values(@userName,@salt,@saltedHashedPassword)";
            using (var cmd = GetCommand(sql))
            {
                cmd.Parameters.Add(GetParameter("userName", DbType.String, entity.Name));
                cmd.Parameters.Add(GetParameter("salt", DbType.String, entity.Salt));
                cmd.Parameters.Add(GetParameter("saltedHashedPassword", DbType.String, entity.SaltedHashedPassword));
                try
                {
                    var newRecordId = await cmd.ExecuteNonQueryAsync();
                    if ((int)newRecordId != 0)
                        return (int)newRecordId;
                }
                catch (InvalidOperationException ex)
                {
                    CloseConnection();
                    throw new InvalidOperationException($"Can`t register user. Object {ex.Message}");
                }
                catch (Exception ex)
                {
                    CloseConnection();
                    throw new Exception($"Can`t register user. Object {ex.Message}");
                }
            }
            return 0;
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
