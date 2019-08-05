//---------------------------------------------------------------------------------------------------------------------
// <copyright file="ICosmosDbClient.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace Engine.CosmosDb
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICosmosDbClient<T>
    {
        Task InitAsync();

        Task AddItemAsync(T item);

        Task<List<T>> GetAllItemsAsync();

        Task UpdateItemAsync(T item);
    }
}
