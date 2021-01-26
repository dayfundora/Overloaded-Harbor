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


        private double GenerateLoadedTugboatToDockTransfer()
        {
            double d = Distribution.Exponential(random, 0.5);
            return d * 60 * 60;
        }


        private double GenerateLoadedTugboatToHarborTransfer(ShipType shipType)
        {
            double d = Distribution.Exponential(random, 1);
            return d * 60 * 60;
        }

        private double GenerateLoadDelay(ShipType shipType)
        {
            double d = 0;
            switch (shipType)
            {
                case ShipType.Small:
                    d = Distribution.Normal(random, 9, 1);
                    break;
                case ShipType.Medium:
                    d = Distribution.Normal(random, 12, 2);
                    break;
                case ShipType.Large:
                    d = Distribution.Normal(random, 18, 3);
                    break;
                default:
                    break;
            }
            return d * 60 * 60;
        }

        private Dock GetFreeDock()
        {
            foreach (var m in docks)
                if (m.Ship == null)
                    return m;
            return null;
        }

        private void TakeShipToDock(Ship barco, Dock muelle)
        {
            barco.EntranceToHarbor = time;
            time += GenerateLoadedTugboatToDockTransfer();
            barco.ArrivalToDock = time;
            double tCarga = time + GenerateLoadDelay(barco.Type);
            barco.LoadEnd = muelle.LoadEnd = tCarga;
            muelle.Ship = barco;
            tugboatInDock = true;
        }

        private void TakeShipFromHarbor(Dock muelleALiberar)
        {
            muelleALiberar.Ship.ExitFromDock = time;
            time += GenerateLoadedTugboatToHarborTransfer(muelleALiberar.Ship.Type);
            tugboatInDock = false;
            muelleALiberar.Ship.HarborExit = time;
            shipsAttended.Enqueue(muelleALiberar.Ship);
        }
    }
}
