using UnityEngine;

namespace TraPortation.Game
{
    public class GameScreen
    {
        public readonly float Left;
        public readonly float Right;
        public readonly float Top;
        public readonly float Bottom;

        public GameScreen(float left, float right, float top, float bottom)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }

        public bool Contains(Vector2 position)
        {
            return position.x >= this.Left && position.x <= this.Right && position.y >= this.Bottom && position.y <= this.Top;
        }

    }
}