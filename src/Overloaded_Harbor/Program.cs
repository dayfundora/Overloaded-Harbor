using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Overloaded_Harbor
{
    class Program
    {
        static void Main(string[] args)
        {

                Tuple<double, double, Queue<Ship>> datos;
                int hours = 480;
                Harbor harbor = new Harbor();
                datos = harbor.StartSimulation(hours);

                Console.WriteLine("Average Wait in Harbor: " + datos.Item1);
                Console.WriteLine("Average Wait in Dock: " + datos.Item2);

                foreach (var item in datos.Item3)
                    Console.WriteLine(item.Type + "    " + item.WaitTime + "    " + item.LoadTime);
        }
    }
}
