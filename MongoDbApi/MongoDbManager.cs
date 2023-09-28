using System.Security.Authentication;
using MongoDB.Driver;
using MongoDbApi.Models;
using MongoDbWebApi;

namespace MongoDbApi
{
    public class MongoDbManager
    {
        private IMongoDatabase _db;
        const string collectionPersonName = "Person";
        const string dbName = "d1";
        public MongoDbManager()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(Secrets.ConnectionString)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            _db = mongoClient.GetDatabase(dbName);
        }

        public async Task Add(string firstName, string lastName)
        {
            var collection = _db.GetCollection<PersonModel>(collectionPersonName);
            var person = new PersonModel() { FirstName = firstName, LastName = lastName };
            await collection.InsertOneAsync(person);
        }
        public async Task Delete(string firstName, string lastName)
        {
            var collection = _db.GetCollection<PersonModel>(collectionPersonName);
            var personMatch = collection.Find(x => x.FirstName == firstName && x.LastName == lastName);
            var person = personMatch.FirstOrDefault();
            if (person == null)
                throw new Exception();

            await collection.DeleteOneAsync(x => x.Id == person.Id);
        }

        public async Task<IEnumerable<string>> GetALl()
        {
            var collection = _db.GetCollection<PersonModel>(collectionPersonName);
            var elements = await collection.FindAsync(_ => true);
            return elements.ToList().Select(x => x.FirstName);
        }
    }
}
