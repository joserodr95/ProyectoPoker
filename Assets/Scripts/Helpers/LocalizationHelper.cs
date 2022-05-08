using UnityEngine.Localization;

namespace Helpers
{
    
    /// <summary>
    /// Helps using the localization class in scripting.
    /// </summary>
    public static class LocalizationHelper
    {
        private static LocalizedString stringRef = new LocalizedString() { TableReference = "Other", TableEntryReference = "Winner" };
        private static string _table = "All";

        /// <summary>
        /// Changes the default table.
        /// </summary>
        /// <param name="s">The name of the new default table.</param>
        public static void UseTable(string s)
        {
            if (!_table.Equals(s)) _table = s;
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        /// <param name="entry">The key of the entry.</param>
        /// <param name="table">The table where the entry is.</param>
        /// <param name="n">The number that defines if the string will be in singular o some plural.</param>
        /// <returns>The localized string.</returns>
        public static string Get(string entry, string table = "", int n = 1)
        {
            stringRef.TableReference = table == "" ? _table : table;
            stringRef.TableEntryReference = entry;
            stringRef.Arguments = new object[] {n};
            return stringRef.GetLocalizedString();
        }
    }
}