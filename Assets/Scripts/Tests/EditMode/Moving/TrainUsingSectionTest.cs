using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using NUnit.Framework;
using TraPortation.Event.Train;
using TraPortation.Moving;
using TraPortation.Moving.Section.Person;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using UnityEngine;
using Zenject;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class TrainUsingSectionTest : ZenjectUnitTestFixture
    {
        [Inject]
        TrainUsingSection.Factory factory;
        [Inject]
        IPublisher<int, TrainEvent> trainPub;
        [Inject]
        IPublisher<int, StationEvent> stationPub;

        List<Station> stations;
        TrainUsingSection section;

        [SetUp]
        public void SetUp()
        {
            var option = Container.BindMessagePipe();

            Container.BindFactory<IReadOnlyList<Station>, TrainUsingSection, TrainUsingSection.Factory>();
            Container.BindMessageBroker<int, TrainEvent>(option);
            Container.BindMessageBroker<int, StationEvent>(option);

            Container.Inject(this);

            var nodes = new List<StationNode>();
            nodes.Add(new StationNode(0, 0, 0, StationKind.Train));
            nodes.Add(new StationNode(1, 1, 1, StationKind.Train));
            nodes.Add(new StationNode(2, 2, 2, StationKind.Train));

            this.stations = nodes.Select(n => new Station(n)).ToList();

            this.section = this.factory.Create(stations);
        }

        [Test]
        public void StartTest()
        {
            Assert.AreEqual(section.Status, SectionStatus.NotStarted);
            Assert.AreEqual(section.Position, new Position(0, 0));

            section.Start();
            Assert.AreEqual(section.Status, SectionStatus.OnStation);

            section.Move(1);
        }

        [Test]
        public void AnotherStationTest()
        {
            section.Start();

            stationPub.Publish(1, new StationEvent(0, stations[0]));
            Assert.AreEqual(section.Status, SectionStatus.OnStation);
        }

        [Test]
        public void AnotherNextStationTest()
        {
            section.Start();

            stationPub.Publish(0, new StationEvent(0, stations[2]));
            Assert.AreEqual(section.Status, SectionStatus.OnStation);
        }

        [Test]
        public void RideOnTest()
        {
            section.Start();

            stationPub.Publish(0, new StationEvent(0, stations[1]));
            Assert.AreEqual(section.Status, SectionStatus.OnTrain);

            // 乗り続ける
            trainPub.Publish(0, new TrainEvent(stations[1], stations[2]));
            Assert.AreEqual(section.Status, SectionStatus.OnTrain);

            // 降りる
            trainPub.Publish(0, new TrainEvent(stations[2], stations[1]));
            Assert.AreEqual(section.Status, SectionStatus.Finished);
        }

        [Test]
        public void RideOnTest2()
        {
            section.Start();

            stationPub.Publish(0, new StationEvent(0, stations[1]));
            Assert.AreEqual(section.Status, SectionStatus.OnTrain);

            // 次の駅が違うので降りる
            trainPub.Publish(0, new TrainEvent(stations[1], stations[0]));
            Assert.AreEqual(section.Status, SectionStatus.OnStation);

            // 電車が来たら乗る
            stationPub.Publish(1, new StationEvent(0, stations[2]));
            Assert.AreEqual(section.Status, SectionStatus.OnTrain);

            // 降りる
            trainPub.Publish(0, new TrainEvent(stations[2], stations[1]));
            Assert.AreEqual(section.Status, SectionStatus.Finished);
        }
    }
}