using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overloaded_Harbor
{
    public static class Distribution
    {
        public static double Exponential(Random random, double lambda)
        {
            double u = random.NextDouble();
            return (-1 / lambda) * Math.Log(u);
        }

        public static double Normal(Random random, double mu, double sigma)
        {
            double a = random.NextDouble();
            double b = random.NextDouble();
            double c = Math.Sqrt(-2 * Math.Log(a)) * Math.Cos(2 * Math.PI * b);
            return Math.Sqrt(sigma) * c + mu;
        }
    }
}