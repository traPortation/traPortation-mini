using NUnit.Framework;
using TraPortation.Moving;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class PositionTest
    {
        // Testアトリビュートを付ける
        [Test]
        public void DistanceToSegmentTest()
        {
            var pos = new Position(1, 1);
            Assert.AreApproximatelyEqual(1f, pos.DistanceToSegment(new Position(0, 0), new Position(2, 0)));
        
			Assert.AreApproximatelyEqual(Mathf.Sqrt(2), pos.DistanceToSegment(new Position(0, 0), new Position(-1, 0)));
			Assert.AreApproximatelyEqual(Mathf.Sqrt(2), pos.DistanceToSegment(new Position(-1, 0), new Position(0, 0)));
		
			Assert.AreApproximatelyEqual(Mathf.Sqrt(2), pos.DistanceToSegment(new Position(-2, 2), new Position(2, -2)));
		}
    }
}