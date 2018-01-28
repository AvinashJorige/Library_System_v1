using Domain.Entities;
using RepositoryDB;
using RepositoryDB.Repositories;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace TestProgram
{
    class Program
    {
        //Usefull Link : https://github.com/aliakseimaniuk/MongoRepository
        static void Main(string[] args)
        {
            Task t = MainAsync();
            t.Wait();

            Console.ReadKey();
        }
        static async Task MainAsync()
        {
            var p = new AdminMaster()
            {
                adCode = "Level 1",
                adEmail = "jo.avi.1990@gmail.com",
                adName = "Avinash",
                adPassword = "123456",
                CreatedDate = DateTime.UtcNow.ToString(),
                ModifiedDate = DateTime.UtcNow.ToString(),
                IsActive = true,
                Id = ObjectId.GenerateNewId().ToString()
            };

            var context = new MongoDataContext();
            var personRepository = new GenericRepository<AdminMaster>(context);

            await personRepository.SaveAsync(p);

            var personFromDatabase = await personRepository.GetByIdAsync(p.Id);            
        }
    }
}
