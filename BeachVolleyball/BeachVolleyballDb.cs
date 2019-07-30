//---------------------------------------------------------------------------------------------------------------------
// <copyright file="BeachVolleyballDb.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    public class BeachVolleyballDb : IBeachVolleyDb
    {
        public async Task<List<Tournament>> GetTournaments()
        {
            HtmlWeb web = new HtmlWeb();
            string mainPage = "http://www.iva.org.il/default.asp";
            HtmlDocument mainDoc = await web.LoadFromWebAsync(mainPage);
            HtmlNode mainTournamentsNode = mainDoc.GetElementbyId("turnir");
            IEnumerable<HtmlNode> tournamentNodes = mainTournamentsNode.Descendants("a")
                .Where(node => 
                !node.InnerHtml.Contains("img")
                && node.GetHref().Contains("Competition.asp?ZoneId="));

            List<Tournament> result = new List<Tournament>();
            foreach (HtmlNode tourNode in tournamentNodes)
            {
                string href = tourNode.GetHref();
                int equalsIndex = href.IndexOf('=');
                string id = href.Substring(equalsIndex + 1);

                string name = HttpUtility.HtmlDecode(tourNode.InnerHtml);

                Tournament newTournament = new Tournament(int.Parse(id), name);
                result.Add(newTournament);
            }

            return result;
        }
    }
}
