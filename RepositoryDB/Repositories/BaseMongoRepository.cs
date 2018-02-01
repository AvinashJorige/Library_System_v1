using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Domain.Repositories;
using Domain.Entities;
using System.Reflection;

namespace RepositoryDB.Repositories
{
    public abstract class BaseMongoRepository<TEntity> : IRepository<TEntity, ObjectId> where TEntity : IEntity
    {
        protected abstract IMongoCollection<TEntity> Collection { get; }

        public virtual async Task<TEntity> GetByIdAsync(ObjectId id)
        {
            return await Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id.ToString()))
            {
                entity.Id = ObjectId.GenerateNewId();
            }

            await Collection.ReplaceOneAsync(
                x => x.Id.Equals(entity.Id),
                entity,
                new UpdateOptions
                {
                    IsUpsert = true
                });

            return entity;
        }

        public virtual async Task DeleteAsync(ObjectId id)
        {
            await Collection.DeleteOneAsync(x => x.Id.Equals(id));
        }
        public virtual async Task<TEntity> GetByCustomAsync(Expression<Func<TEntity, bool>> predicate, string filterKey, string filterVal)
        {
            List<TEntity> _listCollection = await Collection.Find(predicate).ToListAsync();
            var index = 0;
            bool flag = false;
            foreach (var _dataObj in _listCollection)
            {
                Type myType = _dataObj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    if (prop.Name == filterKey)
                    {
                        if (prop.GetValue(_dataObj, null).ToString() == filterVal)
                        {
                            index = _listCollection.IndexOf(_dataObj);
                            flag = true;
                            break;
                        }
                    }
                }
            }
            return flag ? _listCollection[index] : default(TEntity);
        }

        public virtual async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).ToListAsync();
        }
    }
}
