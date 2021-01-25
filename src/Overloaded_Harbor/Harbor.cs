using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overloaded_Harbor
{
    public class Harbor
    {
        Queue<Ship> shipsInHold;
        Queue<Ship> shipsAttended;
        Dock[] docks;
        bool tugboatInDock;//true dock   false harbor
        double time;
        double arrival;
        Random random;

        public Harbor(int docks = 4)
        {
            DocksCount = docks;
        }

        public int DocksCount { get; set; }

        private void InitializeSistem()
        {
            random = new Random();
            shipsInHold = new Queue<Ship>();
            shipsAttended = new Queue<Ship>();
            tugboatInDock = false;
            time = arrival = GenerateArrivalHarbor();

            docks = new Dock[DocksCount];
            for (int i = 0; i < DocksCount; i++)
                docks[i] = new Dock();
        }

        private double GenerateArrivalHarbor()
        {
            double d = Distribution.Exponential(random, 0.125);
            return d * 60 * 60;
        }
    }
}
