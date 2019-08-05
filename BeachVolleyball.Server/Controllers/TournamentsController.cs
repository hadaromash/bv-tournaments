using System.Collections.Generic;
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
            var tournaments = await beachVolleyDb.GetTournamentsAsync();
            return tournaments;
        }

        public async Task UpdateAll()
        {

        }
    }
}