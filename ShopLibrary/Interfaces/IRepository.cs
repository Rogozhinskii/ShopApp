namespace ShopLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс хранилищ сущностей
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Отвечает за автоматические фиксирование изменений отслеживаемых сущностей
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        /// <summary>
        /// Хранимые сущности
        /// </summary>
        IQueryable<T> Items { get; }

        /// <summary>
        /// Возвращает сущность по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Возвращает сущность по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id, CancellationToken token = default);

        /// <summary>
        /// Добавляет сущность в хранилище
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// Добавляет сущность в хранилище
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity, CancellationToken token = default);

        /// <summary>
        /// Выполняет обновление отслеживаемой сущности
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Выполняет обновление отслеживаемой сущности
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAsync(T entity, CancellationToken token = default);

        /// <summary>
        /// Выполняет удаление отслеживаемой сущности
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
        /// <summary>
        /// Выполняет удаление отслеживаемой сущности
        /// </summary>
        /// <param name="entity"></param>
        Task RemoveAsync(T entity, CancellationToken token = default);
        /// <summary>
        /// Выполняет Сохранение отслеживаемых изменений
        /// </summary>
        /// <param name="entity"></param>
        void SaveChanges();
        /// <summary>
        /// Выполняет Сохранение отслеживаемых изменений
        /// </summary>
        /// <param name="entity"></param>
        Task SaveChangesAsync(CancellationToken token = default);


    }
}
