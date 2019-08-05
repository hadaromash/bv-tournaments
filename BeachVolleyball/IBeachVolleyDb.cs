//---------------------------------------------------------------------------------------------------------------------
// <copyright file="IBeachVolleyDb.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBeachVolleyDb
    {
        Task<List<Tournament>> GetTournamentsAsync();

        Task<List<Category>> GetCategoriesAsync(string tournamentId);

        Task<List<Team>> GetTeamsAsync(string tournamentId, int categoryId, string categoryName);

        Task<List<Player>> GetPlayersAsync(string tournamentId, int categoryId, string categoryName);
    }
}
