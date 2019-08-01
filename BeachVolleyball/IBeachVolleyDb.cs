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

        Task<List<Category>> GetCategoriesAsync(int tournamentId);

        Task<List<Team>> GetTeamsAsync(int tournamentId, int categoryId, string categoryName);

        Task<List<Player>> GetPlayersAsync(int tournamentId, int categoryId, string categoryName);
    }
}
