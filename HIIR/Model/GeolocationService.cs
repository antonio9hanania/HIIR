using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Linq;
using System.Collections.Concurrent;

namespace HIIR.Model
{
    public class GeolocationService
    {
        private readonly int LIMIT = 10;
        private double distanceOfCoordsSum = 0;
        private List<Position> locationsList;
        private Position lastKnownLocation = null;
        private MonoticityQueue SpeedQueue;

        public GeolocationService()
        {
            
            SpeedQueue = new MonoticityQueue(LIMIT);
        }

        public int PartialSpeedGraphSize
        {
            get { return SpeedQueue.Limit; }
            set { SpeedQueue.Limit = value; }
        }
        public bool IsTrackingPermissionSucceed { get; private set; } = false;
        public double Speed { get; set; }
        public double DistanceInKM { get; set; }
        public double AverageSpeed { get { return locationsList.Average(s => s.Speed); } }
        public bool IsSpeedMonotonicInc { get { return SpeedQueue.IsMonotonicallyInc; } }
        public int SpeedErrorrAreaOFMonoticity
        { 
            get { return SpeedQueue.ErrorAreaInKPH; }
            set { SpeedQueue.ErrorAreaInKPH = value; }
        }

        public async Task GetPermissions()
        {
            PermissionStatus status;
            if (!IsTrackingPermissionSucceed)
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();


            if (await CrossGeolocator.Current.GetPositionAsync() != null)
                IsTrackingPermissionSucceed = true;
            else
                IsTrackingPermissionSucceed = false;
        }

        public void startGPSTraking()
        {
            bool isSucceed;
            PermissionStatus status;

            Task.Run(async () =>
            {
                try
                {
                    locationsList = new List<Position>();
                    if (!IsTrackingPermissionSucceed)
                        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    CrossGeolocator.Current.PositionChanged += updateRouteMeasuring;


                    var locator = CrossGeolocator.Current;

                    locator.DesiredAccuracy = 1;

                    var position = await locator.GetPositionAsync(timeout: new TimeSpan(0, 0, 1));

                    if (position == null)
                    {
                        isSucceed = false;
                        IsTrackingPermissionSucceed = false;


                    }
                    else
                    {
                        isSucceed = true;
                        Speed = position.Speed;
                        lastKnownLocation = position;
                        locationsList.Add(position);
                        SpeedQueue.Enqueue(position.Speed);
                        await locator.StartListeningAsync(new TimeSpan(0, 0, 1), 0);
                        IsTrackingPermissionSucceed = true;
                    }
                }
                catch
                {
                    isSucceed = false;
                }


            });


         
            //return isSucceed;

        }


        private void updateRouteMeasuring(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var currLocation = e.Position;
            if (lastKnownLocation == null)
                lastKnownLocation = currLocation;

            var distance = Location.CalculateDistance(lastKnownLocation.Latitude, lastKnownLocation.Longitude, currLocation.Latitude, currLocation.Longitude, DistanceUnits.Kilometers);
            if (distance != 0)
            {
                distanceOfCoordsSum += distance;
                DistanceInKM = distanceOfCoordsSum;
                Speed = currLocation.Speed;
                locationsList.Add(currLocation);
                SpeedQueue.Enqueue(currLocation.Speed);

                lastKnownLocation = currLocation;
            }
        }

        public async void PauseTraking()
        {

            CrossGeolocator.Current.PositionChanged -= updateRouteMeasuring;
            await CrossGeolocator.Current.StopListeningAsync();
            lastKnownLocation = null;
        }
        public async void ResumeTraking()
        {
            await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0, 0, 1), 0);
            CrossGeolocator.Current.PositionChanged += updateRouteMeasuring;
        }

        public async void StopTraking()
        {

            CrossGeolocator.Current.PositionChanged -= updateRouteMeasuring;
            await CrossGeolocator.Current.StopListeningAsync();
            lastKnownLocation = null;
            locationsList = null;
            distanceOfCoordsSum = 0;
        }

        public async void RestartTraking()
        {
            StopTraking();
            locationsList = new List<Position>();
            CrossGeolocator.Current.PositionChanged += updateRouteMeasuring;

            await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0, 0, 1), 0);

        }
    }
}
