namespace ShopLibrary.Interfaces
{
    public static class RepositoryExtensions
    {
        public static void Remove<T>(this DBRepository<T> repository,List<T> items)
        {            
            foreach (var item in items)
            {
                repository.Remove(item);
            }
        }
    }
}
