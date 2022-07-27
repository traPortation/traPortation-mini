using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TraPortation.Moving;
using TraPortation.Moving.Section.Person;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class WalkSectionTest
    {
        WalkSection section;

        [SetUp]
        public void SetUp()
        {
            var pos = new List<Position>();
            pos.Add(new Position(0, 0));
            pos.Add(new Position(0, 1));
            pos.Add(new Position(1, 1));
            this.section = new WalkSection(pos);
        }

        [Test]
        public void ConstructTest()
        {
            Assert.IsNotNull(this.section);
            Assert.AreEqual(this.section.Status, SectionStatus.NotStarted);
        }

        [Test]
        public void StartTest()
        {
            this.section.Start();

            Assert.AreEqual(this.section.Status, SectionStatus.Walking);
            Assert.AreEqual(this.section.Position, new Position(0, 0));
        }

        [Test]
        public void MoveTest()
        {
            this.section.Start();

            this.section.Move(0.5f);

            Assert.AreEqual(this.section.Position, new Position(0, 0.5f));
            Assert.AreEqual(this.section.Status, SectionStatus.Walking);

            this.section.Move(2);
            Assert.AreEqual(this.section.Position, new Position(0, 1));
            Assert.AreEqual(this.section.Status, SectionStatus.Walking);

            this.section.Move(2);
            Assert.AreEqual(this.section.Position, new Position(1, 1));
            Assert.AreEqual(this.section.Status, SectionStatus.Finished);

            this.section.Dispose();
        }
    }
}