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
        Task<List<Tournament>> GetTournaments();
    }
}
