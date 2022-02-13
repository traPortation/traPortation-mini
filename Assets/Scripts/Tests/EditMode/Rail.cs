using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using Zenject;
using Moq;
using Moving;
using Moving.Section.Train;

namespace Tests
{
    public class RailTest : ZenjectUnitTestFixture
    {
        [Inject]
        Rail.Factory factory;

        [SetUp]
        public void CommonInstall()
        {
            Container.BindFactory<List<Station>, int, string, Rail, Rail.Factory>();
            Container.BindFactory<IReadOnlyList<Station>, int, TrainSection, TrainSection.Factory>();

            var lineMock = new Mock<UI.ILine>();
            Container.BindInstance(lineMock.Object);

            Container.Inject(this);
        }

        [Test]
        public void FactoryTest()
        {
            int id = 1;
            string name = "name";
            var rail = this.factory.Create(new List<Station>(), id, name);
            
            Assert.AreEqual(rail.ID, id);
            Assert.AreEqual(rail.Name, name);
        }

        // TODO: テスト追加
    }
}