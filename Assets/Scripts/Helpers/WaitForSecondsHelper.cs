using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// Helps waiting for seconds without creating a new WaitForSeconds every time.
    /// </summary>
    public class WaitForSecondsHelper
    {
        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary 
            = new Dictionary<float, WaitForSeconds>();
        
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out WaitForSeconds wait)) return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }
    }
}