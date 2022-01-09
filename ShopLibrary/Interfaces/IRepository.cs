namespace ShopLibrary.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Items { get; }

        T Get(int id);
        Task<T> GetAsync(int id, CancellationToken token = default);

        T Add(T entity);
        Task<T> AddAsync(T entity, CancellationToken token = default);

        void Update(T entity);
        Task UpdateAsync(T entity, CancellationToken token = default);

        void Remove(T entity);
        Task RemoveAsync(T entity, CancellationToken token = default);

    }
}
