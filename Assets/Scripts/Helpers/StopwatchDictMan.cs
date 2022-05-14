using System.Collections.Generic;
using System.Diagnostics;

namespace Helpers
{
    /// <summary>
    /// Class to manage multiple stopwatches with a dictionary identifying by id.
    /// </summary>
    public class StopwatchDictMan : Stopwatch
    {

        private static readonly Dictionary<string, Stopwatch> dict = new Dictionary<string, Stopwatch>();

        /// <summary>
        /// Starts a Stopwatch or resumes one.
        /// </summary>
        /// <param name="id">The name that will be used in the dictionary to identify the Stopwatch</param>
        /// <param name="resume">When a Stopwatch with the same name exists if this is false
        /// it will create a new Stopwatch with the same name else it will resume the existing Stopwatch
        /// (FALSE by default).</param>
        public static void Start(string id, bool resume = false)
        {
            if (resume && dict.ContainsKey(id))
            {
                Resume(id);
            }
            else
            {
                dict.Remove(id);
                dict.Add(id, StartNew());
            }
        }

        /// <summary>
        /// Stops a Stopwatch.
        /// </summary>
        /// <param name="id">The name that identifies the Stopwatch.</param>
        /// <param name="debug">If true it will output a message with the id of the Stopwatch
        /// and it's elapsed time in milliseconds and ticks
        /// (TRUE by default).</param>
        public static void End(string id, bool debug = true)
        {
            dict[id].Stop();
            if (debug) UnityEngine.Debug.Log($"{id} ms: {dict[id].ElapsedMilliseconds}, ticks: {dict[id].ElapsedTicks}");
        }

        /// <summary>
        /// Start an existing Stopwatch.
        /// </summary>
        /// <param name="id">The name of the existing Stopwatch that will start.</param>
        private static void Resume(string id)
        {
            dict[id].Start();
        }


    }
}