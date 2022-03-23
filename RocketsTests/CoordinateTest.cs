using System;
using NUnit.Framework;

namespace LandingRockets
{
    [TestFixture]
    public class CoordinateTest
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 50)]
        [TestCase(99, 99)]
        [TestCase(0, 98)]
        [TestCase(99, 0)]
        public void TestValid(int x, int y)
        {
            try
            {
                var coordinate = new Coordinate(x, y);
                Assert.That(coordinate, Is.Not.Null);
            }
            catch(Exception E)
            {
                Assert.Fail(E.Message);
            }
        }

        [Test]
        [TestCase(5, 5, 6, 5)]
        [TestCase(6, 7, 7, 6)]
        [TestCase(7, 15, 7, 14)]
        [TestCase(90, 1, 90, 2)]
        [TestCase(15, 16, 16, 15)]
        [TestCase(18, 48, 17, 48)]
        [TestCase(2, 76, 1, 76)]
        public void TestConflit(int reservedX, int reservedY, int x, int y)
        {
            var reserved = new Coordinate(reservedX, reservedY);
            Assert.That(reserved, Is.Not.Null);
            Assert.IsTrue(reserved.ConflictWith(new Coordinate(x, y)));
        }

        [Test]
        [TestCase(5, 5, 6, 1)]
        [TestCase(6, 7, 8, 80)]
        [TestCase(7, 15, 1, 14)]
        [TestCase(90, 1, 0, 2)]
        [TestCase(15, 16, 50, 15)]
        public void TestNoConflit(int reservedX, int reservedY, int x, int y)
        {
            var reserved = new Coordinate(reservedX, reservedY);
            Assert.That(reserved, Is.Not.Null);
            Assert.IsFalse(reserved.ConflictWith(new Coordinate(x, y)));
        }

    }
}