//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Match.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    public class Match
    {
        public Match(Team team1, Team team2)
        {
            this.Team1 = team1;
            this.Team2 = team2;
        }

        public Team Team1 { get; private set; }

        public Team Team2 { get; private set; }
    }
}
