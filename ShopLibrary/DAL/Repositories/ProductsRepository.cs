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


        public override async Task<List<Product>> Select(string fieldName, object value)
        {
            List<Product> products = new();            
            if (value is null)
                throw new ArgumentNullException("value to filter cannot be null");
            string sql = $"Select * from {TableNames.ProductsTable} where {fieldName}=@value";
            using (var cmd = GetCommand(sql))
            {
                try
                {
                    cmd.Parameters.Add(GetParameter("value", DbType.String, value.ToString()));
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
                    throw;
                }


            }
            return products;
        }


        public override bool Insert(Product entity)
        {
            string sql = $"Insert into {TableNames.ProductsTable} (email,productCode,description)" +
                       $"values(@email,@productCode,@description)";
            using(var cmd = GetCommand()){
                cmd.CommandText = sql;
                cmd.Parameters.Add(GetParameter("email", DbType.String, entity.Email));
                cmd.Parameters.Add(GetParameter("productCode", DbType.Int32, entity.ProductCode));
                cmd.Parameters.Add(GetParameter("description", DbType.String, entity.Description));
                try{
                    var insertResult=cmd.ExecuteNonQuery();
                    if (insertResult != 0)
                        return true;
                }
                catch (InvalidOperationException){
                    CloseConnection();
                    return false;
                }
                catch(Exception ex)
                {
                    CloseConnection();
                }
            }
            return false;
        }

        public override async Task<bool> Delete(Product entity)
        {
            bool result = false;
            string sql = $"Delete from {TableNames.ProductsTable} where id=@id";
            using (var cmd = GetCommand(sql)){
                cmd.Parameters.Add(GetParameter("id",DbType.Int32, entity.Id));
                try{
                    int rowDeleted=await cmd.ExecuteNonQueryAsync();
                    if (rowDeleted != 0) result = true;
                }
                catch (Exception)
                {
                    CloseConnection();
                    throw;
                }
            }
            return result;
        }


        public override async Task<bool> Update(Product entity)
        {
            string sql = $"Update {TableNames.ProductsTable} set description=@description," +
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
                try
                {
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
