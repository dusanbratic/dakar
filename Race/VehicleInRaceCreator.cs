using DakarRallySimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRallySimulation.Race
{
    public class VehicleInRaceCreator
    {
        private const int MINUTES_IN_HOUR = 60;
        public VehicleInRace Create(Vehicle vehicle)
        {
            switch (vehicle.Type)
            {
                case VehicleType.SportsCar:
                    return new VehicleInRace(vehicle.Id, 0.12 / MINUTES_IN_HOUR, 0.02 / MINUTES_IN_HOUR, 140.0 / MINUTES_IN_HOUR, 5 * MINUTES_IN_HOUR);
                    break;
                case VehicleType.TerrainCar:
                    return new VehicleInRace(vehicle.Id, 0.03 / MINUTES_IN_HOUR, 0.01 / MINUTES_IN_HOUR, 100.0 / MINUTES_IN_HOUR, 5 * MINUTES_IN_HOUR);
                    break;
                case VehicleType.Truck:
                    return new VehicleInRace(vehicle.Id, 0.06 / MINUTES_IN_HOUR, 0.04 / MINUTES_IN_HOUR, 80.0 / MINUTES_IN_HOUR, 7 * MINUTES_IN_HOUR);
                    break;
                case VehicleType.CrossMotorcycle:
                    return new VehicleInRace(vehicle.Id, 0.03 / MINUTES_IN_HOUR, 0.02 / MINUTES_IN_HOUR, 85.0 / MINUTES_IN_HOUR, 3 * MINUTES_IN_HOUR);
                    break;
                case VehicleType.SportMotorcycle:
                    return new VehicleInRace(vehicle.Id, 0.18 / MINUTES_IN_HOUR, 0.10 / MINUTES_IN_HOUR, 130.0 / MINUTES_IN_HOUR, 3 * MINUTES_IN_HOUR);
                    break;
                default:
                    return new VehicleInRace(0, 0, 0, 0, 0);
                    break;
            }
        }
    }
}
