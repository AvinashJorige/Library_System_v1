using Domain.Entities;
using RepositoryDB;
using RepositoryDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var p = new Person()
            {
                FirstName = "John",
                LastName = "Smith"
            };

            var context = new MongoDataContext();
            var personRepository = new PersonRepository(context);

            await personRepository.SaveAsync(p);

            var personFromDatabase = await personRepository.GetByIdAsync(p.Id);

            Console.WriteLine($"{personFromDatabase.FirstName}, {personFromDatabase.LastName}, id: {personFromDatabase.Id}");
        }
    }
}
