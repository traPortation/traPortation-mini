using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;
using Zenject;
using MessagePipe;
using Cysharp.Threading.Tasks;
using Moving;
using Moving.Section.Train;
using Event.Train;
using Traffic.Node;

namespace Tests
{
    public class TrainSectionTest : ZenjectUnitTestFixture
    {
        [Inject]
        TrainSection.Factory factory;
        [Inject]
        ISubscriber<int, TrainEvent> trainSub;
        [Inject]
        ISubscriber<int, StationEvent> stationSub;

        List<Station> stations;
        TrainSection section;

        [SetUp]
        public void SetUp()
        {
            var option = Container.BindMessagePipe();
            Container.BindMessageBroker<int, TrainEvent>(option);
            Container.BindMessageBroker<int, StationEvent>(option);

            Container.BindFactory<IReadOnlyList<Station>, int, float, TrainSection, TrainSection.Factory>();

            Container.Inject(this);

            var nodes = new List<StationNode>();
            nodes.Add(new StationNode(0, 0, 0));
            nodes.Add(new StationNode(0, 1, 1));
            nodes.Add(new StationNode(2, 2, 2));

            this.stations = nodes.Select(n => new Station(n)).ToList();

            this.section = this.factory.Create(stations, 1, 0.01f);
        }

        [Test]
        public void StartTest()
        {
            Assert.AreEqual(this.section.Status, SectionStatus.NotStarted);

            this.section.Start();
            Assert.AreEqual(this.section.Status, SectionStatus.TrainMoving);
            Assert.AreEqual(this.section.Position, new Position(0, 0));
        }

        [UnityTest]
        public IEnumerator MoveTest() => UniTask.ToCoroutine(async () =>
        {
            this.section.Start();

            // 次の駅まで着かない場合
            this.section.Move(0.5f);
            Assert.AreEqual(this.section.Position, new Position(0, 0.5f));

            // 次の駅に着く場合はそこで止まる
            this.section.Move(10);
            Assert.AreEqual(this.section.Position, new Position(0, 1));

            // 止まっている間は動かない
            this.section.Move(10);
            Assert.AreEqual(this.section.Position, new Position(0, 1));

            await UniTask.Delay(20);

            this.section.Move(10);
            Assert.AreEqual(this.section.Position, new Position(2, 2));

            await UniTask.Delay(200);

            // 折り返し
            this.section.Move(10);
            Assert.AreEqual(this.section.Position, new Position(0, 1));

            await UniTask.Delay(20);

            this.section.Move(10);
            Assert.AreEqual(this.section.Position, new Position(0, 0));

            await UniTask.Delay(20);

            // 折り返し
            this.section.Move(10);
            Assert.AreEqual(this.section.Position, new Position(0, 1));
        });
    }
}