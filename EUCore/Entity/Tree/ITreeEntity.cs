namespace EUCore.Entity.Tree
{
    public interface ITreeEntity<TPrimaryKey>: IEntity<TPrimaryKey>, ITree
    {
        TPrimaryKey ParentId { get; set; }
    }
}
