using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using Zenject;
using Moq;

namespace Tests
{
    public class RailTest : ZenjectUnitTestFixture
    {
        [Inject]
        Rail.Factory factory;

        [SetUp]
        public void CommonInstall()
        {
            Container.BindFactory<List<PathNode>, int, string, Rail, Rail.Factory>();

            var lineMock = new Mock<UI.ILine>();
            Container.BindInstance(lineMock.Object);

            Container.Inject(this);
        }

        [Test]
        public void FactoryTest()
        {
            int id = 1;
            string name = "name";
            var rail = this.factory.Create(new List<PathNode>(), id, name);
            
            Assert.AreEqual(rail.ID, id);
            Assert.AreEqual(rail.Name, name);
        }

        // TODO: テスト追加
    }
}