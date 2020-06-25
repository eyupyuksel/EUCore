namespace EUCore.Entity.Auditing
{
    public interface IFullAudited : IDeletableAudited, IPassivable
    {
        void Verify();
    }
    public interface IDeletableAudited : IAudited, IDeletionAudited
    {
    }
    public interface IFullAudited<TPrimaryKey, TUser> : IAudited<TPrimaryKey,TUser>, IFullAudited, IDeletionAudited<TPrimaryKey,TUser>
        where TUser : IEntity<TPrimaryKey>
    {

    }
}