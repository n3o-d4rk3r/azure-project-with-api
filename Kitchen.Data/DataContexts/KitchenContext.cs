using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Kitchen.Data.DataContexts
{


    public class KitchenContext
    {
        private static IMongoDatabase? Database { get; set; }
        private static MongoClient? mongoClient { get; set; }

        private readonly List<Func<Task>> _commands;
        public KitchenContext(IConfiguration configuration)
        {
            // Set Guid to CSharp style (with dash -)
            //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();

            RegisterConventions();
            var settings = MongoClientSettings.FromConnectionString(configuration.GetSection("MongoSettings").GetSection("Connection").Value);

            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Configure mongo (You can inject the config, just to simplify)
            mongoClient = new MongoClient(settings);

            Database = mongoClient.GetDatabase(configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);
        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack
            {
            new IgnoreExtraElementsConvention(true),
            new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }

        public async Task<int> SaveChanges()
        {
            var qtd = _commands.Count;
            foreach (var command in _commands)
            {
                await command();
            }

            _commands.Clear();
            return qtd;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task AddCommand(Func<Task> func)
        {
            _commands.Add(func);
            return Task.CompletedTask;
        }
    }
}
