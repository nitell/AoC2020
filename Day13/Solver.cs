using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day13
{
    class Solver
    {

        public string Solve1()
        {
            var input = File.ReadAllLines(@"Day13\input.txt");
            var earliest = int.Parse(input[0]);
            var busses = input[1].Split(',').Where(s => s != "x").Select(int.Parse);

            var wait = busses.Select(b => (buss: b, wait: earliest % b == 0 ? 0 : b + (earliest / b) * b - earliest));
            var bus = wait.OrderBy(b => b.wait).First();
            return (bus.wait * bus.buss).ToString();
        }


        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long LCM(long a, long b)
        {
            return (a / GCD(a, b)) * b;
        }


        public string Solve2()
        {            
            var input = File.ReadAllLines(@"Day13\input.txt");            
            var busses = input[1].Split(',').Select((s, i) => (buss: s=="x" ? 0 : Int64.Parse(s), index: i)).Where(b=>b.buss >0).OrderByDescending(t => t.Item1).ToArray();
            var biggestBus = busses.First();

            //earliest possible answer is the minute that makes last bus start on time
            var minute = biggestBus.buss - biggestBus.index;
            //And the next possible candidate is next on the biggest bus schedule
            var step = biggestBus.buss;

            for (var busIndex = 1; busIndex <= busses.Length; busIndex++)
            {
                //Step minute until we find the minute where the next biggest bus leaves
                while (busses.Take(busIndex).Any(t => (minute + t.index) % t.Item1 != 0))
                {
                    minute += step;
                }

                //For the next bus, step with the smallest common divisor for the busses;
                step = busses.Take(busIndex).Select(t => t.buss).Aggregate(LCM);
            }

            return minute.ToString();
        }
    }
}
