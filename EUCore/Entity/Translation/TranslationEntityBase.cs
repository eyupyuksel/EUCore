namespace EUCore.Entity.Translation
{
    public abstract class TranslationEntityBase : TranslationEntityBase<int, int, int>, ITranslationEntity
    {
        
    }
    public abstract class TranslationEntityBase<TPrimaryKey, TCultureKey, TMasterKey> : EntityBase<TPrimaryKey>, ITranslationEntity<TPrimaryKey, TCultureKey, TMasterKey>
    {
        public virtual TCultureKey CultureId { get; set; }
        public virtual TMasterKey MasterId { get; set; }
    }
}
