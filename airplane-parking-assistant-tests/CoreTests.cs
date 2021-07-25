using System;
using airplane_parking_assistant;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using NUnit.Framework;

namespace airplane_parking_assistant_tests
{
    [TestFixture]
    public class CoreTests
    {

       [Test]
        public void NoPlanesParked_ShouldAllowPlaneTooPark()
        {
            var plane = new Plane("AS80");
            
            var actual = new ParkAssistant().Park(plane);
            
            Assert.IsTrue(actual);
        }

        [Test]
        public void JumboFull_ShouldNotAllowAnyOtherJumbo()
        {
            var parking = new ParkAssistant();
            
            for (int i = 0; i < 25; i++)
            {
                parking.Park(new Plane("AS80"));
            }
            Assert.IsFalse(parking.Park(new Plane("AS80")));
        }

        [Test]
        public void PropTwentyEightArrived_ShouldAllowAllToLand()
        {
            var parking = new ParkAssistant();
            
            for (int i = 0; i < 25; i++)
            {
                parking.Park(new Plane("E195"));
            }
            Assert.IsTrue(parking.Park(new Plane("E195")));
        }
        
        [Test]
        public void JetShouldBeParkableWhenPropsExceedCount_ShouldAllowAllToLand()
        {
            var parking = new ParkAssistant();
            
            for (int i = 0; i < 71; i++)
            {
                Assert.IsTrue(parking.Park(new Plane("E195")));
            }
            
            for (int i = 0; i < 4; i++)
            {
                Assert.IsTrue(parking.Park(new Plane("AS80"))); 
            }
            
        }

        [Test]
        public void AirportFull_ShouldThrowException()
        {
            var parking = new ParkAssistant();

            // Prop
            for (int i = 0; i < 25; i++)
                Assert.IsTrue(parking.Park(new Plane("E195")));
            
            // Jet
            for (int i = 0; i < 50; i++)
                Assert.IsTrue(parking.Park(new Plane("A330")));
            
            // Jumbo
            for (int i = 0; i < 25; i++)
                Assert.IsTrue(parking.Park(new Plane("AS80"))); 
            
            Assert.Throws<Exception>(() => parking.Park(new Plane("AS80"))); 
        }
    }
}