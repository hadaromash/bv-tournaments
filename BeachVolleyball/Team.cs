﻿//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Team.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    public class Team
    {
        public Team(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;

            this.Rank = player1.Rank + player2.Rank;
            this.PreviousYearRank = player1.PreviousYearRank + player2.PreviousYearRank;
        }

        public Player Player1 { get; private set; }

        public Player Player2 { get; private set; }

        public double Rank { get; private set; }

        public double PreviousYearRank { get; private set; }

        public override string ToString()
        {
            return string.Format("({0}, {1} \t2019 Rank: {2}, 2018 Rank: {3})", this.Player1, this.Player2, this.Rank, this.PreviousYearRank);
        }
    }
}
