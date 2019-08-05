using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeachVolleyball.Server.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IBeachVolleyDb beachVolleyDb;

        public CategoriesController(IBeachVolleyDb beachVolleyDb)
        {
            this.beachVolleyDb = beachVolleyDb;
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 86400)]
        public async Task<List<Category>> Index(string tournamentId)
        {
            var categories = await beachVolleyDb.GetCategoriesAsync(tournamentId);
            return categories;
        }
    }
}