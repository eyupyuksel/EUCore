namespace EUCore.Entity.Translation
{
    public interface ITranslationEntity : ITranslationEntity<int, int, int>
    {
        
    }
    public interface ITranslationEntity<TPrimaryKey, TCultureKey, TMasterKey> : IEntity<TPrimaryKey>
    {
        TCultureKey CultureId { get; set; }
        TMasterKey MasterId { get; set; }
    }
}
