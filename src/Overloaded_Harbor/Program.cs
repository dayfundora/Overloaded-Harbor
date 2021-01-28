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
            for (int i = 0; i < 10; i++)
            {

                Tuple<double, double, Queue<Ship>> datos;
                using (StreamReader sr = new StreamReader("in.txt"))
                {
                    var time = long.Parse(sr.ReadLine());
                    Harbor harbor = new Harbor();
                    datos = harbor.StartSimulation(time);
                }

                using (StreamWriter sw = new StreamWriter("out" + i + ".txt"))
                {
                    sw.WriteLine("Average Wait in Harbor: " + datos.Item1);
                    sw.WriteLine("Average Wait in Dock: " + datos.Item2);

                    foreach (var item in datos.Item3)
                        sw.WriteLine(item.Type + "    " + item.WaitTime + "    " + item.LoadTime);
                }
                Process.Start("out" + i + ".txt");
            }
        }
    }
}
