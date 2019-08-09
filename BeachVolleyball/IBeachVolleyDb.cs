//---------------------------------------------------------------------------------------------------------------------
// <copyright file="IBeachVolleyDb.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBeachVolleyDb
    {
        Task<List<Tournament>> GetTournamentsAsync(CancellationToken cancellationToken);

        Task<List<Category>> GetCategoriesAsync(string tournamentId, CancellationToken cancellationToken);

        Task<List<Team>> GetTeamsAsync(string tournamentId, int categoryId, string categoryName, CancellationToken cancellationToken);

        Task<List<Player>> GetPlayersAsync(string tournamentId, int categoryId, string categoryName, CancellationToken cancellationToken);

        Task<IvaPlayer> GetIvaPlayerAsync(string playerId);
    }
}
