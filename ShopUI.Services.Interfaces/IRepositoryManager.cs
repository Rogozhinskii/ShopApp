namespace ShopUI.Services.Interfaces
{
    /// <summary>
    /// Интерфейс доспута к созданным хранилкам
    /// </summary>
    public interface IRepositoryManager
    {
        /// <summary>
        /// Коллекция зарегистрированных хранилищ
        /// </summary>
        List<object> Repositories { get; }
        /// <summary>
        /// Возвращает зарегистрированный репозиторий, по его "типу"
        /// </summary>
        /// <param name="repositoryType"></param>
        /// <returns></returns>
        public object GetRepository(RepositoryType repositoryType);
    }
}
