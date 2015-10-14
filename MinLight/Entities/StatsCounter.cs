using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinLight.Entities
{
    using System.Threading;

    public class StatsCounter
    {
        public static long RaysTraced;

        public static void RayTraced()
        {
            Interlocked.Increment(ref RaysTraced);
        }

        public static void Reset()
        {
            RaysTraced = 0;
        }

    }
}
