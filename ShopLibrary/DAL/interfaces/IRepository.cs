using ShopLibrary.Models;
using System.Data;

namespace ShopLibrary.DAO.interfaces
{
    public interface IRepository<T> 
    {
        public string ConnectionString { get;}
        public ConnectionState ConnectionState { get; }
        public IDbDataAdapter DataAdapter { get; }

        public T GetById(int id);
        public T Find(object value);
        public Task<List<T>> Select();       
        public Task<List<T>> Select(string fieldName, object value);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(T entity);
        public bool Insert(T entity);
        public bool InsertMany(IEnumerable<T> entities);

        
    }
}
