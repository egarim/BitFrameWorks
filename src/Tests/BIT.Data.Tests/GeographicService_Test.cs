using BIT.Data.Models;
using BIT.Data.Services;
using NUnit.Framework;
using System;

namespace BIT.Data.Tests
{
    public class GeographicService_Test
    {
        //[{"lat":33.360390342655606,"lng":-111.77877783709913},{"lat":33.38329288020202,"lng":-112.38052368164062},{"lat":33.64667008483524,"lng":-111.86168624160157}]

        GeoPoint[] ChicagoPolygon = {new GeoPoint(42.902683475152706, -96.12195799702405),
                            new GeoPoint(40.46633638069888, -96.20984862202405),
                            new GeoPoint(40.65830115005681, -88.49744627827405),
                            new GeoPoint(43.143638856179976, -89.04676268452405),
                            new GeoPoint(42.81409625583358,-96.19886229389905)};

        GeoPoint PointInsideChicagoPolygon = new GeoPoint(41.945759108647536, -92.27674315327405);
        GeoPoint PointSanSalvador = new GeoPoint(13.68935, -89.18718);
        GeoPoint Line1Start = new GeoPoint(0,1);
        GeoPoint Line1End = new GeoPoint(5, 1);
        GeoPoint Line2Start = new GeoPoint(2, 0);
        GeoPoint Line2End = new GeoPoint(2, 3);
        GeoPoint Line3Start = new GeoPoint(0,5);
        GeoPoint Line3End = new GeoPoint(5, 5);

        GeoPoint P = new GeoPoint(0, 1);
        GeoPoint Q = new GeoPoint(0, 2);
        GeoPoint R = new GeoPoint(0, 3);


        GeoPoint P1 = new GeoPoint(10, 0);
        GeoPoint Q1 = new GeoPoint(5, 5);
        GeoPoint R1 = new GeoPoint(8, 5);


        GeoPoint P2 = new GeoPoint(10, 0);
        GeoPoint Q2 = new GeoPoint(8, 5);
        GeoPoint R2 = new GeoPoint(5, 5);


        GeoPoint Q3 = new GeoPoint(8, 5);
        GeographicService GeoService = new GeographicService();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsInsidePolygon_PointInsidePolygon_TestShouldPass()
        {
             Assert.IsTrue(GeoService.IsInsidePolygon(ChicagoPolygon,PointInsideChicagoPolygon));
        }
        [Test]
        public void IsInsidePolygon_PointOutsidePolygon_TestShouldPass()
        {
            Assert.IsFalse(GeoService.IsInsidePolygon(ChicagoPolygon, PointSanSalvador));
        }
        [Test]
        public void LinesIntersect_LinesDoIntersect_TestShouldPass()
        {
            Assert.IsTrue(GeoService.LinesIntersect(Line1Start,Line1End,Line2Start,Line2End));
        }
        [Test]
        public void LinesIntersect_LinesDoNetIntersect_TestShouldPass()
        {
            Assert.IsFalse(GeoService.LinesIntersect(Line1Start, Line1End, Line3Start, Line3End));
        }

        [Test]
        public void GetOrientation_colinear_TestShouldPass()
        {
            Assert.AreEqual(GeoService.GetOrientation(P,Q,R),0);
        }
        [Test]
        public void GetOrientation_Clockwise_TestShouldPass()
        {
            Assert.AreEqual(GeoService.GetOrientation(P1, Q1, R1), 1);
          
        }
        [Test]
        public void GetOrientation_Counterclockwise_TestShouldPass()
        {
            Assert.AreEqual(GeoService.GetOrientation(P2, Q2, R2), 2);
        }
        [Test]
        public void IsOnSegment_IsTrue_TestShouldPass()
        {
            Assert.IsTrue(GeoService.IsOnSegment(P, Q, R));
        }
        [Test]
        public void IsOnSegment_IsFalse_TestShouldPass()
        {
            Assert.IsFalse(GeoService.IsOnSegment(P, Q3, R));
        }
    }
}
