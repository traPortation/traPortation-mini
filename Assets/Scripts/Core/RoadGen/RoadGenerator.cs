using System.Collections.Generic;
using UnityEngine;

namespace TraPortation.Core.RoadGen
{
    public class RoadGenerator
    {
        public List<Road> roads = new List<Road>();

        public RoadGenerator() { }

        public void GenerateRoads()
        {
            while (this.roads.Count < 30)
            {
                var p = this.randomPoint();
                var angle = this.randomAngle();
                var angleRev = angle + Mathf.PI;

                Road startNearestRoad = null;
                float nearestDist = float.MaxValue;
                Vector2 start, end;

                // 一番近い道路を探す
                foreach (var road in this.roads)
                {
                    var (collide, point) = road.line.CollideToHalfLine(p, angle);
                    if (collide)
                    {
                        var dist = (point - p).magnitude;
                        if (dist < nearestDist)
                        {
                            startNearestRoad = road;
                            nearestDist = dist;
                        }
                    }
                }

                if (startNearestRoad != null)
                {
                    if (Mathf.Abs(startNearestRoad.line.Angle() - angle) < 1.0)
                    {
                        continue;
                    }

                    start = startNearestRoad.line.CollideToHalfLine(p, angle).Item2;
                }
                else
                {
                    start = Line.EdgePoint(p, angle);
                }

                Road endNearestRoad = null;
                nearestDist = float.MaxValue;

                foreach (var road in roads)
                {
                    var (collide, point) = road.line.CollideToHalfLine(p, angleRev);
                    if (collide)
                    {
                        var dist = (point - p).magnitude;
                        if (dist < nearestDist)
                        {
                            endNearestRoad = road;
                            nearestDist = dist;
                        }
                    }
                }

                if (endNearestRoad != null)
                {
                    if (Mathf.Abs(endNearestRoad.line.Angle() - angleRev) < 1.0)
                    {
                        continue;
                    }
                    end = endNearestRoad.line.CollideToHalfLine(p, angleRev).Item2;
                }
                else
                {
                    if (startNearestRoad == null && this.roads.Count != 0)
                    {
                        continue;
                    }
                    end = Line.EdgePoint(p, angleRev);
                }

                var r = new Road(new Line(start, end));
                this.roads.Add(r);

                if (startNearestRoad != null)
                {
                    r.AddNeigbor(start, startNearestRoad);
                    startNearestRoad.AddNeigbor(start, r);
                }
                if (endNearestRoad != null)
                {
                    r.AddNeigbor(end, endNearestRoad);
                    endNearestRoad.AddNeigbor(end, r);
                }
            }
        }

        Vector2 randomPoint()
        {
            return new Vector2(Random.Range(Const.X.Min, Const.X.Max), Random.Range(Const.Y.Min, Const.Y.Max));
        }

        float randomAngle()
        {
            return Random.Range(0.0f, 2.0f) * Mathf.PI;
        }
    }
}