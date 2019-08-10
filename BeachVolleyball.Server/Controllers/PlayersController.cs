using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeachVolleyball.Server.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IBeachVolleyDb beachVolleyDb;

        public PlayersController(IBeachVolleyDb beachVolleyDb)
        {
            this.beachVolleyDb = beachVolleyDb;
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 86400)]
        public async Task<string> Photo(int id)
        {
            IvaPlayer player = await this.beachVolleyDb.GetIvaPlayerAsync(id.ToString());
            string imageLink = player.pic_profile_web;
            return imageLink;
        }
    }
}