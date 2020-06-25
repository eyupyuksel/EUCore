namespace EUCore.Entity.Auditing
{
    public interface IAudited : ICreationAudited, IModificationAudited
    {

    }

    public interface IAudited<TPrimaryKey,TUser> : IAudited, ICreationAudited<TPrimaryKey, TUser>, IModificationAudited<TPrimaryKey,TUser>
        where TUser : IEntity<TPrimaryKey>
    {

    }
}