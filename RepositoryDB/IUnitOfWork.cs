using MongoDB.Driver;

namespace RepositoryDB
{
    public interface IUnitOfWork
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
        IMongoDatabase CreateNewDatabase();
    }
}
