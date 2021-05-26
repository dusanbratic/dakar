using DakarRallySimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRallySimulation.Race
{
    public class VehicleInRace
    {
        private List<int> malfunctions;
        private double malfunctionProbability;
        private double heavyMalfunctionProbability;
        private int repairementPeriod;
        private int periodsRunning;
        private Random random;
        private const double RACE_DISTANCE = 10000;

        public VehicleInRace(long id, double malfunctionProbability, double heavyMalfunctionProbability, double speed, int repairementPeriod)
        {
            this.Id = id;
            this.malfunctionProbability = malfunctionProbability;
            this.heavyMalfunctionProbability = heavyMalfunctionProbability;
            this.Speed = speed;
            this.repairementPeriod = repairementPeriod;
            periodsRunning = 0;
            VehicleState = State.Running;
            random = new Random();
            malfunctions = new List<int>();
        }

        public State VehicleState { get; set; }
        public double DistanceCovered { get; set; }
        public double Speed { get; set; }
        public long Id { get; set; }

        public void Update(int raceDuration)
        {
            switch (VehicleState)
            {
                case State.Running:
                    UpdateRunning(raceDuration);
                    break;
                case State.Repair:
                    UpdateRepair(raceDuration);
                    break;
                default:
                    break;
            }
        }

        private void UpdateRunning(int raceDuration)
        {
            periodsRunning++;
            DistanceCovered = periodsRunning * Speed;
            if (DistanceCovered > RACE_DISTANCE)
            {
                VehicleState = State.Finished;
                return;
            }

            if (random.NextDouble() < heavyMalfunctionProbability)
            {
                VehicleState = State.Abort;
                return;
            }

            if (random.NextDouble() < malfunctionProbability)
            {
                VehicleState = State.Repair;
                malfunctions.Add(raceDuration);
                return;
            }
        }

        private void UpdateRepair(int raceDuration)
        {
            int repairementDuration = raceDuration - malfunctions.Last();
            if (repairementDuration >= repairementPeriod)
            {
                VehicleState = State.Running;
            }
        }
    }

    public enum State
    {
        NotStarted,
        Running,
        Repair,
        Abort,
        Finished
    }
}
