namespace ShopLibrary.Interfaces
{
    /// <summary>
    /// Методы расширения для работы с хранилищами
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Выполняет множественное удаление сущностей из хранилища
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="items"></param>
        public static void RemoveRange<T>(this IRepository<T> repository,List<T> items) where T : class,IEntity, new()
        {
            repository.AutoSaveChanges = false;
            foreach (var item in items)
            {
                repository.Remove(item);
            }
            repository.SaveChanges();
            repository.AutoSaveChanges=true;
        }

        /// <summary>
        /// Выполняет множественное удаление сущностей из хранилища
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="items"></param>
        public static async Task RemoveRangeAsync<T>(this IRepository<T> repository, List<T> items,CancellationToken token=default) where T : class, IEntity, new()
        {
            repository.AutoSaveChanges = false;
            foreach (var item in items)
            {
                await repository.RemoveAsync(item,token);
            }
            await repository.SaveChangesAsync(token);
            repository.AutoSaveChanges = true;
        }



    }
}
