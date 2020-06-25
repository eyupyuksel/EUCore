using EUCore.Entity.Auditing;

namespace EUCore.Entity.Tree
{
    public abstract class TreeEntityBase<TPrimaryKey>: AuditedEntityBase<TPrimaryKey>, ITreeEntity<TPrimaryKey>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public TPrimaryKey ParentId { get; set; }
    }
}
