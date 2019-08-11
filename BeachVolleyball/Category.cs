//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    public class Category
    {
        public Category(string displayName, int id, Pool[] pools, int teamsNumber)
        {
            this.DisplayName = displayName;
            this.Id = id;
            this.Pools = pools;
            this.TeamsNumber = teamsNumber;
        }

        public string DisplayName { get; }

        public int Id { get; }

        public Pool[] Pools { get; }

        public int TeamsNumber { get; }
    }
}
