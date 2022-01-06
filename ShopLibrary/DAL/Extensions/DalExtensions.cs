using ShopLibrary.DAO.interfaces;

namespace ShopLibrary.DAL.Extensions
{
    public static class DalExtensions
    {
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

        public static async Task<List<T>> Select<T>(this IRepository<T> repository, Func<T, bool> predicate)
        {
            var list = await repository.Select();            
            return list.Where(predicate).ToList();
        }
    }
}
