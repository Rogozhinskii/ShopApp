namespace ShopLibrary.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для всех сущностей в источнике данных
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
    }
}
