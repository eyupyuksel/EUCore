using System.Diagnostics.CodeAnalysis;
using DapperExtensions.Mapper;
// ReSharper disable once RedundantUsingDirective
using System.Linq;

namespace EUCore.Entity
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class DefaultEntityMapper<TEntity> : AutoClassMapper<TEntity> 
        where TEntity : class, IEntity
    {
        public DefaultEntityMapper()
        {
            Table(TableName.Replace("Entity", string.Empty));

            var primaryKey = Properties.Single(p => p.Name.Equals("Id"));
            primaryKey?.GetType().GetProperty("KeyType")?.SetValue(primaryKey, KeyType.Identity);

            var creationTime = Properties.SingleOrDefault(p => p.Name.Equals("CreationTime"));
            creationTime?.GetType().GetProperty("IsReadOnly")?.SetValue(creationTime, true);

            var creatorUserId = Properties.SingleOrDefault(p => p.Name.Equals("CreatorUserId"));
            creatorUserId?.GetType().GetProperty("IsReadOnly")?.SetValue(creationTime, true);
        }
    }
}
