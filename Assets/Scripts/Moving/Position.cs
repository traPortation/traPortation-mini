using UnityEngine;
using Traffic.Node;

namespace Moving
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
    }
}