//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    public class Category
    {
        public Category(string displayName, int id, Pool[] pools, int teamsNumber, string webPage)
        {
            this.DisplayName = displayName;
            this.Id = id;
            this.Pools = pools;
            this.TeamsNumber = teamsNumber;
            WebPage = webPage;
        }

        public string DisplayName { get; }

        public int Id { get; }

        public Pool[] Pools { get; }

        public int TeamsNumber { get; }

        public string WebPage { get; }
    }
}
