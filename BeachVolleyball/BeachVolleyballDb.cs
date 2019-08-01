//---------------------------------------------------------------------------------------------------------------------
// <copyright file="BeachVolleyballDb.cs" company="Microsoft">
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

    public class BeachVolleyballDb : IBeachVolleyDb
    {
        private const string IVABaseUrl = "http://www.iva.org.il";

        public async Task<List<Tournament>> GetTournamentsAsync()
        {
            HtmlWeb web = new HtmlWeb();
            string mainPage = IVABaseUrl + "/default.asp";
            HtmlDocument mainDoc = await web.LoadFromWebAsync(mainPage);
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

                Tournament newTournament = new Tournament(int.Parse(id), name);
                result.Add(newTournament);
            }

            return result;
        }

        public async Task<List<Category>> GetCategoriesAsync(int tournamentId)
        {
            HtmlWeb web = new HtmlWeb();
            string tournamentPage = GetTournamentPageUrl(tournamentId);
            HtmlDocument tournamentDoc = await web.LoadFromWebAsync(tournamentPage);

            HtmlNode categoriesSelector = tournamentDoc.GetElementbyId("SubZoneId");
            IEnumerable<HtmlNode> categoriesOptions = categoriesSelector.Descendants("option").Where(option => option.GetAttributeValue("value", 0) != 0);

            List<Category> categories = new List<Category>();
            foreach(HtmlNode categoryOption in categoriesOptions)
            {
                string displayName = HttpUtility.HtmlDecode(categoryOption.InnerText).TrimEnd();
                int categoryId = categoryOption.GetAttributeValue("value", 0);
                Category newCategory = new Category(displayName, categoryId, tournamentId);
                categories.Add(newCategory);
            }

            return categories;
        }

        private static string GetTournamentPageUrl(int tournamentId)
        {
            return string.Format("{0}/Competition.asp?ZoneId={1}", IVABaseUrl, tournamentId);
        }

        public async Task<List<Team>> GetTeamsAsync(int tournamentId, int categoryId, string categoryName)
        {
            List<Player> players = await this.GetPlayersAsync(tournamentId, categoryId, categoryName);

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

        public async Task<List<Player>> GetPlayersAsync(int tournamentId, int categoryId, string categoryName)
        {
            List<HtmlNode> htmlPlayers = await GetTournamentPlayersNodes(categoryId, tournamentId);

            IRanksMap ranks = await RanksMap.CreateAsync(2019, categoryName);
            IRanksMap previousYearRanks = await RanksMap.CreateAsync(2018, categoryName);

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

        private static async Task<List<HtmlNode>> GetTournamentPlayersNodes(int categoryId, int tournamentId)
        {
            HtmlWeb web = new HtmlWeb();
            string tournamentWebPage = GetTournamentPageUrl(categoryId, tournamentId);
            HtmlDocument tournamentDoc = await web.LoadFromWebAsync(tournamentWebPage);
            HtmlNode playersTable = tournamentDoc.GetElementbyId("TeamRoster");
            List<HtmlNode> htmlPlayers = playersTable.Descendants("tr").ToList();
            htmlPlayers.RemoveRange(0, 1);
            return htmlPlayers;
        }

        private static string GetTournamentPageUrl(int categoryId, int tournamentId)
        {
            return string.Format("{0}/Competition.asp?SubZoneId={1}&ZoneId={2}", IVABaseUrl, categoryId, tournamentId);
        }
    }
}
