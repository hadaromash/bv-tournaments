//---------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultCosmosDbClient.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace Engine.CosmosDb
{
    using BeachVolleyball;
    using Microsoft.Azure.Cosmos;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    public class DefaultCosmosDbClient : ITournamentsCosmosDbClient
    {
        /// The Azure Cosmos DB endpoint for running this GetStarted sample.
        private readonly string EndpointUrl = @"https://bv-tournaments.documents.azure.com:443/";

        /// The primary key for the Azure DocumentDB account.
        private readonly string PrimaryKey = "WiXLjpp2pY5HanpRuifCjpJo1KnTrUFlE1lVpfsOFC3s5dqWuC8MvpVDyjp5FZ9tE29YMPpqBultIiAHgpCeXg==";

        private readonly CosmosClient cosmosClient;
        private Database database;
        private Container container;

        private string databaseId = "TournamentsDatabase";
        private string containerId = "TournamentsContainer";

        public DefaultCosmosDbClient()
        {
            this.cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);
        }

        public async Task InitAsync()
        {
            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync();
        }

        private async Task CreateDatabaseAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);
        }

        private async Task CreateContainerAsync()
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/Name");
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }

        public async Task AddItemAsync(Tournament tournament)
        {
            try
            {
                // Read the item to see if it exists. ReadItemAsync will throw an exception if the item does not exist and return status code 404 (Not found).
                ItemResponse<Tournament> andersenFamilyResponse = await this.container.ReadItemAsync<Tournament>(tournament.Id, new PartitionKey(tournament.Name));
                Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the tournament.
                ItemResponse<Tournament> tournamentResponse = await this.container.CreateItemAsync(tournament, new PartitionKey(tournament.Name));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", tournamentResponse.Resource.Id, tournamentResponse.RequestCharge);
            }
        }

        public async Task<List<Tournament>> GetAllItemsAsync()
        {
            var sqlQueryText = "SELECT * FROM c";
            return await GetTournaments(sqlQueryText);
        }

        public async Task<List<Tournament>> GetAllActiveTournamentsAsync()
        {
            var sqlQueryText = "SELECT * FROM c where c.IsActive='True'";
            return await GetTournaments(sqlQueryText);
        }

        private async Task<List<Tournament>> GetTournaments(string sqlQueryText)
        {
            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Tournament> queryResultSetIterator = this.container.GetItemQueryIterator<Tournament>(queryDefinition);

            List<Tournament> tournaments = new List<Tournament>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Tournament> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Tournament tournament in currentResultSet)
                {
                    tournaments.Add(tournament);
                    Console.WriteLine("\tRead {0}\n", tournament);
                }
            }

            return tournaments;
        }

        public async Task UpdateItemAsync(Tournament tournament)
        {
            // replace the item with the updated content
            ItemResponse<Tournament> itemResponse = await this.container.ReplaceItemAsync(tournament, tournament.Id, new PartitionKey(tournament.Name));
            Console.WriteLine("Updated Family [{0},{1}].\n \tBody is now: {2}\n", tournament.Name, tournament.Id, itemResponse.Resource);

        }
    }
}
