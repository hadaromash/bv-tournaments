//---------------------------------------------------------------------------------------------------------------------
// <copyright file="WebCrawlerRankDb.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball.Ranks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class WebCrawlerRankDb : IRankDb
    {
        Dictionary<string, IRanksMap> rankMaps = new Dictionary<string, IRanksMap>();

        public async Task<IRanksMap> GetRanksMapAsync(int year, Gender gender, AgeGroup ageGroup)
        {
            string key = CreateKey(year, gender, ageGroup);
            if (rankMaps.TryGetValue(key, out IRanksMap value))
            {
                return value;
            }

            IRanksMap newRankMap = await RanksMap.CreateAsync(year, gender, ageGroup);
            rankMaps.Add(key, newRankMap);
            return newRankMap;
        }

        private string CreateKey(int year, Gender gender, AgeGroup ageGroup)
        {
            return string.Format("{0}-{1}-{2}", year, (int)gender, (int)ageGroup);
        }
    }
}
