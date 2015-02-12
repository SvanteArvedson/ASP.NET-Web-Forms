using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Hur_manga_versaler.Model
{
    /// <summary>
    /// Contains methods for analyzing strings.
    /// </summary>
    public static class TextAnalyzer
    {
        /// <summary>
        /// Returns the number of capitals in a string.
        /// </summary>
        /// <param name="text">The sring to be analyzed.</param>
        /// <returns>Number of capitals in Text.</returns>
        public static int GetNumberOfCapitals(string text)
        {
            Regex rgx = new Regex(@"[^A-ZÅÄÖ]");
            string capitalsInText = rgx.Replace(text, "");

            return capitalsInText.Length;
        }
    }
}