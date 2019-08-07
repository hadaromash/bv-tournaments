//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Pool.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using System.Collections.Generic;
    using System.Text;

    public class Pool
    {
        public Pool(int number, int teamsNumber)
        {
            this.Number = number;
            this.TeamsNumber = teamsNumber;
            this.Teams = new List<Team>();
            this.QualificationMatches = new List<QualificationMatch>();
        }

        public int Number { get; }

        public int TeamsNumber { get; }

        public List<Team> Teams { get; }

        public List<QualificationMatch> QualificationMatches { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("---Pool number {0}---", this.Number));
            foreach (Team team in this.Teams)
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString();
        }
    }
}
