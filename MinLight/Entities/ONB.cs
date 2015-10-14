namespace MinLight.Entities
{
    using System;

    public class ONB
    {
        public Vector Normal, Binormal, Tangent;

        public ONB()
        {
            Normal = new Vector(1, 0, 0);
            Binormal = new Vector(0, 1, 0);
            Tangent = new Vector(0, 0, 1);
        }

        public ONB(Vector n)
        {
            this.Normal = Vector.Unitize(n);
            var tmpX = (Math.Abs(Normal.x) > 0.99f) ? new Vector(0, 1, 0) : new Vector(1, 0, 0);
            Binormal = Vector.Unitize(Vector.Cross(Normal, tmpX));
            Tangent = Vector.Cross(Binormal, Normal);
        }


        public Vector ToWorld(Vector a)
        {
            return Normal * a.x + Binormal * a.y + Tangent * a.z;
        }

        public Vector ToLocal(Vector a)
        {
            return new Vector(Vector.Dot(a, Normal), Vector.Dot(a, Binormal), Vector.Dot(a, Tangent));
        }

    }
}