//---------------------------------------------------------------------------------------------------------------------
// <copyright file="SnakePoolsDraw.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SnakePoolsDraw : IPoolsDraw
    {
        private readonly IBeachVolleyDb beachVolleyDb;

        public SnakePoolsDraw(IBeachVolleyDb beachVolleyDb)
        {
            this.beachVolleyDb = beachVolleyDb;
        }

        public async Task<List<Pool>> GetPoolsAsync(int tournamentId, int categoryId, string categoryName)
        {
            List<Team> teams = await this.beachVolleyDb.GetTeamsAsync(tournamentId, categoryId, categoryName);

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

            if (teams.Count >= 1)
            {
                return SetPoolsDraw(teams, 1, 3);
            }

            return new List<Pool>();
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
    }
}
