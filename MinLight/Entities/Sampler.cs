using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinLight.Entities
{
    public class Sampler
    {
        public virtual float GetNextSample(int index = 0)
        {
            return 0.0f;
        }

        public virtual Sampler Clone()
        {
            return new Sampler();
        }
    }


    public class DotNetSampler : Sampler
    {
        protected Random rnd;

        public DotNetSampler()
        {
            rnd = new Random(Environment.TickCount);
        }

        public override Sampler Clone()
        {
            return new DotNetSampler() { rnd = new Random(rnd.Next()) };
        }

        public override float GetNextSample(int index = 0)
        {
            return (float)rnd.NextDouble();
        }
    }

}
