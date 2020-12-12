using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day11
{
    class Solver
    {

        List<Func<Point, Point>> directions;

        public Solver()
        {
            directions = new List<Func<Point, Point>>();
            directions.Add(p => new Point(p.X + 1, p.Y));
            directions.Add(p => new Point(p.X + 1, p.Y + 1));
            directions.Add(p => new Point(p.X, p.Y + 1));

            directions.Add(p => new Point(p.X - 1, p.Y));
            directions.Add(p => new Point(p.X - 1, p.Y - 1));
            directions.Add(p => new Point(p.X, p.Y - 1));

            directions.Add(p => new Point(p.X + 1, p.Y - 1));
            directions.Add(p => new Point(p.X - 1, p.Y + 1));
        }

        public string Solve1()
        {

            var rows = File.ReadAllLines(@"Day11\input.txt").Select(s => s.ToArray()).ToArray();
            var previous = new char[rows.Length][];
            while (!AreEqual(rows, previous))
            {
                previous = rows;
                rows = Step(rows, GetAdjacent,4);
            }

            return rows.SelectMany(s => s).Where(s => s == '#').Count().ToString();

        }

        private bool AreEqual(char[][] a, char[][] b)
        {
            for (int i = 0; i < a.Length; i++)
                if (b[i] == null || !Enumerable.SequenceEqual(a[i], b[i]))
                    return false;
            return true;
        }

        private char[][] Step(char[][] state, Func<char[][], int, int, IEnumerable<char>> getImpactingSeats, int thresHold)
        {
            var newState = state.Select(r => (char[])r.Clone()).ToArray();
            for (int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state[i].Length; j++)
                {
                    var impactingSeats = getImpactingSeats(state, i, j).ToArray();
                    if (state[i][j] == 'L' && !impactingSeats.Any())
                        newState[i][j] = '#';
                    else if (state[i][j] == '#' && impactingSeats.Count() >= thresHold)
                        newState[i][j] = 'L';
                }
            }
            return newState;
        }


        private IEnumerable<char> GetInSight(char[][] state, int x, int y)
        {
            foreach (var dir in directions)
            {
                var p = dir(new Point(x, y));
                while (IsInBounds(state, p))
                {                    
                    if (state[p.X][p.Y] == '#')
                    {
                        yield return '#';
                        break;
                    }

                    if (state[p.X][p.Y] == 'L')
                        break;

                    p = dir(p);
                } 
            }
        }



        private IEnumerable<char> GetAdjacent(char[][] state, int x, int y)
        {
            return directions.Select(d => d(new Point(x, y))).Where(p => IsInBounds(state, p)).Select(p => state[p.X][p.Y]).Where(c => c == '#');
        }

        private bool IsInBounds(char[][] state, Point p)
        {
            return p.X >= 0 && p.X < state.Length && p.Y >= 0 && p.Y < state[p.X].Length;
        }

        public string Solve2()
        {
            var rows = File.ReadAllLines(@"Day11\input.txt").Select(s => s.ToArray()).ToArray();
            var previous = new char[rows.Length][];
            while (!AreEqual(rows, previous))
            {
                previous = rows;
                rows = Step(rows, GetInSight,5);
            }

            return rows.SelectMany(s => s).Where(s => s == '#').Count().ToString();
        }
    }
}
