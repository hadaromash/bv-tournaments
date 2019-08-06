//---------------------------------------------------------------------------------------------------------------------
// <copyright file="IPoolsDraw.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPoolsDraw
    {
        List<Pool> SetupPools(List<Team> teams);
    }
}
