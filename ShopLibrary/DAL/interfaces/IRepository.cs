namespace ShopLibrary.DAO.interfaces
{
    public interface IRepository<T>
    {
        public T GetById(int id);
        public T Find(object value);
        public List<T> GetAll();
        public bool Update(T entity);
        public bool Delete(int id);
        public bool Insert(T entity);
        public bool InsertMany(IEnumerable<T> entities);
        
    }
}
