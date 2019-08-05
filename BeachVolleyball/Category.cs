//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    public class Category
    {
        public Category(string displayName, int categoryId, Pool[] pools)
        {
            this.DisplayName = displayName;
            this.Id = categoryId;
            this.Pools = pools;
        }

        public string DisplayName { get; }

        public int Id { get; }

        public Pool[] Pools { get; }
    }
}
