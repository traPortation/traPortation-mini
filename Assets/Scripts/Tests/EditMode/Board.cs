using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class BoardTest
    {
        Board board;

        [SetUp]
        public void SetUp()
        {
            this.board = new Board();
        }

        [Test]
        public void InitializeTest()
        {
            Assert.AreEqual(this.board.Nodes.Count, 0);
        }

        [Test]
        public void AddStationNodeTest()
        {
            for (int i = 1; i < 100; i++)
            {
                var x = Random.Range(0f, 10f);
                var y = Random.Range(0f, 10f);

                var node = this.board.AddStationNode(x, y);

                Assert.AreEqual(this.board.Nodes.Count, i);
                Assert.AreEqual((node.X, node.Y), (x, y));
                Assert.AreEqual(node.Index, this.board.Nodes.Count - 1);
            }
        }
    }
}