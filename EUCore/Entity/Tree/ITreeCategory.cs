using System.Collections.Generic;

namespace EUCore.Entity.Tree
{
    public interface ITreeCategory<TPrimaryKey> : ITreeEntity<TPrimaryKey>
    {
        ITreeCategory<TPrimaryKey> Parent { get; set; }
        List<ITreeCategory<TPrimaryKey>> Childs { get; set; }
    }
}
