namespace EUCore.Entity.Authory
{
    public abstract class AuthoryEntityBase : AuthoryEntityBase<int, int>, IAuthoryEntity
    {
        
    }
    public abstract class AuthoryEntityBase<TPrimaryKey, TUserPrimaryKey> : EntityBase<TPrimaryKey>, IAuthoryEntity<TPrimaryKey, TUserPrimaryKey>
    {
        public TUserPrimaryKey UserId { get; set; }
        public AuthoryType AuthoryType { get; set; }
        public bool View { get; set; }
        public bool Create { get; set; } = true;
        public bool Update { get; set; } = true;
        public bool Delete { get; set; } = true;
        public virtual bool IsAuthory { get; set; } = true;
    }
}
