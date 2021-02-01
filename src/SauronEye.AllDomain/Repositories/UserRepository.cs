using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SauronEye.AllDomain.Domain.Commom;
using SauronEye.AllDomain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Repositories
{
    public class UserRepository : ARepository
    {
        public UserRepository(IConfiguration config) : base(config)
        {
        }

        public async Task Save(User item) 
        {
            var db = GetDB();
            if (!(await this.CheckCollection(db, nameof(User))))
            {
                db.CreateCollection(nameof(User));
            }
            var col = db.GetCollection<User>(nameof(User));


            var userExist = await this.GetByUsername(item.Username);
            if (userExist != null) item.Id = userExist.Id;

            var res = await col.ReplaceOneAsync(
                filter: new BsonDocument("Username", item.Username),
                options: new ReplaceOptions { IsUpsert = true },
                replacement: item);

            Debug.WriteLine(res.ToString());
        }


        public async Task<User> GetByUsername(string username)
        {
            var user = await GetUser(x => x.Username == username);
            return user;
        }
        public async Task<User> GetByToken(string token)
        {
            var user = await GetUser(x => x.Jwt == token);
            return user;
        }

        public async Task<User> GetByLogin(string username, string password)
        {
            var user = await GetUser(x => x.Username == username && x.Password == password);
            return user;
        }

        private async Task<User> GetUser(Expression<Func<User,bool>> filter)
        {
            var db = GetDB();
            var col = db.GetCollection<User>("User");
            var options = new FindOptions<User> { Limit = 1 };
            var cursor = await col.FindAsync(filter, options);

            IEnumerable<User> list = null;
            while (await cursor.MoveNextAsync())
            {
                list = cursor.Current;
            }
            List<User> users = list != null ? list.ToList() : null;

            return users.Any() ? users.First() : null;
        }
    }
}
