using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    internal static class YieldCache
    {
        private class FloatComparer : IEqualityComparer<float>
        {
            bool IEqualityComparer<float>.Equals(float x, float y)
            {
                return x == y;
            }

            int IEqualityComparer<float>.GetHashCode(float obj)
            {
                return obj.GetHashCode();
            }
        }

        public static readonly WaitForEndOfFrame WaitForEndOfFrame = new();
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new();

        private static readonly Dictionary<float, WaitForSeconds> TimeInterval = new(new FloatComparer());

        private static readonly Dictionary<float, WaitForSecondsRealtime> TimeIntervalReal = new(new FloatComparer());

        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            WaitForSeconds wfs;
            if (!TimeInterval.TryGetValue(seconds, out wfs))
                TimeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
            return wfs;
        }

        public static WaitForSecondsRealtime WaitForSecondsRealTime(float seconds)
        {
            WaitForSecondsRealtime wfsReal;
            if (!TimeIntervalReal.TryGetValue(seconds, out wfsReal))
                TimeIntervalReal.Add(seconds, wfsReal = new WaitForSecondsRealtime(seconds));
            return wfsReal;
        }
    }
}