//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using HtmlAgilityPack;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Player
    {
        public Player(string name, string playerId, double rank, double previousYearRank, string association, int age)
        {
            this.Name = name;
            this.PlayerId = playerId;
            this.Rank = rank;
            this.PreviousYearRank = previousYearRank;
            this.Association = association;
            this.Age = age;
        }

        public string Name { get; private set; }

        public string PlayerId { get; private set; }

        public double Rank { get; private set; }

        public double PreviousYearRank { get; private set; }

        public string Association { get; private set; }

        public int Age { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.Name, this.Rank);
        }
    }
}
