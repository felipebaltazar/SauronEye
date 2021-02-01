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
    public class ProblemRepository : ARepository
    {
        public ProblemRepository(IConfiguration config) : base(config)
        {
        }

        public async Task Save(Problem item, string username = "")
        {
            var db = GetDB();
            if (!(await this.CheckCollection(db, nameof(Problem))))
            {
                db.CreateCollection(nameof(Problem));
            }
            var col = db.GetCollection<Problem>(nameof(Problem));

            if (username != "") item.Id = $"{username}_{item.Eventid}";
            if (username == "" && item.Id == null) throw new Exception("É necessário informar o id do problema ou um username associado!");

            var res = await col.ReplaceOneAsync(
                filter: new BsonDocument("_id", item.Id),
                options: new ReplaceOptions { IsUpsert = true },
                replacement: item);

            Debug.WriteLine(res.ToString());
        }


        public async Task<List<Problem>> GetByUserAndEventId(string username, string eventid)
        {
            var list = await GetProblems(x => x.Id == $"{username}_{eventid}");
            return list;
        }

        public async Task<List<Problem>> GetByUser(string username)
        {
            var list = await GetProblems(x => x.Id.StartsWith(username));
            return list;
        }

        private async Task<List<Problem>> GetProblems(Expression<Func<Problem, bool>> filter)
        {
            var db = GetDB();
            var col = db.GetCollection<Problem>("Problem");
            var options = new FindOptions<Problem> { Limit = 50 };
            var cursor = await col.FindAsync(filter, options);

            IEnumerable<Problem> list = null;
            while (await cursor.MoveNextAsync())
            {
                list = cursor.Current;
            }
            var problems = list != null ? list.ToList() : null;

            return problems.Any() ? problems : null;
        }

    }
}
