using System.Collections.Generic;

namespace EUCore.Entity.Tree
{
    public abstract class TreeCategoryBase<TPrimaryKey> : TreeEntityBase<TPrimaryKey>, ITreeCategory<TPrimaryKey>
    {
        public virtual ITreeCategory<TPrimaryKey> Parent { get; set; }
        public virtual List<ITreeCategory<TPrimaryKey>> Childs { get; set; } = new List<ITreeCategory<TPrimaryKey>>();
    }
}
