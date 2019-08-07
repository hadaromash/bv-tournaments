using System;
using System.Collections.Generic;
using System.Threading;
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
            CancellationTokenSource cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(20));
            var tournaments = await this.tournamentsService.GetAllTournamentsAsync(cancellationToken.Token);
            return tournaments;
        }
    }
}