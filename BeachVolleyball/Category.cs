//---------------------------------------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BeachVolleyball
{
    public class Category
    {
        public Category(string displayName, int categoryId, int tournamentId)
        {
            DisplayName = displayName;
            Id = categoryId;
            TournamentId = tournamentId;
        }

        public string DisplayName { get; }

        public int Id { get; }

        public int TournamentId { get; }
    }
}
