using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRallySimulation.Models
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public VehicleType Type { get; set; }
    }

    public enum VehicleType
    {
        SportsCar,
        TerrainCar,
        Truck,
        CrossMotorcycle,
        SportMotorcycle
    }
}
