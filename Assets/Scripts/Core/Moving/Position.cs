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
            return Mathf.Abs((b.Y - a.Y) * this.X - (b.X - a.X) * this.Y + b.X * a.Y - b.Y * a.X) / Mathf.Sqrt(Mathf.Pow(b.X - a.X, 2) + Mathf.Pow(a.Y - b.Y, 2));
        }
    }
}