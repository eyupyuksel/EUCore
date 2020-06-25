namespace EUCore.Entity
{
    public interface IEntity
    {
        bool IsTransient();
        object GetPrimaryKey();
    }
}