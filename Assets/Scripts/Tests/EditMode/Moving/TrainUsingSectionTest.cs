using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using Zenject;
using MessagePipe;
using Moving;
using Moving.Section.Person;
using Traffic.Node;
using Event;

namespace Tests
{
    public class TrainUsingSectionTest : ZenjectUnitTestFixture
    {
        [Inject]
        TrainUsingSection.Factory factory;
        [Inject]
        IPublisher<int, VehicleArrivedEvent> vehiclePub;
        [Inject]
        IPublisher<int, StationArrivedEvent> stationPub;

        List<Station> stations;
        TrainUsingSection section;

        [SetUp]
        public void SetUp()
        {
            var option = Container.BindMessagePipe();

            Container.BindFactory<IReadOnlyList<Station>, TrainUsingSection, TrainUsingSection.Factory>();
            Container.BindMessageBroker<int, VehicleArrivedEvent>(option);
            Container.BindMessageBroker<int, StationArrivedEvent>(option);

            Container.Inject(this);

            var nodes = new List<StationNode>();
            nodes.Add(new StationNode(0, 0, 0));
            nodes.Add(new StationNode(1, 1, 1));
            nodes.Add(new StationNode(2, 2, 2));

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

            stationPub.Publish(1, new StationArrivedEvent(0, stations[0]));
            Assert.AreEqual(section.Status, SectionStatus.OnStation);
        }

        [Test]
        public void AnotherNextStationTest()
        {
            section.Start();

            stationPub.Publish(0, new StationArrivedEvent(0, stations[2]));
            Assert.AreEqual(section.Status, SectionStatus.OnStation);
        }

        [Test]
        public void RideOnTest()
        {
            section.Start();

            stationPub.Publish(0, new StationArrivedEvent(0, stations[1]));
            Assert.AreEqual(section.Status, SectionStatus.OnTrain);

            // 乗り続ける
            vehiclePub.Publish(0, new VehicleArrivedEvent(stations[1], stations[2]));
            Assert.AreEqual(section.Status, SectionStatus.OnTrain);

            // 降りる
            vehiclePub.Publish(0, new VehicleArrivedEvent(stations[2], stations[1]));
            Assert.AreEqual(section.Status, SectionStatus.Finished);
        }

        [Test]
        public void RideOnTest2()
        {
            section.Start();

            stationPub.Publish(0, new StationArrivedEvent(0, stations[1]));
            Assert.AreEqual(section.Status, SectionStatus.OnTrain);

            // 次の駅が違うので降りる
            vehiclePub.Publish(0, new VehicleArrivedEvent(stations[1], stations[0]));
            Assert.AreEqual(section.Status, SectionStatus.OnStation);

            // 電車が来たら乗る
            stationPub.Publish(1, new StationArrivedEvent(0, stations[2]));
            Assert.AreEqual(section.Status, SectionStatus.OnTrain);

            // 降りる
            vehiclePub.Publish(0, new VehicleArrivedEvent(stations[2], stations[1]));
            Assert.AreEqual(section.Status, SectionStatus.Finished);
        }
    }
}