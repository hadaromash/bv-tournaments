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
    using System.Threading;
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

        public async Task<List<Tournament>> GetAllTournamentsAsync(CancellationToken cancellationToken)
        {
            await this.tournamentsCosmosDbClient.InitAsync(cancellationToken);
            return await this.tournamentsCosmosDbClient.GetAllActiveTournamentsAsync(cancellationToken);
        }

        public async Task UpdateTournamentsAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            List<Tournament> tournamentsFromCrawler = await this.beachVolleyDb.GetTournamentsAsync(cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await this.tournamentsCosmosDbClient.InitAsync(cancellationToken);
            List<Tournament> tournamentsFromDb = await this.tournamentsCosmosDbClient.GetAllActiveTournamentsAsync(cancellationToken);

            TournamentComparer comparer = new TournamentComparer();
            IEnumerable<Tournament> onlyInCrawler = tournamentsFromCrawler.Except(tournamentsFromDb, comparer);
            IEnumerable<Tournament> inBoth = tournamentsFromCrawler.Intersect(tournamentsFromDb, comparer);
            IEnumerable<Tournament> onlyInDb = tournamentsFromDb.Except(tournamentsFromCrawler, comparer);

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            foreach(Tournament tour in onlyInCrawler)
            {
                try
                {
                    await this.tournamentsCosmosDbClient.AddItemAsync(tour, cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to add tournament: {0}, exception: {1}", tour, e);
                }
            }

            foreach(Tournament tour in inBoth)
            {
                await UpdateTournament(tour, cancellationToken);
            }

            foreach (Tournament tournament in onlyInDb)
            {
                tournament.IsActive = false;
                await UpdateTournament(tournament, cancellationToken);
            }
        }

        private async Task UpdateTournament(Tournament tournament, CancellationToken cancellationToken)
        {
            try
            {
                await this.tournamentsCosmosDbClient.UpdateItemAsync(tournament, cancellationToken);
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
