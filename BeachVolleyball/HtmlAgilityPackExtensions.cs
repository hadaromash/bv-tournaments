//---------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlAgilityPackExtensions.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------------------------------------

namespace BeachVolleyball
{
    using HtmlAgilityPack;

    internal static class HtmlAgilityPackExtensions
    {
        public static string GetHref(this HtmlNode node)
        {
            return node.GetAttributeValue("href", string.Empty);
        }
    }
}
