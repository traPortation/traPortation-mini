using System.Collections.Generic;
using UnityEngine;

namespace TraPortation.Core.RoadGen
{
    public class Line
    {
        public Vector2 start;
        public Vector2 end;

        public Line(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public float Angle()
        {
            return Mathf.Atan2(end.y - start.y, end.x - start.x);
        }

        public float Distance()
        {
            return (end - start).magnitude;
        }

        Vector2 vector()
        {
            return (end - start);
        }

        static float Cross(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        public (bool, Vector2) CollideToHalfLine(Vector2 point, float angle)
        {
            // https://www.nekonecode.com/math-lab/pages/collision2/line-and-line-pos/

            var v1 = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            var v2 = this.vector();
            var v = this.start - point;

            if (Mathf.Abs(Cross(v1, v2)) < 0.1)
            {
                return (false, Vector2.zero);
            }

            var t = Cross(v, v1) / Cross(v1, v2);

            // 交点
            var p = this.start + v2 * t;

            if (t < 0 || t > 1)
            {
                return (false, Vector2.zero);
            }


            t = Cross(-v, v2) / Cross(v2, v1);
            if (t < 0)
            {
                return (false, Vector2.zero);
            }

            return (true, p);
        }

        public static Vector2 EdgePoint(Vector2 point, float angle)
        {
            var lx_min = new Line(new Vector2(Const.X.Min, Const.Y.Min), new Vector2(Const.X.Min, Const.Y.Max));
            var lx_max = new Line(new Vector2(Const.X.Max, Const.Y.Min), new Vector2(Const.X.Max, Const.Y.Max));
            var ly_min = new Line(new Vector2(Const.X.Min, Const.Y.Min), new Vector2(Const.X.Max, Const.Y.Min));
            var ly_max = new Line(new Vector2(Const.X.Min, Const.Y.Max), new Vector2(Const.X.Max, Const.Y.Max));

            var lines = new List<Line> { lx_min, lx_max, ly_min, ly_max };

            for (int i = 0; i < lines.Count; i++)
            {
                var (isCollide, p) = lines[i].CollideToHalfLine(point, angle);
                if (isCollide)
                {
                    return p;
                }
            }

            Debug.Log(point);
            Debug.Log(angle);
            // TODO: 後で直す 端に近すぎるとだめそう
            throw new System.Exception();
        }
    }
}