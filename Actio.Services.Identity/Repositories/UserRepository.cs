using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<User> Collection => _database.GetCollection<User>("Users");

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(User user) => await Collection.InsertOneAsync(user);

        public Task<User> GetAsync(Guid id) => Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public Task<User> GetAsync(string email) => Collection.AsQueryable().FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant());
    }
}
