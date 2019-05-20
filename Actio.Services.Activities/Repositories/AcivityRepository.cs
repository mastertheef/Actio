using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Actio.Services.Activities.Repositories
{
    public class AcivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public AcivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activities");

        public async Task AddAsync(Activity activity) => await Collection.InsertOneAsync(activity);

        public async Task<Activity> GetAsync(Guid id) => await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
    }
}
