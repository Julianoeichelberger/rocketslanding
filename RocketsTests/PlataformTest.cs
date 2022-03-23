using System;
using NUnit.Framework;

namespace LandingRockets
{
    [TestFixture]
    public class PlataformTest
    {
        [Test]
        [TestCase(10, 10)]
        [TestCase(5, 10)]
        [TestCase(95, 95)]
        [TestCase(1, 1)]
        [TestCase(9, 50)]
        public void TestValid(int SizeX, int SizeY)
        {
            try
            {
                var plataform = new Plataform(SizeX, SizeY);
                Assert.That(plataform, Is.Not.Null);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(96, 96)]
        [TestCase(100, 0)]
        public void TestInvalid(int SizeX, int SizeY)
        {
            try
            {
                var plataform = new Plataform(SizeX, SizeY);

                Assert.Fail("should return PlataformSizeExceededLimits exception");
            }
            catch (PlataformSizeExceededLimits E)
            {
                Assert.Pass(E.Message);
            }
            catch
            {
                Assert.Fail("Not handled exception");
            }
        }

        [Test]
        [TestCase(10, 10, 5, 5)]
        [TestCase(10, 10, 7, 7)]
        [TestCase(10, 10, 7, 9)]
        [TestCase(25, 20, 15, 19)]
        [TestCase(15, 15, 14, 13)]
        public void TestCoordinatesInRange(
            int SizeX, int SizeY, int X, int Y)
        {
            var coordinate = new Coordinate(X, Y);

            var plataform = new Plataform(SizeX, SizeY);
            Assert.IsTrue(plataform.IsInRange(coordinate));
        }

        [Test]
        [TestCase(10, 10, 15, 16)]
        [TestCase(10, 10, 20, 19)]
        [TestCase(20, 20, 26, 25)]
        [TestCase(25, 20, 30, 35)]
        [TestCase(15, 15, 21, 21)]
        public void TestCoordinatesIsOutOfRange(
            int SizeX, int SizeY, int X, int Y)
        {
            var coordinate = new Coordinate(X, Y);

            var plataform = new Plataform(SizeX, SizeY);
            Assert.IsFalse(plataform.IsInRange(coordinate));
        }
    }
}
