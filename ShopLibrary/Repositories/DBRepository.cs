using Microsoft.EntityFrameworkCore;
using ShopLibrary.Context;
using ShopLibrary.Interfaces;

namespace ShopLibrary
{
    internal class DBRepository<T> : IRepository<T> where T : class,IEntity, new()
    {
        private readonly ShopAppDB _db;
        private readonly DbSet<T> _dbSet;

        public bool AutoSaveChanges { get; set; } = true;

        public DBRepository(ShopAppDB context)
        {
            _db = context;
            _dbSet=context.Set<T>();
        }
        public virtual IQueryable<T> Items => _dbSet;

        public T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State = EntityState.Added;            
            if(AutoSaveChanges)
                _db.SaveChanges();
            return entity;
        }

        public async Task<T> AddAsync(T entity,CancellationToken token=default)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State = EntityState.Added;
            if(AutoSaveChanges)
                await _db.SaveChangesAsync(token).ConfigureAwait(false);
            return entity;
        }

        public T Get(int id)=>
            Items.SingleOrDefault(x=>x.Id==id);


        public async Task<T> GetAsync(int id, CancellationToken token = default) =>
            await Items.SingleOrDefaultAsync(x => x.Id == id, token).ConfigureAwait(false);

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State= EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T entity, CancellationToken token = default)
        {
            if(entity==null)
                throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State=EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(token).ConfigureAwait(false);
            
        }


        public void Remove(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State = EntityState.Deleted;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(T entity, CancellationToken token = default)
        {
            if(entity==null)
                throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State = EntityState.Deleted;
            if(AutoSaveChanges)
                await _db.SaveChangesAsync(token).ConfigureAwait(false);            
        }

    }
}
