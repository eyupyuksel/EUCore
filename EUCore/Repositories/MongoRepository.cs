using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EUCore.Audit;
using EUCore.Configuration;
using EUCore.Entity;
using EUCore.Extension;
using EUCore.Repositories.Dapper;
using EUCore.Repositories.Extensions;
using MongoDB.Driver;

namespace EUCore.Repositories
{
    public class MongoRepository<TEntity, TPrimaryKey> : DapperRepositoryBase<TEntity, TPrimaryKey>
       where TEntity : class, IEntity<TPrimaryKey>, new()
    {

        private readonly IMongoCollection<TEntity> mongoCollection;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly AppSettings _appSettings;

        protected IMongoCollection<TEntity> Connection => mongoCollection;
        protected MongoClient Client => client;
        protected IMongoDatabase Database => client.GetDatabase(_appSettings.DatabaseConfig.ConnectionString);



        public string CollectionName
        {
            get { return typeof(TEntity).Name.Replace("Entity", ""); }
        }
        public MongoRepository(AppSettings appSettings, IAuditManager auditManager) : base(auditManager)
        {
            _appSettings = appSettings;
            client = new MongoClient(appSettings.DatabaseConfig.ConnectionString);
            database = client.GetDatabase(appSettings.DatabaseConfig.DatabaseName);
            mongoCollection = database.GetCollection<TEntity>(CollectionName);
        }

        public override long Count(Expression<Func<TEntity, bool>> predicate)
        {
            var count = GetAll(predicate).Count();
            return count;
        }

        public override Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public override void Delete(TEntity entity, bool force = false)
        {
            if (force)
            {
                mongoCollection.DeleteOne(m => m.Id.Equals(entity.Id));
                return;
            }
            OnDelete(entity);
            if (entity is ISoftDelete)
                mongoCollection.ReplaceOne(m => m.Id.Equals((TPrimaryKey)entity.GetPrimaryKey()), entity);
            else
                mongoCollection.DeleteOne(m => m.Id.Equals((TPrimaryKey)entity.GetPrimaryKey()));

        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate, bool force = false)
        {
            GetAll(predicate.AndDeleteFilter()).ForEach(x => Delete(x, force));
        }

        public override TEntity First(TPrimaryKey id) => FirstOrDefault(id) ?? throw new Exception("EntityNotFoundException");


        public override TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            var result = FirstOrDefault(predicate);
            return result ?? throw new KeyNotFoundException();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(id.CreateKeyCondition<TEntity, TPrimaryKey>());
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var obj = mongoCollection.Find(predicate.AndDeleteFilter()).FirstOrDefault();
            return obj;
        }

        public override TEntity Get(TPrimaryKey id)
        {
            return mongoCollection.Find<TEntity>(m => m.Id.Equals(id)).FirstOrDefault();
            throw new Exception("EntityNotFoundException");
        }

        public override IEnumerable<TEntity> GetAll()
        {
            return mongoCollection.Find(default(Expression<Func<TEntity, bool>>).AndDeleteFilter()).ToList();
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var pred = predicate.AndDeleteFilter();
            var pred1 = predicate.AndDeleteFilter().Compile();
            var temp = mongoCollection.Find(pred);
            return temp.ToList();
        }

        public override IEnumerable<TEntity> GetAllByNoFilter(Expression<Func<TEntity, bool>> predicate)
        {
            return mongoCollection.Find(predicate).ToList();
        }

        public override IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override void Insert(TEntity entity) => InsertAndGetId(entity);

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            OnInsert(entity);
            mongoCollection.InsertOne(entity);
            TPrimaryKey primaryKey = (TPrimaryKey)entity.GetPrimaryKey();
            AfterInsert(entity);
            return primaryKey;
        }

        public override TEntity Single(TPrimaryKey id)
        {
            return Single(id.CreateKeyCondition<TEntity, TPrimaryKey>());
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return mongoCollection.Find(predicate.AndDeleteFilter()).Single();
        }

        public override void Update(TEntity entity)
        {
            OnUpdate(entity);
            mongoCollection.ReplaceOne(m => m.Id.Equals((TPrimaryKey)entity.GetPrimaryKey()), entity);
        }
    }

}
