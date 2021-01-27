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

        public Tuple<double, double, Queue<Ship>> StartSimulation(double duration)
        {
            double waitAverage = 0;
            double waitAverageDock = 0;
            if (DocksCount > 0)
            {
                duration = duration * 60 * 60;
                InitializeSistem();
                while (time < duration)
                {
                    Dock dockToFree = nextDockToFree();
                    if ((dockToFree == null || arrival <= dockToFree.LoadEnd))
                    {
                        if (arrival >= duration)
                            break;

                        time = Math.Max(time, arrival);
                        ShipType shipType = GenerateShipType(random);
                        Ship newShip = new Ship(shipType, arrival);
                        Dock freeDock = GetFreeDock();
                        bool existFreeDock = freeDock != null;
                        if (existFreeDock)
                        {
                            if (tugboatInDock)
                                time += GenerateFreeDockTransfer();
                            TakeShipToDock(newShip, freeDock);
                        }
                        else
                            shipsInHold.Enqueue(newShip);
                        arrival += GenerateArrivalHarbor();
                    }
                    else if (dockToFree != null && dockToFree.LoadEnd < arrival)
                    {
                        if (dockToFree.LoadEnd >= duration)
                            break;

                        time = Math.Max(time, dockToFree.LoadEnd);
                        if (!tugboatInDock)
                            time += GenerateFreeDockTransfer();

                        TakeShipFromHarbor(dockToFree);

                        waitAverage += (dockToFree.Ship.EntranceToHarbor - dockToFree.Ship.ArraivalToHarbor + dockToFree.Ship.ExitFromDock - dockToFree.Ship.LoadEnd);
                        waitAverageDock += (dockToFree.Ship.ExitFromDock - dockToFree.Ship.ArrivalToDock);
                        dockToFree.Ship = null;

                        if (shipsInHold.Count > 0)
                        {
                            Ship shipToEnter = shipsInHold.Dequeue();
                            TakeShipToDock(shipToEnter, dockToFree);
                        }
                    }
                }
                waitAverage = waitAverage / (double)shipsAttended.Count / 60 / 60;
                waitAverageDock = waitAverageDock / (double)shipsAttended.Count / 60 / 60;
            }

            return new Tuple<double, double, Queue<Ship>>(waitAverage, waitAverageDock, shipsAttended);
        }

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

        private Dock nextDockToFree()
        {
            Dock freeDock = null;
            double smaller = double.MaxValue;
            foreach (var m in docks)
                if (m.Ship != null && m.LoadEnd < smaller)
                {
                    smaller = m.LoadEnd;
                    freeDock = m;
                }
            return freeDock;
        }

        private ShipType GenerateShipType(Random r)
        {
            int valor = r.Next(4);
            if (valor == 0)
                return ShipType.Small;
            else if (valor == 1)
                return ShipType.Medium;
            else
                return ShipType.Large;
        }

        private double GenerateArrivalHarbor()
        {
            double d = Distribution.Exponential(random, 0.125);
            return d * 60 * 60;
        }

        private double GenerateFreeDockTransfer()
        {
            double d = Distribution.Exponential(random, 0.6666666);
            return d * 60;
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
