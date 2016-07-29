﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.Utils;

namespace CarPooling.Models
{
    public class Driver : Rider
    {
        public Boundary PickupBoundary { get; set; }
        public Boundary DropoffBoundary { get; set; }

        public Driver(string id, Coordinate pickup, Coordinate dropoff) : base(id, pickup, dropoff)
        {

        }

        public void ComputeBoundaries()
        {
            var distance = this.FlyingDistance / 2;

            this.PickupBoundary = this.GetBoundary(this.Pickup, distance);
            this.DropoffBoundary = this.GetBoundary(this.Dropoff, distance);
        }

        public bool Bounds(Passenger passenger)
        {
            return true;
        }

        private Boundary GetBoundary(Coordinate point, double distance)
        {
            const double oneDegreeInKm = 110.574;

            var latitudeConversionFactor = distance / oneDegreeInKm;
            var longitudeConversionFactor = distance / oneDegreeInKm / Math.Abs(Math.Cos(GeoLocation.ToRadians(point.Latitude)));


            var minLatitude = point.Latitude - latitudeConversionFactor;
            var minLongitude = point.Longitude - longitudeConversionFactor;

            var maxLatitude = point.Latitude + latitudeConversionFactor;
            var maxLongitude = point.Longitude + longitudeConversionFactor;

            var boundary = new Boundary
            {
                MinCoordinate = new Coordinate(minLatitude, minLongitude),
                MaxCoordinate = new Coordinate(maxLatitude, maxLongitude)
            };

            return boundary;
        }

        //        public bool isWithin(Coordinate pt, GeoCoordinate sw, GeoCoordinate ne)
        //        {
        //            return pt.Latitude >= sw.Latitude &&
        //                   pt.Latitude <= ne.Latitude &&
        //                   pt.Longitude >= sw.Longitude &&
        //                   pt.Longitude <= ne.Longitude
        //}

        public bool DrivesInSameDirection(Passenger passenger)
        {
            return true;
        }
    }
}
