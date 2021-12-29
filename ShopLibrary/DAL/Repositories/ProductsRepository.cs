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
    }
}
