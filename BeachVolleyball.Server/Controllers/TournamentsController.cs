using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeachVolleyball.Server.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly IBeachVolleyDb beachVolleyDb;

        public TournamentsController(IBeachVolleyDb beachVolleyDb)
        {
            this.beachVolleyDb = beachVolleyDb;
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 1800)]
        public async Task<List<Tournament>> Index()
        {
            var tournaments = await beachVolleyDb.GetTournaments();
            return tournaments;
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 900)]
        public async Task<List<Pool>> Pools(int id, Category category)
        {
            Tournament tournament = new Tournament(id, string.Empty);
            await tournament.InitPools(category);

            return tournament.Pools;
        }
    }
}