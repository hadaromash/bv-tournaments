using System.Collections.Generic;
using System.Threading.Tasks;
using Engine;
using Microsoft.AspNetCore.Mvc;

namespace BeachVolleyball.Server.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly IBeachVolleyDb beachVolleyDb;
        private readonly ITournamentsService tournamentsService;

        public TournamentsController(IBeachVolleyDb beachVolleyDb, ITournamentsService tournamentsService)
        {
            this.beachVolleyDb = beachVolleyDb;
            this.tournamentsService = tournamentsService;
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 1800)]
        public async Task<List<Tournament>> Index()
        {
            return new List<Tournament>();
        }

        public async Task UpdateAll()
        {
            await this.tournamentsService.UpdateTournamentsAsync();
        }
    }
}