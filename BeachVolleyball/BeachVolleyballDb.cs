//---------------------------------------------------------------------------------------------------------------------
// <copyright file="BeachVolleyballDb.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    public class BeachVolleyballDb : IBeachVolleyDb
    {
        private const string IVABaseUrl = "http://www.iva.org.il";

        private readonly IPoolsDraw poolsDraw;
        private readonly IRankDb rankDb;

        public BeachVolleyballDb(IPoolsDraw poolsDraw, IRankDb rankDb)
        {
            this.poolsDraw = poolsDraw;
            this.rankDb = rankDb;
        }

        public async Task<List<Tournament>> GetTournamentsAsync(CancellationToken cancellationToken)
        {
            HtmlWeb web = new HtmlWeb();
            string mainPage = IVABaseUrl + "/default.asp";
            HtmlDocument mainDoc = await web.LoadFromWebAsync(mainPage, cancellationToken);
            HtmlNode mainTournamentsNode = mainDoc.GetElementbyId("turnir");
            IEnumerable<HtmlNode> tournamentNodes = mainTournamentsNode.Descendants("a")
                .Where(node => 
                !node.InnerHtml.Contains("img")
                && node.GetHref().Contains("Competition.asp?ZoneId="));

            List<Tournament> result = new List<Tournament>();
            foreach (HtmlNode tourNode in tournamentNodes)
            {
                string href = tourNode.GetHref();
                int equalsIndex = href.IndexOf('=');
                string id = href.Substring(equalsIndex + 1);

                string name = HttpUtility.HtmlDecode(tourNode.InnerHtml);
                string tournamentLink = GetTournamentPageUrl(id);

                List<Category> categories = await this.GetCategoriesAsync(id, cancellationToken);

                Tournament newTournament = new Tournament(id, name, true, categories.ToArray(), tournamentLink);
                result.Add(newTournament);
            }

            return result;
        }

        public async Task<List<Category>> GetCategoriesAsync(string tournamentId, CancellationToken cancellationToken)
        {
            HtmlWeb web = new HtmlWeb();
            string tournamentPage = GetTournamentPageUrl(tournamentId);
            HtmlDocument tournamentDoc = await web.LoadFromWebAsync(tournamentPage, cancellationToken);

            HtmlNode categoriesSelector = tournamentDoc.GetElementbyId("SubZoneId");
            IEnumerable<HtmlNode> categoriesOptions = categoriesSelector.Descendants("option").Where(option => option.GetAttributeValue("value", 0) != 0);

            List<Category> categories = new List<Category>();
            foreach(HtmlNode categoryOption in categoriesOptions)
            {
                string displayName = HttpUtility.HtmlDecode(categoryOption.InnerText).TrimEnd();
                int categoryId = categoryOption.GetAttributeValue("value", 0);
                string webPage = GetCategoryPageUrl(categoryId, tournamentId);

                List<Team> teams = await this.GetTeamsAsync(tournamentId, categoryId, displayName, cancellationToken);
                List<Pool> pools = this.poolsDraw.SetupPools(teams);
                Category newCategory = new Category(displayName, categoryId, pools.ToArray(), teams.Count, webPage);
                categories.Add(newCategory);
            }

            return categories;
        }

        private static string GetTournamentPageUrl(string tournamentId)
        {
            return string.Format("{0}/Competition.asp?ZoneId={1}", IVABaseUrl, tournamentId);
        }

        public async Task<List<Team>> GetTeamsAsync(string tournamentId, int categoryId, string categoryName, CancellationToken cancellationToken)
        {
            List<Player> players = await this.GetPlayersAsync(tournamentId, categoryId, categoryName, cancellationToken);

            List<Team> teams = new List<Team>();
            for (int i = 0; i < players.Count / 2; i++)
            {
                var player1 = players[i * 2];
                var player2 = players[i * 2 + 1];
                var newTeam = new Team(player1, player2);
                teams.Add(newTeam);
            }

            return teams;
        }

        public async Task<List<Player>> GetPlayersAsync(string tournamentId, int categoryId, string categoryName, CancellationToken cancellationToken)
        {
            List<HtmlNode> htmlPlayers = await GetTournamentPlayersNodes(categoryId, tournamentId, cancellationToken);

            IRanksMap ranks = null;
            IRanksMap previousYearRanks = null;
            if (htmlPlayers.Any())
            {
                Gender gender = GetGender(categoryName);
                AgeGroup ageGroup = GetAgeGroup(categoryName);

                ranks = await this.rankDb.GetRanksMapAsync(DateTime.UtcNow.Year, gender, ageGroup, cancellationToken);
                previousYearRanks = await this.rankDb.GetRanksMapAsync(2018, gender, ageGroup, cancellationToken);
            }

            List<Player> players = new List<Player>();
            for (int i = 0; i < htmlPlayers.Count; i++)
            {
                HtmlNode playerRowNode = htmlPlayers[i];
                HtmlNode playerNameNode = playerRowNode.Descendants("a").FirstOrDefault();

                if (playerNameNode == null)
                {
                    continue;
                }

                string name = HttpUtility.HtmlDecode(playerNameNode.InnerText);
                string playerId = playerNameNode.Attributes["href"].Value.Split('(')[1].Split(')')[0];

                int associationIndex = i % 2 == 0 ? 2 : 1;
                var columnDes = playerRowNode.Descendants("td").ToList();
                string association = HttpUtility.HtmlDecode(columnDes[associationIndex].InnerText);
                int age = int.Parse(HttpUtility.HtmlDecode(columnDes[associationIndex + 1].InnerText));

                double rank = ranks.GetRank(name, association, age);
                double previousYearRank = previousYearRanks.GetRank(name, association, age);


                Player player = new Player(name, playerId, rank, previousYearRank, association, age);
                players.Add(player);
            }

            return players;
        }

        private static Gender GetGender(string categoryDisplayName)
        {
            switch (categoryDisplayName)
            {
                case "נשים רמה א'":
                    return Gender.Female;
                case "גברים רמה א'":
                    return Gender.Male;
                case "גברים רמה ב'":
                    return Gender.Male;
                case "נשים רמה ב'":
                    return Gender.Female;
                case "נוער בנים":
                    return Gender.Male;
                case "נוער בנות":
                    return Gender.Female;
                case "נערים":
                    return Gender.Male;
                case "נערות":
                    return Gender.Female;
                default:
                    throw new Exception("Unknown category name: {0}" + categoryDisplayName);
            }
        }

        private static AgeGroup GetAgeGroup(string categoryDisplayName)
        {
            switch (categoryDisplayName)
            {
                case "נשים רמה א'":
                    return AgeGroup.Matures;
                case "גברים רמה א'":
                    return AgeGroup.Matures;
                case "גברים רמה ב'":
                    return AgeGroup.Matures;
                case "נשים רמה ב'":
                    return AgeGroup.Matures;
                case "נוער בנים":
                    return AgeGroup.Youth;
                case "נוער בנות":
                    return AgeGroup.Youth;
                case "נערים":
                    return AgeGroup.Youth;
                case "נערות":
                    return AgeGroup.Youth;
                default:
                    throw new Exception("Unknown category name: {0}" + categoryDisplayName);
            }
        }

        private static async Task<List<HtmlNode>> GetTournamentPlayersNodes(int categoryId, string tournamentId, CancellationToken cancellationToken)
        {
            HtmlWeb web = new HtmlWeb();
            string categoryPageUrl = GetCategoryPageUrl(categoryId, tournamentId);
            HtmlDocument categoryDoc = await web.LoadFromWebAsync(categoryPageUrl, cancellationToken);
            HtmlNode playersTable = categoryDoc.GetElementbyId("TeamRoster");
            List<HtmlNode> htmlPlayers = playersTable.Descendants("tr").ToList();
            htmlPlayers.RemoveRange(0, 1);
            return htmlPlayers;
        }

        private static string GetCategoryPageUrl(int categoryId, string tournamentId)
        {
            return string.Format("{0}/Competition.asp?SubZoneId={1}&ZoneId={2}", IVABaseUrl, categoryId, tournamentId);
        }

        public async Task<IvaPlayer> GetIvaPlayerAsync(string playerId)
        {
            HttpClient httpClient = new HttpClient();
            string url = string.Format("{0}/json_player_data.asp?PlayerId={1}", IVABaseUrl, playerId);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<IvaPlayer>>(content);
            IvaPlayer player = players.First();

            const string inlineLinkPrefix = "../";
            bool shouldAddIva = false;
            string newLink = player.pic_profile_web;
            while (newLink.StartsWith(inlineLinkPrefix))
            {
                newLink = newLink.Substring(inlineLinkPrefix.Length);
                shouldAddIva = true;
            }

            if (shouldAddIva)
            {
                if (!newLink.StartsWith("/"))
                {
                    newLink = newLink.Insert(0, "/");
                }

                newLink = newLink.Insert(0, IVABaseUrl);
                player.pic_profile_web = newLink;
            }

            return player;
        }
    }
}
