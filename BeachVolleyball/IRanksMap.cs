//---------------------------------------------------------------------------------------------------------------------
// <copyright file="IRanksMap.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    public interface IRanksMap
    {
        int Year { get; }

        double GetRank(string playerName, string association, int age);
    }
}
