//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Tournament.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class Tournament
    {
        public Tournament(int tournamentId, string name)
        {
            this.TournamentId = tournamentId;
            this.Name = name;
        }

        public Category Category { get; private set; }

        public int TournamentId { get; private set; }

        public string Name { get; private set; }

        public List<Team> Teams { get; private set; }

        public List<Pool> Pools { get; private set; }

        public async Task InitPools(Category category)
        {
            this.Category = Category;
            this.Teams = await CreateTeamsList(category, this.TournamentId);
            this.Pools = SetPoolsDraw(this.Teams);
        }

        private static async Task<List<Team>> CreateTeamsList(Category category, int tournamentId)
        {
            List<Player> players = await CreatePlayersListAsync(category, tournamentId);

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

        private static async Task<List<Player>> CreatePlayersListAsync(Category category, int tournamentId)
        {
            List<HtmlNode> htmlPlayers = await GetTournamentPlayersNodes(category, tournamentId);
            IRanksMap ranks = await RanksMap.CreateAsync(2019, category);
            IRanksMap previousYearRanks = await RanksMap.CreateAsync(2018, category);

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

        private static async Task<List<HtmlNode>> GetTournamentPlayersNodes(Category category, int tournamentId)
        {
            HtmlWeb web = new HtmlWeb();
            string tournamentWebPage = GetTournamentPageUrl(category, tournamentId);
            HtmlDocument tournamentDoc = await web.LoadFromWebAsync(tournamentWebPage);
            HtmlNode playersTable = tournamentDoc.GetElementbyId("TeamRoster");
            List<HtmlNode> htmlPlayers = playersTable.Descendants("tr").ToList();
            htmlPlayers.RemoveRange(0, 2);
            return htmlPlayers;
        }

        private static string GetTournamentPageUrl(Category category, int tournamentId)
        {
            int levelId = GetLevelId(category);
            return string.Format("http://www.iva.org.il/Competition.asp?SubZoneId={0}&ZoneId={1}", levelId, tournamentId);
        }

        private static int GetLevelId(Category category)
        {
            switch (category)
            {
                case Category.MenA:
                    return 1192;
                case Category.MenB:
                    return 1195;
                case Category.WomenA:
                    return 1193;
                case Category.WomenB:
                    return 1194;
                case Category.YouthMen:
                    return 1218;
                case Category.YouthWomen:
                    return 1219;
                default: throw new Exception();
            }
        }

        private static List<Pool> SetPoolsDraw(List<Team> teams)
        {
            if (teams.Count >= 32)
            {
                return SetPoolsDraw(teams, 8, 4);
            }

            if (teams.Count >= 24)
            {
                return SetPoolsDraw(teams, 8, 3);
            }

            if (teams.Count >= 16)
            {
                return SetPoolsDraw(teams, 4, 4);
            }

            if (teams.Count >= 12)
            {
                return SetPoolsDraw(teams, 4, 3);
            }

            if (teams.Count >= 8)
            {
                return SetPoolsDraw(teams, 2, 4);
            }

            if (teams.Count >= 6)
            {
                return SetPoolsDraw(teams, 2, 3);
            }

            if (teams.Count >= 4)
            {
                return SetPoolsDraw(teams, 2, 2);
            }

            return SetPoolsDraw(teams, 1, 3);
        }

        private static List<Pool> SetPoolsDraw(List<Team> teams, int numOfPools, int numOfTeamsInPool)
        {
            teams = teams.OrderByDescending(team => team.Rank).ThenByDescending(team => team.PreviousYearRank).ToList();
            List<Pool> pools = new List<Pool>();
            for (int i = 0; i < numOfPools; i++)
            {
                pools.Add(new Pool(i + 1, numOfTeamsInPool));
            }

            bool goForward = true;
            int currPoolIndex = 0;
            for (int i = 0; i < teams.Count; i++)
            {
                pools[currPoolIndex].Teams.Add(teams[i]);
                if (goForward)
                {
                    if (currPoolIndex == numOfPools - 1)
                    {
                        goForward = false;
                    }
                    else
                    {
                        currPoolIndex++;
                    }
                }
                else
                {
                    if (currPoolIndex == 0)
                    {
                        goForward = true;
                    }
                    else
                    {
                        currPoolIndex--;
                    }
                }
            }

            UpdateQualificationMatches(pools);

            return pools;
        }

        private static void UpdateQualificationMatches(List<Pool> pools)
        {
            foreach (Pool pool in pools)
            {
                int extras = pool.Teams.Count - pool.TeamsNumber;
                for (int i = 0; i < extras; i++)
                {
                    Team team1 = pool.Teams[pool.TeamsNumber - extras];
                    Team team2 = pool.Teams.Last();

                    QualificationMatch qualificationMatch = new QualificationMatch(team1, team2);
                    pool.QualificationMatches.Add(qualificationMatch);

                    pool.Teams.Remove(team1);
                    pool.Teams.Remove(team2);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("***{0} - Pools for category: {1}***", this.Name, this.Category));
            sb.AppendLine();
            foreach (Pool pool in this.Pools)
            {
                sb.AppendLine(pool.ToString());
            }

            return sb.ToString();
        }
    }
}
