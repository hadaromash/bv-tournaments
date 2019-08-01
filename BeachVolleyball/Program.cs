using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BeachVolleyball
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IBeachVolleyDb beachVolleyDb = new BeachVolleyballDb();
            List<Tournament> tournaments = await beachVolleyDb.GetTournamentsAsync();

            List<CategoryCodes> categoryCodes = new List<CategoryCodes>
            {
                new CategoryCodes(CategoryType.MenA),
                new CategoryCodes(CategoryType.MenB),
                new CategoryCodes(CategoryType.WomenA),
                new CategoryCodes(CategoryType.WomenB),
                new CategoryCodes(CategoryType.YouthMen),
                new CategoryCodes(CategoryType.YouthWomen)
            };

            Tournament tournament = GetTournament(tournaments);
            CategoryCodes category = GetCategory(categoryCodes);

            while (tournament != null && category != null)
            {
                Debug.WriteLine(tournament);
                category = GetCategory(categoryCodes);
            }
        }

        private static Tournament GetTournament(List<Tournament> tournaments)
        {
            Console.WriteLine("Select tournament by id:");
            for (int i = 0; i < tournaments.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i + 1, tournaments[i].Name);
            }

            int tournamentId = int.Parse(Console.ReadLine());
            if (tournamentId - 1 < tournaments.Count)
            {
                Tournament tournament = tournaments[tournamentId - 1];
                return tournament;
            }

            return null;
        }

        private static CategoryCodes GetCategory(List<CategoryCodes> categoryCodes)
        {
            PrintIntro(categoryCodes);
            int categoryId = int.Parse(Console.ReadLine());
            CategoryCodes category = categoryCodes.Where(catCode => catCode.Code == categoryId).FirstOrDefault();
            return category;
        }

        private static void PrintIntro(List<CategoryCodes> categoryCodes)
        {
            Console.WriteLine("Enter category code:");

            foreach (var catCode in categoryCodes)
            {
                Console.WriteLine("{0} - {1}", catCode.Category, catCode.Code);
            }
        }

        private class CategoryCodes
        {
            private static int nextCode = 1;
            public CategoryCodes(CategoryType cat)
            {
                this.Category = cat;
                this.Code = nextCode;
                nextCode++;
            }

            public CategoryType Category { get; private set; }
            public int Code { get; private set; }
        }
    }
}
