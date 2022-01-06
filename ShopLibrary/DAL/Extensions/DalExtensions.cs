using ShopLibrary.DAO.interfaces;

namespace ShopLibrary.DAL.Extensions
{
    /// <summary>
    /// Расширения для работы с хранилищами
    /// </summary>
    public static class DalExtensions
    {
        /// <summary>
        /// Удаляет записи из бд, соответвующие условию predicatе, возвращает true если все записи удалены, иначе false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<bool> Delete<T>(this IRepository<T> repository, Func<T, bool> predicate)
        {
            var entities = (await repository.Select()).Where(predicate).ToList();            
            if (entities.Any())
            {
                foreach (T item in entities)
                {
                    var deleteResult = await repository.Delete(item);
                    if (!deleteResult)
                        throw new InvalidOperationException("Cant`t delete object");
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// производит выборку записей из бд, соответующих predicate, возвращает список записей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<List<T>> Select<T>(this IRepository<T> repository, Func<T, bool> predicate)
        {            
            var selectedRecords= await repository.Select();            
            return selectedRecords.Where(predicate).ToList();
        }
    }
}
