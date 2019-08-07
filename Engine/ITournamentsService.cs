//---------------------------------------------------------------------------------------------------------------------
// <copyright file="ITournamentsService.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace Engine
{
    using BeachVolleyball;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITournamentsService
    {
        Task UpdateTournamentsAsync(CancellationToken cancellationToken);

        Task<List<Tournament>> GetAllTournamentsAsync(CancellationToken cancellationToken);
    }
}
