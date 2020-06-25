using System.Collections.Generic;
using System.Linq;

namespace EUCore.Entity.Tree
{
    public static class TreeExtensions
    {
        public static TResult Build<TResult, TPrimaryKey>(this TResult root, IEnumerable<TResult> list) where TResult : ITreeCategory<TPrimaryKey>, new()
        {
            foreach (var permissionResult in list.Where(p => Equals(p.ParentId, root.Id)))
            {
                root.Childs.Add(permissionResult);
                Build<TResult,TPrimaryKey>(permissionResult, list);
            }
            return root;
        }
    }
}
