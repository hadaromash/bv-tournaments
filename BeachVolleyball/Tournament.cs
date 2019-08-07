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
        public Tournament(string id, string name, bool isActive, Category[] categories)
        {
            this.Id = id;
            this.Name = name;
            this.IsActive = isActive;
            this.Categories = categories;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; }

        public string Name { get; }

        public bool IsActive { get; set; }

        public Category[] Categories { get; }

        public override string ToString()
        {
            return string.Format("Tournament name: {0}, id: {1}", this.Name, this.Id);
        }
    }
}
