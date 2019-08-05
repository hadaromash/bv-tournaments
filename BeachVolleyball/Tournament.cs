//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Tournament.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace BeachVolleyball
{
    public class Tournament
    {
        public Tournament(int tournamentId, string name, bool isActive, Category[] categories)
        {
            this.Id = tournamentId;
            this.Name = name;
            this.IsActive = isActive;
            this.Categories = categories;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        public string Name { get; }

        public bool IsActive { get; }

        public Category[] Categories { get; }

        public override string ToString()
        {
            return string.Format("Tournament name: {0}, id: {1}", this.Name, this.Id);
        }
    }
}
