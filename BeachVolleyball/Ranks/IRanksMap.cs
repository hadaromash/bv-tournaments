﻿//---------------------------------------------------------------------------------------------------------------------
// <copyright file="IRanksMap.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

using System.Threading;

namespace BeachVolleyball
{
    public interface IRanksMap
    {
        int Year { get; }

        double GetRank(string playerName, string association, int age);
    }
}
