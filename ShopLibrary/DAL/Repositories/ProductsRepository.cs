using ShopLibrary.Models;
using System.Data;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class ProductsRepository:Repository<Product>
    {
        public ProductsRepository(DbProviderFactory factory, string connectionString) :base(factory,connectionString)
        {            
        }

        public override async Task<List<Product>> Select()
        {
            OpenConnection();
            List<Product> products = new();
            string sql = $"Select * from {TableConstants.ProductsTable}";
            using (var cmd = GetCommand(sql))
            {
                try
                {                   
                    using var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var product = new Product();
                        product.Id = (int)reader["id"];
                        product.Email = reader.GetString("email");
                        product.Description = reader.GetString("description");
                        product.ProductCode = (int)reader["productCode"];

                        products.Add(new Product
                        {
                            Id = (int)reader["id"],
                            Email = reader.GetString("email"),
                            Description = reader.GetString("description"),
                            ProductCode = (int)reader["productCode"]
                        });
                    }

                }
                catch (Exception)
                {
                    CloseConnection();
                    throw;
                }


            }
            return products;
        }

        public override async Task<int> Insert(Product entity)
        {
            OpenConnection();
            string sqlInsert = $"Insert into {TableConstants.ProductsTable} (email,productCode,description)" +
                       $"values(@email,@productCode,@description)";
            string sqlIdentity = $"SELECT @@identity FROM {TableConstants.ProductsTable}";
            using (var cmdInsert = GetCommand(sqlInsert)){
                using var cmdIdentity=GetCommand(sqlIdentity);                //MSAccess не поддерживает возвращаемый параметр
                cmdInsert.Parameters.Add(GetParameter("email", DbType.String, entity.Email));
                cmdInsert.Parameters.Add(GetParameter("productCode", DbType.Int32, entity.ProductCode));
                cmdInsert.Parameters.Add(GetParameter("description", DbType.String, entity.Description));
                DbTransaction transaction = (DbTransaction)_connection.BeginTransaction();
                try
                {
                    cmdInsert.Transaction = transaction;
                    cmdIdentity.Transaction= transaction;
                    await cmdInsert.ExecuteNonQueryAsync();
                    int newRecordId=(int)await cmdIdentity.ExecuteScalarAsync();
                    transaction.Commit();
                    return newRecordId;  
                }
                catch (InvalidOperationException){
                    CloseConnection(); transaction?.Rollback();                  
                }
                catch(Exception){
                    CloseConnection(); transaction?.Rollback();
                }
            }
            return 0;
        }

        public override async Task<bool> Delete(Product entity)
        {
            OpenConnection();
            bool result = false;
            string sql = $"Delete from {TableConstants.ProductsTable} where id=@id";
            using (var cmd = GetCommand(sql)){
                cmd.Parameters.Add(GetParameter("id",DbType.Int32, entity.Id));
                try{
                    int rowDeleted=await cmd.ExecuteNonQueryAsync();
                    if (rowDeleted != 0) result = true;
                }
                catch (Exception){
                    CloseConnection();
                    throw;
                }
            }
            return result;
        }


        public override async Task<bool> Update(Product entity)
        {
            if(entity == null)
                return false;
            OpenConnection();
            string sql = $"Update {TableConstants.ProductsTable} set description=@description," +
                                                                $"email=@email," +
                                                                $"productCode=@productCode " +
                                                                $"where id=@id";
            bool result = false;
            using (var cmd = GetCommand(sql))
            {            
                cmd.Parameters.Add(GetParameter("description", DbType.String, entity.Description));
                cmd.Parameters.Add(GetParameter("email", DbType.String, entity.Email));
                cmd.Parameters.Add(GetParameter("productCode", DbType.Int32, entity.ProductCode));
                cmd.Parameters.Add(GetParameter("id", DbType.Int32, entity.Id));
                try{
                    int rowUpdated = await cmd.ExecuteNonQueryAsync();
                    if(rowUpdated != 0) result = true;
                }
                catch (Exception){
                    CloseConnection();
                    throw;
                }

            }
            return result;
        }


    }
}
