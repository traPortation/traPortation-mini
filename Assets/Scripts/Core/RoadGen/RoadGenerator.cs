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
            int i = 0;

            while (this.roads.Count < Const.General.RoadCount)
            {
                i++;

                if (i > 1000)
                {
                    Debug.Log("RoadGenerator: Failed to generate roads");
                    this.roads = new List<Road>();
                    i = 0;
                    continue;
                }

                var p = this.randomPoint();
                var angle = this.randomAngle();

                try
                {
                    this.AddRoad(p, angle);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            }
        }

        public void AddRoad(Vector2 p, float angle)
        {
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
                if (angleNear(angle, startNearestRoad.line.Angle(), Const.RoadGen.RoadMinAngle))
                {
                    return;
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
                if (angleNear(angle, endNearestRoad.line.Angle(), Const.RoadGen.RoadMinAngle))
                {
                    return;
                }
                end = endNearestRoad.line.CollideToHalfLine(p, angleRev).Item2;
            }
            else
            {
                if (startNearestRoad == null && this.roads.Count != 0)
                {
                    return;
                }
                end = Line.EdgePoint(p, angleRev);
            }

            if ((end - start).magnitude < Const.RoadGen.RoadMinLength)
            {
                return;
            }

            if (startNearestRoad != null)
            {
                foreach (var n in startNearestRoad.GetNeigbors())
                {
                    var (point, road) = n;
                    if ((point - start).magnitude < Const.RoadGen.NearRoadDist)
                    {
                        var ang = road.line.Angle();
                        if (point == road.line.start) ang += Mathf.PI;

                        if (point == startNearestRoad.line.start || point == startNearestRoad.line.end)
                        {
                            if (angleNear(angle, road.line.Angle(), Const.RoadGen.RoadMinAngle))
                            {
                                return;
                            }
                        }
                        else if (angleNearSameWay(angle, ang, Const.RoadGen.RoadMinAngle))
                        {
                            return;
                        }
                    }
                }
            }

            if (endNearestRoad != null)
            {
                foreach (var n in endNearestRoad.GetNeigbors())
                {
                    var (point, road) = n;
                    if ((point - end).magnitude < Const.RoadGen.NearRoadDist)
                    {
                        var ang = road.line.Angle();
                        if (point == road.line.end) ang += Mathf.PI;

                        if (point == endNearestRoad.line.start || point == endNearestRoad.line.end)
                        {
                            if (angleNear(angle, road.line.Angle(), Const.RoadGen.RoadMinAngle))
                            {
                                return;
                            }
                        }
                        else if (angleNearSameWay(angle, ang, Const.RoadGen.RoadMinAngle))
                        {
                            return;
                        }
                    }
                }
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

        Vector2 randomPoint()
        {
            return new Vector2(Random.Range(Const.X.Min, Const.X.Max), Random.Range(Const.Y.Min, Const.Y.Max));
        }

        float randomAngle()
        {
            return Random.Range(0.0f, 2.0f) * Mathf.PI;
        }

        /// <summary>
        /// aとbの角度がangle以内かどうか 
        /// </summary>
        bool angleNear(float a, float b, float angle)
        {
            return Mathf.Abs(a - b) % Mathf.PI < angle || Mathf.Abs(a - b) % Mathf.PI > Mathf.PI - angle;
        }

        bool angleNearSameWay(float a, float b, float angle)
        {
            return Mathf.Abs(a - b) < angle || Mathf.Abs(a - b) > Mathf.PI * 2 - angle;
        }
    }
}