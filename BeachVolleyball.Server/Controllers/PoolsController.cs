using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeachVolleyball.Server.Controllers
{
    public class PoolsController : Controller
    {
        private readonly IPoolsDraw poolsDraw;

        public PoolsController(IPoolsDraw poolsDraw)
        {
            this.poolsDraw = poolsDraw;
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 900)]
        public async Task<List<Pool>> Index(int tournamentId, int categoryId, string categoryName)
        {
            List<Pool> pools = await this.poolsDraw.GetPoolsAsync(tournamentId, categoryId, categoryName);
            return pools;
        }
    }
}