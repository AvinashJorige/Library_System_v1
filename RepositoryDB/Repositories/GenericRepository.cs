using Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDB.Repositories
{
    public class GenericRepository<T> : BaseMongoRepository<T> where T : IEntity
    {
        private string AdminCollectionName = "";

        private readonly MongoDataContext _dataContext;

        public GenericRepository(MongoDataContext dataContext)
        {
            Type typeParameterType = typeof(T);

            _dataContext = dataContext;
            AdminCollectionName = typeParameterType.Name;
        }

        protected override IMongoCollection<T> Collection => _dataContext.MongoDatabase.GetCollection<T>(AdminCollectionName);
    }
}
