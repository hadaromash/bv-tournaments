//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Tournament.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    public class Tournament
    {
        public Tournament(int tournamentId, string name)
        {
            this.TournamentId = tournamentId;
            this.Name = name;
        }

        public int TournamentId { get; private set; }

        public string Name { get; private set; }

        public override string ToString()
        {
            return string.Format("Tournament name: {0}, id: {1}", this.Name, this.TournamentId);
        }
    }
}
