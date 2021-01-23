using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overloaded_Harbor
{
    public enum ShipType { Small, Medium, Large };

    public class Ship
    {
        public Ship(ShipType type, double arrivalToHarbor)
        {
            Type = type;
            ArraivalToHarbor = arrivalToHarbor;
        }
        public ShipType Type { get; set; }
        public double ArraivalToHarbor { get; set; }
        public double EntranceToHarbor { get; set; }
        public double ArrivalToDock { get; set; }
        public double LoadEnd { get; set; }
        public double ExitFromDock { get; set; }
        public double HarborExit { get; set; }
        public double WaitTime
        {
            get { return (EntranceToHarbor - ArraivalToHarbor + ExitFromDock - LoadEnd) / 60 / 60; }
        }
        public double LoadTime
        {
            get { return (LoadEnd - ArrivalToDock) / 60 / 60; }
        }
    }
}
