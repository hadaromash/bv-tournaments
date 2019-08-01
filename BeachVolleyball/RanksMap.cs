//---------------------------------------------------------------------------------------------------------------------
// <copyright file="RanksMap.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    public class RanksMap : IRanksMap
    {
        private enum CategoryType
        {
            MenA,
            MenB,
            WomenA,
            WomenB,
            YouthMen,
            YouthWomen
        }

        private Dictionary<string, double> ranksMap;

        public int Year { get; private set; }

        private RanksMap(int year, Dictionary<string, double> ranksMap)
        {
            this.Year = year;
            this.ranksMap = ranksMap;
        }

        public static async Task<RanksMap> CreateAsync(int year, string categoryName)
        {
            CategoryType category = GetCategoryType(categoryName);
            var ranksMap = await CreateRanksMapAsync(year, category);
            return new RanksMap(year, ranksMap);
        }

        public double GetRank(string playerName, string association, int age)
        {
            string key = playerName + association + age.ToString();
            if (ranksMap.TryGetValue(key, out double rank))
            {
                return rank;
            }

            return 0;
        }

        private static CategoryType GetCategoryType(string displayName)
        {
            switch (displayName)
            {
                case "נשים רמה א'":
                    return CategoryType.WomenA;
                case "גברים רמה א'":
                    return CategoryType.MenA;
                case "גברים רמה ב'":
                    return CategoryType.MenB;
                case "נשים רמה ב'":
                    return CategoryType.WomenB;
                case "נוער בנים":
                    return CategoryType.YouthMen;
                case "נוער בנות":
                    return CategoryType.YouthWomen;
                default:
                    throw new Exception("Unknown category name: {0}" + displayName);
            }
        }

        private static async Task<Dictionary<string, double>> CreateRanksMapAsync(int year, CategoryType category)
        {
            Dictionary<string, double> ranksMap = new Dictionary<string, double>();
            List<HtmlNode> rankNodes = await GetPlayersRankNodesAsync(year, category);
            foreach (var rankNode in rankNodes)
            {
                var relevantDescendants = rankNode.Descendants("td").ToList();
                string name = HttpUtility.HtmlDecode(relevantDescendants[1].InnerText);

                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }

                string association = HttpUtility.HtmlDecode(relevantDescendants[2].InnerText);
                string age = HttpUtility.HtmlDecode(relevantDescendants[3].InnerText);
                double rank = double.Parse(HttpUtility.HtmlDecode(relevantDescendants[5].InnerText));

                string key = name + association + age;
                ranksMap.Add(key, rank);
            }

            return ranksMap;
        }

        private static async Task<List<HtmlNode>> GetPlayersRankNodesAsync(int year, CategoryType category)
        {
            string rankPageByYear = GetRankingPageUrl(year, category);
            HtmlWeb web = new HtmlWeb();
            HtmlDocument rankingDoc = await web.LoadFromWebAsync(rankPageByYear);
            var rankingTableArea = rankingDoc.GetElementbyId("Ranking");
            var rankingTable = rankingTableArea.Descendants("table").First();
            IEnumerable<HtmlNode> playerRankNodes = rankingTable.Descendants("tr");
            var rankNodeList = playerRankNodes.ToList();
            rankNodeList.RemoveAt(0);
            return rankNodeList;
        }

        private static string GetRankingPageUrl(int year, CategoryType category)
        {
            const string RankingWebPageTemplate = "http://www.iva.org.il/Ranking.asp?cYear={0}&cMode=0&GenderId={1}&level_id={2}#";
            int genderId = GetGenderId(category);
            int levelId = GetLevelId(category);
            return string.Format(RankingWebPageTemplate, year, genderId, levelId);
        }

        private static int GetGenderId(CategoryType category)
        {
            switch(category)
            {
                case CategoryType.MenA:
                case CategoryType.MenB:
                case CategoryType.YouthMen:
                    return 10;
                case CategoryType.WomenA:
                case CategoryType.WomenB:
                case CategoryType.YouthWomen:
                    return 11;
                default: throw new Exception("Unsupported category: " + category);
            }
        }

        private static int GetLevelId(CategoryType category)
        {
            switch (category)
            {
                case CategoryType.MenA:
                case CategoryType.MenB:
                case CategoryType.WomenA:
                case CategoryType.WomenB:
                    return 1;
                case CategoryType.YouthMen:
                case CategoryType.YouthWomen:
                    return 4;
                default:
                    throw new Exception("Unsupported category: " + category);
            }
        }
    }
}
