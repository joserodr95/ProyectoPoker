using System.Linq;

namespace Helpers
{
    /// <summary>
    /// Class that holds methods that extend the string class.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Turn a string into sentence format.
        /// <br/><br/>
        /// For example: "This is all." --> "This Is All."
        /// </summary>
        /// <param name="input">The string that will be transformed.</param>
        /// <returns>The transformed string.</returns>
        public static string ToSentence(this string input)
        {
            return new string(input.SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new[] { ' ', c } : new[] { c }).ToArray());
        }
    }
}