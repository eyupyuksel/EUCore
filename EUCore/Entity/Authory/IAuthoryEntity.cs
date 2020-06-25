namespace EUCore.Entity.Authory
{
    public interface IAuthoryEntity : IAuthoryEntity<int, int>
    {
        
    }
    public interface IAuthoryEntity<TPrimaryKey, TUserKey> : IEntity<TPrimaryKey>
    {
        TUserKey UserId { get; set; }
        AuthoryType AuthoryType { get; set; }
        bool View { get; set; }
        bool Create { get; set; }
        bool Update { get; set; }
        bool Delete { get; set; }
        bool IsAuthory { get; set; }


    }
}
