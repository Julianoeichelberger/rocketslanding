using System;
using NUnit.Framework;

namespace LandingRockets
{
    [TestFixture]
    public class LandingTest
    {
        [Test]
        [TestCase(10, 10, 5, 5)]
        [TestCase(10, 10, 10, 8)]
        [TestCase(10, 10, 15, 15)]
        [TestCase(10, 10, 6, 7)]
        [TestCase(10, 10, 7, 8)]
        [TestCase(20, 20, 15, 16)]
        [TestCase(20, 20, 5, 22)]
        [TestCase(30, 20, 30, 22)]
        [TestCase(30, 20, 35, 6)]
        [TestCase(30, 50, 30, 55)]
        public void TestOk(
            int plataformX, int plataformY, int x, int y)
        {
            try
            {
                Landing landing = new Landing(
                    new Plataform(plataformX, plataformY));

                string response = landing.Check(new Coordinate(x, y));
                Assert.AreEqual(Response.Ok, response);
            }
            catch (Exception E)
            {
                Assert.Fail(E.Message);
            }
        }

        [Test]
        [TestCase(10, 10, 5, 3)]
        [TestCase(10, 10, 15, 16)]
        [TestCase(10, 10, 25, 15)]
        [TestCase(10, 10, 99, 8)]
        [TestCase(20, 20, 26, 16)]
        [TestCase(20, 20, 5, 30)]
        [TestCase(30, 20, 38, 22)]
        [TestCase(30, 20, 35, 1)]
        [TestCase(30, 50, 30, 60)]
        [TestCase(93, 93, 99, 99)]
        public void TestOutOfPlataform(
          int plataformX, int plataformY, int x, int y)
        {
            try
            {
                Landing landing = new Landing(
                    new Plataform(plataformX, plataformY));

                string response = landing.Check(new Coordinate(x, y));
                Assert.AreEqual(Response.OutOfPlatform, response);
            }
            catch (Exception E)
            {
                Assert.Fail(E.Message);
            }
        }      

        [Test]
        [TestCase(7, 7, 7, 8)]
        [TestCase(7, 7, 6, 7)]
        [TestCase(7, 7, 6, 6)]
        [TestCase(8, 6, 8, 7)] 
        [TestCase(8, 6, 8, 5)] 
        [TestCase(15, 15, 15, 15)] 
        [TestCase(15, 15, 15, 14)] 
        public void TestClash(
          int x, int y, int x2, int y2)
        {
            try
            {
                Landing landing = new Landing(new Plataform());

                string response = landing.Check(new Coordinate(x, y));
                Assert.AreEqual(Response.Ok, response);

                response = landing.Check(new Coordinate(x2, y2));
                Assert.AreEqual(Response.Clash, response);
            }
            catch (Exception E)
            {
                Assert.Fail(E.Message);
            }
        }
    }
}
