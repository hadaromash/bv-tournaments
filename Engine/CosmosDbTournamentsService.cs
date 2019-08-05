//---------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentsService.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace Engine
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BeachVolleyball;

    public class CosmosDbTournamentsService : ITournamentsService
    {
        private readonly IBeachVolleyDb beachVolleyDb;

        public CosmosDbTournamentsService(IBeachVolleyDb beachVolleyDb)
        {
            this.beachVolleyDb = beachVolleyDb;
        }

        public Task<List<Tournament>> GetAllTournamentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTournamentsAsync()
        {
            List<Tournament> tournaments = await this.beachVolleyDb.GetTournamentsAsync();
        }
    }
}
