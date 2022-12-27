using TraPortation.Traffic.Node;
using UnityEngine;

namespace TraPortation.Moving
{
    public struct Position
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Position(INode node)
        {
            this.X = node.X;
            this.Y = node.Y;
        }
        public Position(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        static public float Distance(Position a, Position b)
        {
            return Mathf.Sqrt(Mathf.Pow(a.X - b.X, 2) + Mathf.Pow(a.Y - b.Y, 2));
        }

        public Vector3 ToVector3(float z)
        {
            return new Vector3(this.X, this.Y, z);
        }

        // a, bを通る直線との距離
        public float DistanceToLine(Position a, Position b)
        {
            return Mathf.Abs((b.Y - a.Y) * (this.X - a.X) - (b.X - a.X) * (this.Y - a.Y)) / Position.Distance(a, b);
        }

        // a, bを通る線分との距離
        public float DistanceToSegment(Position a, Position b)
        {
            var t = ((this.X - a.X) * (b.X - a.X) + (this.Y - a.Y) * (b.Y - a.Y));
            if (t < 0) return Position.Distance(this, a);
            
            t = ((this.X - b.X) * (a.X - b.X) + (this.Y - b.Y) * (a.Y - b.Y));
            if (t < 0) return Position.Distance(this, b);

            return this.DistanceToLine(a, b);
        }
    }
}