using System.Data;

namespace ShopLibrary.DAO.interfaces
{
    public interface IRepository<T> 
    {
        public string ConnectionString { get;}
        public ConnectionState ConnectionState { get; }              
        public Task<List<T>> Select();
        public Task<bool> Update(T entity);
        public Task<bool> Delete(T entity);
        public Task<int> Insert(T entity);        
        public Task<bool> InsertMany(IEnumerable<T> entities);
        
    }
}
