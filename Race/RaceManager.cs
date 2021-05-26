using DakarRallySimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRallySimulation.Race
{
    public class RaceManager
    {
        private VehicleInRaceCreator vehicleInRaceCreator;
        private Dictionary<long, VehicleInRace> dictVehicles;
        private DateTime raceStart;
        private bool raceIsOn;
        //race duration in minutes
        private int raceDuration;
        public RaceManager()
        {
            vehicleInRaceCreator = new VehicleInRaceCreator();
            raceIsOn = false;
        }

        public bool StartRace(IEnumerable<Vehicle> vehicles)
        {
            if (raceIsOn)
            {
                return false;
            }

            dictVehicles = new Dictionary<long, VehicleInRace>();
            foreach (Vehicle vehicle in vehicles)
            {
                dictVehicles.Add(vehicle.Id, vehicleInRaceCreator.Create(vehicle));
            }

            raceDuration = 0;
            raceIsOn = true;
            raceStart = DateTime.Now;
            Thread thread = new Thread(ManageRace);
            thread.Start();

            return true;
        }

        public bool TryGetStatus(long id, out RaceStatus status)
        {
            VehicleInRace vehicle;
            if (!dictVehicles.TryGetValue(id, out vehicle))
            {
                status = null;
                return false;
            }

            double distanceCovered = vehicle.DistanceCovered;
            if (vehicle.VehicleState == State.Running)
            {
                DateTime lastUpdate = raceStart + (raceDuration) * TimeSpan.FromMinutes(1);
                TimeSpan periodFromLastUpdate = DateTime.Now - lastUpdate;
                distanceCovered += (periodFromLastUpdate / TimeSpan.FromMinutes(1)) * vehicle.Speed;
            }

            status = new RaceStatus() { DistanceCovered = distanceCovered, State = vehicle.VehicleState };
            return true;
        }

        private void ManageRace(object obj)
        {
            while (raceIsOn)
            {
                DateTime nextUpdate = raceStart + (raceDuration + 1) * TimeSpan.FromMinutes(1);
                TimeSpan waitPeriod = nextUpdate - DateTime.Now;
                Thread.Sleep(waitPeriod);
                raceDuration++;
                foreach (VehicleInRace vehicle in dictVehicles.Values)
                {
                    vehicle.Update(raceDuration);
                }

                if (dictVehicles.Values.All(x => x.VehicleState == State.Abort || x.VehicleState == State.Finished))
                {
                    raceIsOn = false;
                }
            }
        }
    }
}
