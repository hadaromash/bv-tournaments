using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeachVolleyball.Server.Controllers
{
    public class CategoriesController : Controller
    {
        public string[] Index()
        {
            string[] categories = Enum.GetNames(typeof(Category));
            return categories;
        }
    }
}