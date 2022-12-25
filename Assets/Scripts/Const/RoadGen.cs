using UnityEngine;

namespace TraPortation.Const
{
    public class RoadGen
    {
		// 交差する道がなす角度の最小値
		public const float RoadMinAngle = 30 * Mathf.Deg2Rad;

		// 道同士が「近い」と判定する距離
		public const float NearRoadDist = 1.0f;

		// 道の長さの最小値
		public const float RoadMinLength = 1.0f;
    }
}