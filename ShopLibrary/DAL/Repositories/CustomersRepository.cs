﻿using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class CustomersRepository : Repository<Customer>
    {
        private DbDataAdapter _dbAdapter;
        public CustomersRepository(DbProviderFactory factory, string connectionString) 
            : base(factory, connectionString)
        {
        }

       

        public override async Task<List<Customer>> Select()
        {
            List<Customer> retval = new();
            string sql = $"Select * from {TableNames.CustomersTable}";
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
        public override async Task<bool> Insert(Customer entity)
        {
            if(entity == null)
                 throw new ArgumentNullException($"can`t add an empty link {nameof(entity)}");
            string sql = $"Insert into {TableNames.CustomersTable} (surname,name,patronymic,phoneNumber,email)" +
                                                                   $"values(@surname,@name,@patronymic,@phoneNumber,@email)";
            using(var cmd = GetCommand(sql)){                
                cmd.Parameters.Add(GetParameter("surname", DbType.String, entity.Surname));
                cmd.Parameters.Add(GetParameter("name", DbType.String, entity.Name));
                cmd.Parameters.Add(GetParameter("patronymic", DbType.String, entity.Patronymic));
                cmd.Parameters.Add(GetParameter("phoneNumber", DbType.String, entity.PhoneNumber ?? string.Empty));
                cmd.Parameters.Add(GetParameter("email", DbType.String, entity.Email));
                try{
                    var insertResult=await cmd.ExecuteNonQueryAsync();
                    if(insertResult!=0)
                        return true;
                }
                catch (InvalidOperationException){
                    CloseConnection();
                    return false;                 
                }
                catch (Exception){
                    CloseConnection();
                    throw;
                }

            }
            return false;
        }
    }
}
