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
        RoadGenerator generator = new RoadGenerator();

        [SetUp]
        public void Setup()
        {
            this.generator = new RoadGenerator();
        }

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


        [Test]
        public void RoadGenerateTest()
        {
            generator.AddRoad(new Vector2(3, 3), 0);
            generator.AddRoad(new Vector2(4, 3), 0f);

            Assert.AreEqual(generator.roads.Count, 1);

            generator.AddRoad(new Vector2(3, 5), 0.1f);
            Assert.AreEqual(generator.roads.Count, 1);

            generator.AddRoad(new Vector2(4, 5), Mathf.PI / 2);
            Assert.AreEqual(generator.roads.Count, 2);
        }

        [Test]
        public void RoadGenerateTest2()
        {
            generator.AddRoad(new Vector2(1, 1), 0);
            generator.AddRoad(new Vector2(1.1f, 1.01f), 0.2f);

            Assert.AreEqual(generator.roads.Count, 1);
        }
    }
}