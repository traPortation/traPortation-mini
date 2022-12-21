using System.Collections.Generic;
using NUnit.Framework;
using TraPortation.Core.RoadGen;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

#nullable enable

namespace Tests
{
    public class RoadGeneratorTest
    {
        [Test]
        public void CollideToHalfLineTest()
        {
            var line = new Line(new Vector2(0, 0), new Vector2(10, 0));
            var (collide, point) = line.CollideToHalfLine(new Vector2(2, -2), Mathf.PI / 4);
            Assert.IsTrue(collide);
            Assert.AreApproximatelyEqual(point.x, 4);
            Assert.AreApproximatelyEqual(point.y, 0);

            (collide, point) = line.CollideToHalfLine(new Vector2(2, -2), Mathf.PI / 4 + Mathf.PI);
            Assert.IsFalse(collide);
        }
    }
}