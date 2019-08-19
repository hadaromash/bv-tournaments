//---------------------------------------------------------------------------------------------------------------------
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
        }

        public Player Player1 { get; }

        public Player Player2 { get; }

        public double Rank => this.Player1.Rank + this.Player2.Rank;

        public double PreviousYearRank => this.Player1.PreviousYearRank + this.Player2.PreviousYearRank;

        public override string ToString()
        {
            return string.Format("({0}, {1} \t2019 Rank: {2}, 2018 Rank: {3})", this.Player1, this.Player2, this.Rank, this.PreviousYearRank);
        }
    }
}
