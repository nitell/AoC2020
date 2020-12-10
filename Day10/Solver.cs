using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day10
{
    class Solver
    {
        public string Solve1()
        {

            var data = File.ReadAllLines(@"Day10\input.txt").Select(Int64.Parse).OrderBy(i => i).ToArray();
            var connected = new[] { 0l }.Union(data).Union(new[] { data.Max() + 3 }).ToArray();
            var diffs = Enumerable.Range(0, connected.Length - 1).Select(i => connected[i + 1] - connected[i]);
            return (diffs.Where(d => d == 1).Count() * diffs.Where(d => d == 3).Count()).ToString();
        }

        public string Solve2()
        {
            var data = File.ReadAllLines(@"Day10\input.txt").Select(Int64.Parse).OrderBy(i => i).ToArray();
            var connected = new[] { 0l }.Union(data).Union(new[] { data.Max() + 3 }).ToArray();
            var incomingPathCounts = new Int64[connected.Length]; //Could probably use a sliding window instead, but.. not necessary
            incomingPathCounts[0] = 1;
            for(Int64 i=0;i<connected.Length;i++)
            {
                for (Int64 j = 1; j <= 3; j++)
                {
                    if (i + j < connected.Length && connected[i + j] - connected[i] <= 3)
                        incomingPathCounts[i + j] = incomingPathCounts[i + j] + incomingPathCounts[i];
                }
            }

            return incomingPathCounts.Last().ToString();
        }
    }
}
