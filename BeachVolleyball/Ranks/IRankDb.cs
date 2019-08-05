//---------------------------------------------------------------------------------------------------------------------
// <copyright file="IRankDb.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace BeachVolleyball
{
    public interface IRankDb
    {
        Task<IRanksMap> GetRanksMapAsync(int year, Gender gender, AgeGroup ageGroup);
    }
}
