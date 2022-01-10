namespace System.Collections.ObjectModel
{
    public static class CollectionExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items) =>
            new ObservableCollection<T>(items);

        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> items) =>
            new ObservableCollection<T>(items);

        public static bool RemoveRange<T>(this ObservableCollection<T> collection,List<T> deletedItems)
        {
            var result = false;
            foreach (T item in deletedItems){
                result=collection.Remove(item);
                if (!result)
                    throw new ArgumentException($"Can`t remove {nameof(item)} {typeof(T)}");
            }
            return result;
        }
    }
}
