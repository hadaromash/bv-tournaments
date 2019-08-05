//---------------------------------------------------------------------------------------------------------------------
// <copyright file="ITournamentsCosmosDbClient.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace Engine.CosmosDb
{
    using BeachVolleyball;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITournamentsCosmosDbClient : ICosmosDbClient<Tournament>
    {
        Task<List<Tournament>> GetAllActiveTournamentsAsync();
    }
}
