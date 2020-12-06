using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day06
{
    class Solver
    {
        internal string Solve1()
        {
            var records = File.ReadAllText(@"Day06\input.txt").Split(Environment.NewLine + Environment.NewLine);
            return records.Sum(r => r.Replace(Environment.NewLine, String.Empty).Distinct().Count()).ToString();            
        }

        internal string Solve2()
        {
            var records = File.ReadAllText(@"Day06\input.txt").Split(Environment.NewLine + Environment.NewLine);
            return records.Sum(r => r.Split(Environment.NewLine).Aggregate((a, b) => new String(a.Intersect(b).ToArray())).Count()).ToString();
        }
    }
}
