using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly bool _seed;
        private readonly IMongoDatabase _database;

        public MongoInitializer(IMongoDatabase database, IOptions<MongoOptions> options)
        {
            _database = database;
            _seed = options.Value.Seed;
        }

        public async Task InitializeAsync()
        {
            if (_initialized) { return; }
            RegisterConvensions();

            if (!_seed) { return; }
        }

        private void RegisterConvensions()
        {
            ConventionRegistry.Register("ActionConvensions", new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
