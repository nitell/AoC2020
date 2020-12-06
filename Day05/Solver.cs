using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day05
{
    class Solver
    {
        internal string Solve1()
        {
            var records = File.ReadAllLines(@"Day05\input.txt");
            var max = 0;
            foreach(var record in records)            
            {
                var rows = Enumerable.Range(0, 128);
                foreach (var c in record.Take(7))
                {
                    rows = c == 'F' ? rows.Take(rows.Count() / 2) : rows.Skip(rows.Count() / 2);
                }
                
                var columns = Enumerable.Range(0, 8);
                foreach (var c in record.Skip(7))
                {
                    columns = c == 'L' ? columns.Take(columns.Count() / 2) : columns.Skip(columns.Count() / 2);
                }

                max = Math.Max(max, rows.First() * 8 + columns.First());                
            }
            return max.ToString();

        }

        internal string Solve2()
        {
            var records = File.ReadAllLines(@"Day05\input.txt");
            var takenSeats = new bool[128 * 8];
            foreach (var record in records)
            {
                var rows = Enumerable.Range(0, 128);
                foreach (var c in record.Take(7))
                {
                    rows = c == 'F' ? rows.Take(rows.Count() / 2) : rows.Skip(rows.Count() / 2);
                }

                var columns = Enumerable.Range(0, 8);
                foreach (var c in record.Skip(7))
                {
                    columns = c == 'L' ? columns.Take(columns.Count() / 2) : columns.Skip(columns.Count() / 2);
                }

                takenSeats[rows.First() * 8 + columns.First()] = true;
            }
            var firstUsedSeat = Array.IndexOf(takenSeats, true);
            return (Array.IndexOf(takenSeats.Skip(firstUsedSeat).ToArray(), false)+firstUsedSeat).ToString();
        }
    }
}
