//---------------------------------------------------------------------------------------------------------------------
// <copyright file="ICosmosDbClient.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace Engine.CosmosDb
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICosmosDbClient<T>
    {
        Task InitAsync(CancellationToken cancellationToken);

        Task AddItemAsync(T item, CancellationToken cancellationToken);

        Task<List<T>> GetAllItemsAsync(CancellationToken cancellationToken);

        Task UpdateItemAsync(T item, CancellationToken cancellationToken);
    }
}
