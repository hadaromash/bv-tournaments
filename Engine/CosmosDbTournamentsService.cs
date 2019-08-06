//---------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentsService.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BeachVolleyball;
    using Engine.CosmosDb;

    public class CosmosDbTournamentsService : ITournamentsService
    {
        private readonly IBeachVolleyDb beachVolleyDb;
        private readonly ITournamentsCosmosDbClient tournamentsCosmosDbClient;

        public CosmosDbTournamentsService(IBeachVolleyDb beachVolleyDb, ITournamentsCosmosDbClient tournamentsCosmosDbClient)
        {
            this.beachVolleyDb = beachVolleyDb;
            this.tournamentsCosmosDbClient = tournamentsCosmosDbClient;
        }

        public async Task<List<Tournament>> GetAllTournamentsAsync()
        {
            await this.tournamentsCosmosDbClient.InitAsync();
            return await this.tournamentsCosmosDbClient.GetAllActiveTournamentsAsync();
        }

        public async Task UpdateTournamentsAsync()
        {
            List<Tournament> tournamentsFromCrawler = await this.beachVolleyDb.GetTournamentsAsync();

            await this.tournamentsCosmosDbClient.InitAsync();
            List<Tournament> tournamentsFromDb = await this.tournamentsCosmosDbClient.GetAllActiveTournamentsAsync();

            TournamentComparer comparer = new TournamentComparer();
            IEnumerable<Tournament> onlyInCrawler = tournamentsFromCrawler.Except(tournamentsFromDb, comparer);
            IEnumerable<Tournament> inBoth = tournamentsFromCrawler.Intersect(tournamentsFromDb, comparer);
            IEnumerable<Tournament> onlyInDb = tournamentsFromDb.Except(tournamentsFromCrawler, comparer);

            foreach(Tournament tour in onlyInCrawler)
            {
                try
                {
                    await this.tournamentsCosmosDbClient.AddItemAsync(tour);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to add tournament: {0}, exception: {1}", tour, e);
                }
            }

            foreach(Tournament tour in inBoth)
            {
                await UpdateTournament(tour);
            }

            foreach (Tournament tournament in onlyInDb)
            {
                tournament.IsActive = false;
                await UpdateTournament(tournament);
            }
        }

        private async Task UpdateTournament(Tournament tournament)
        {
            try
            {
                await this.tournamentsCosmosDbClient.UpdateItemAsync(tournament);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to update tournament: {0}, exception: {1}", tournament, e);
            }
        }

        private class TournamentComparer : IEqualityComparer<Tournament>
        {
            public bool Equals(Tournament x, Tournament y)
            {
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(Tournament obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
