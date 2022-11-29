using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MessagePipe;
using NUnit.Framework;
using TraPortation.Event.Train;
using TraPortation.Moving;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class TrainPathTest : ZenjectUnitTestFixture
    {
        [Inject]
        TrainPath.Factory factory;
        [Inject]
        ISubscriber<int, TrainEvent> trainSub;
        [Inject]
        ISubscriber<int, StationEvent> stationSub;

        List<Station> stations;
        TrainPath path;

        [SetUp]
        public void SetUp()
        {
            var option = Container.BindMessagePipe();
            Container.BindMessageBroker<int, TrainEvent>(option);
            Container.BindMessageBroker<int, StationEvent>(option);

            Container.BindFactory<int, IReadOnlyList<Station>, TrainPath, TrainPath.Factory>();

            Container.Inject(this);

            var nodes = new List<StationNode>();
            nodes.Add(new StationNode(0, 0, 0, StationKind.Train));
            nodes.Add(new StationNode(0, 1, 1, StationKind.Train));
            nodes.Add(new StationNode(2, 2, 2, StationKind.Train));

            this.stations = nodes.Select(n => new Station(n)).ToList();

            this.path = this.factory.Create(1, stations);
            this.path.StopMilliseconds = 10;
        }

        [Test]
        public void StartTest()
        {
            Assert.AreEqual(this.path.Status, SectionStatus.NotStarted);

            this.path.Move(0);
            Assert.AreEqual(this.path.Status, SectionStatus.Moving);
            Assert.AreEqual(this.path.Position, new Position(0, 0));
        }

        [UnityTest]
        public IEnumerator MoveTest() => UniTask.ToCoroutine(async () =>
        {
            // 次の駅まで着かない場合
            this.path.Move(0.5f);
            Assert.AreEqual(this.path.Position, new Position(0, 0.5f));

            // 次の駅に着く場合はそこで止まる
            this.path.Move(10);
            Assert.AreEqual(this.path.Position, new Position(0, 1));

            // 止まっている間は動かない
            this.path.Move(10);
            Assert.AreEqual(this.path.Position, new Position(0, 1));

            await UniTask.Delay(20);

            this.path.Move(10);
            Assert.AreEqual(this.path.Position, new Position(2, 2));

            await UniTask.Delay(200);

            // 折り返し
            this.path.Move(10);
            Assert.AreEqual(this.path.Position, new Position(0, 1));

            await UniTask.Delay(20);

            this.path.Move(10);
            Assert.AreEqual(this.path.Position, new Position(0, 0));

            await UniTask.Delay(20);

            // 折り返し
            this.path.Move(10);
            Assert.AreEqual(this.path.Position, new Position(0, 1));
        });
    }
}