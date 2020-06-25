namespace EUCore.Entity
{
    public interface IEntity<TPrimaryKey> : IEntity
    {
        TPrimaryKey Id { get; set; }
        
    }
}
