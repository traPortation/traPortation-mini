using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using Traffic;
using Traffic.Node;
using System.Linq;

#nullable enable

namespace Tests
{
    public class BoardTest
    {
        Board board = new Board();

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
            // TODO: Board側の修正に合わせて画面外への設置のテストを追加
            for (int i = 1; i <= 100; i++)
            {
                var x = Random.Range(0f, 10f);
                var y = Random.Range(0f, 10f);

                var node = this.board.AddStationNode(x, y);

                Assert.IsTrue(node is StationNode);
                Assert.AreEqual(this.board.Nodes.Count, i);
                Assert.AreEqual((node.X, node.Y), (x, y));
                Assert.AreEqual(node.Index, this.board.Nodes.Count - 1);
            }
        }

        [Test]
        public void AddIntersectionNodeTest()
        {
            for (int i = 1; i <= 100; i++)
            {
                var x = Random.Range(0f, 10f);
                var y = Random.Range(0f, 10f);

                var node = this.board.AddIntersectionNode(x, y);

                Assert.IsTrue(node is IntersectionNode);
                Assert.AreEqual(this.board.Nodes.Count, i);
                Assert.AreEqual((node.X, node.Y), (x, y));
                Assert.AreEqual(node.Index, this.board.Nodes.Count - 1);
            }
        }

        [Test]
        public void AddVehicleEdgeTest()
        {
            var nodes = new List<StationNode>();

            for (int i = 1; i <= 100; i++)
            {
                var x = Random.Range(0f, 10f);
                var y = Random.Range(0f, 10f);

                var node = this.board.AddStationNode(x, y);
                nodes.Add(node);
            }

            for (int i = 1; i <= 100; i++)
            {
                var idx1 = Random.Range(0, 100);
                var idx2 = Random.Range(0, 100);
                if (idx1 == idx2)
                {
                    idx2 = (idx2 + 1) % 100;
                }

                var node1 = nodes[idx1];
                var node2 = nodes[idx2];

                var edges = this.board.AddVehicleRoute(node1, node2, Const.EdgeType.Train);

                Assert.AreEqual(edges, node1.Edges.Last());
                Assert.AreEqual(edges.To, node2);

            }
        }

        [Test]
        public void GetNearestNodeTest()
        {
            for (int i = 1; i <= 50; i++)
            {
                var x = Random.Range(0f, 10f);
                var y = Random.Range(0f, 10f);
                var node = this.board.AddIntersectionNode(x, y);
            }
            for (int i = 1; i <= 50; i++)
            {
                var x = Random.Range(0f, 10f);
                var y = Random.Range(0f, 10f);
                var node = this.board.AddStationNode(x, y);
            }
            for (int i = 1; i <= 100; i++)
            {
                var x = Random.Range(0f, 10f);
                var y = Random.Range(0f, 10f);
                var node = this.board.GetNearestNode(new Vector3(x, y, Const.Z.Person));

                Assert.IsTrue(node is IBoardNode);
                float minDistance = (x - node.X) * (x - node.X) + (y - node.Y) * (y - node.Y);
                foreach (var boardNode in this.board.Nodes)
                {
                    float dist = (x - boardNode.X) * (x - boardNode.X) + (y - boardNode.Y) * (y - boardNode.Y);
                    Assert.IsTrue(minDistance <= dist);
                }
            }
        }
    }
}