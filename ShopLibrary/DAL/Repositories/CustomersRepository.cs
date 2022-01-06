using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class CustomersRepository : Repository<Customer>
    {
        public CustomersRepository(DbProviderFactory factory, string connectionString) 
            : base(factory, connectionString)
        {
        }

       

        public override async Task<List<Customer>> Select()
        {
            OpenConnection();
            List<Customer> retval = new();
            string sql = $"Select * from {TableConstants.CustomersTable}";
            try
            {
                using var cmd = GetCommand(sql);               
                using var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    retval.Add(new Customer
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        Surname = reader["surname"].ToString(),
                        Patronymic = reader["patronymic"].ToString(),
                        Email = reader["email"].ToString(),
                        PhoneNumber = reader["phoneNumber"].ToString()
                    });
                }
            }
            catch (InvalidOperationException){
                CloseConnection();
                throw;
            }
            catch(Exception){
                CloseConnection();
                throw;
            }
            finally{
                CloseConnection();
            }
            
            return retval;
        }
        public override async Task<int> Insert(Customer entity)
        {
            OpenConnection();
            if(entity == null)
                 throw new ArgumentNullException($"can`t add an empty link {nameof(entity)}");
            string sql = $"Insert into {TableConstants.CustomersTable} (surname,name,patronymic,phoneNumber,email)" +
                                                                   $"values(@surname,@name,@patronymic,@phoneNumber,@email);select @@IDENTITY;";
            using(var cmd = GetCommand(sql)){               
                cmd.Parameters.Add(GetParameter("surname", DbType.String, entity.Surname));
                cmd.Parameters.Add(GetParameter("name", DbType.String, entity.Name));
                cmd.Parameters.Add(GetParameter("patronymic", DbType.String, entity.Patronymic));
                cmd.Parameters.Add(GetParameter("phoneNumber", DbType.String, entity.PhoneNumber ?? string.Empty));
                cmd.Parameters.Add(GetParameter("email", DbType.String, entity.Email));                
                try{
                    var newRecordId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    if (newRecordId != 0)
                        return newRecordId;
                }
                catch (InvalidOperationException){
                    CloseConnection();
                   throw;                 
                }
                catch (Exception){
                    CloseConnection();
                    throw;
                }

            }
            return 0;
        }

        public override async Task<bool> Delete(Customer entity)
        {
            OpenConnection();
            string sql = $"Delete from {TableConstants.CustomersTable} where id=@id";
            using(var cmd = GetCommand(sql))
            {
                cmd.Parameters.Add(GetParameter("id", DbType.Int32, entity.Id));
                try{
                    var deleteResult=await cmd.ExecuteNonQueryAsync();
                    if ((int)deleteResult != 0)
                        return true;
                }
                catch (Exception){
                    CloseConnection();
                    throw;
                }
            }
            return false;
        }

        public override async Task<bool> Update(Customer entity)
        {
            OpenConnection();
            if (entity == null)
                return false;
            string sql = $"Update {TableConstants.CustomersTable} set surname=@surname," +
                                                                    $"name=@name," +
                                                                    $"patronymic=@patronymic, " +
                                                                    $"phoneNumber=@phoneNumber, " +
                                                                    $"email=@email " +
                                                                    $"where id=@id";
            using(var cmd = GetCommand(sql)){
                cmd.Parameters.Add(GetParameter("surname", DbType.String, entity.Surname));
                cmd.Parameters.Add(GetParameter("name", DbType.String, entity.Name));
                cmd.Parameters.Add(GetParameter("patronymic", DbType.String, entity.Patronymic));
                cmd.Parameters.Add(GetParameter("phoneNumber", DbType.String, entity.PhoneNumber??""));
                cmd.Parameters.Add(GetParameter("email", DbType.String, entity.Email));
                cmd.Parameters.Add(GetParameter("id", DbType.Int32, entity.Id));
                try{
                    var result =await cmd.ExecuteNonQueryAsync();
                    if(result!=0)
                        return true;
                }
                catch (Exception){
                    CloseConnection();
                    throw;
                }
                return false;
            }
                                                                    
        }
    }
}
