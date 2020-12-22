using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day17
{
    public record Coordinate
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Coordinate(int x, int y, int z) => (X, Y, Z) = (x, y, z);
    }

    public class Grid
    {
        HashSet<Coordinate> _grid = new HashSet<Coordinate>();


        public void SetActive(int x, int y, int z)
        {
            _grid.Add(new Coordinate(x, y, z));
        }

        public bool IsActive(Coordinate c)
        {
            return _grid.Contains(c);
        }

        public void Step()
        {
            var allCellsToCheck = new HashSet<Coordinate>(_grid).Union(_grid.SelectMany(o => GetNeighbors(o)));
            var newGrid = new HashSet<Coordinate>(_grid);

            foreach (var c in allCellsToCheck)
            {
                var neighbors = GetNeighbors(c).ToArray();
                var activeNeighbors = neighbors.Where(n => _grid.Contains(n)).ToArray();
                if (_grid.Contains(c) && activeNeighbors.Count() < 2 || activeNeighbors.Count() > 3)
                    newGrid.Remove(c);
                if (!_grid.Contains(c) && activeNeighbors.Count() == 3)
                    newGrid.Add(c);
            }
            _grid = newGrid;
        }

        public IEnumerable<Coordinate> GetNeighbors(Coordinate c)
        {
            var permutations = new[] { -1, 0, 1 }.SelectMany(x => new[] { -1, 0, 1 }.Select(y => (x, y))).SelectMany(o => new[] { -1, 0, 1 }.Select(z => (o.x, o.y, z)));
            return permutations.Where(o => o != (0, 0, 0)).Select(o => new Coordinate(c.X + o.x, c.Y + o.y, c.Z + o.z));
        }

        public IEnumerable<Coordinate> Coordinates => _grid;

        public int ActiveCount => _grid.Count();

    }

    public record Coordinate4
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int W { get; }

        public Coordinate4(int x, int y, int z, int w) => (X, Y, Z, W) = (x, y, z, w);
    }

    public class Grid4
    {
        HashSet<Coordinate4> _grid = new HashSet<Coordinate4>();


        public void SetActive(int x, int y, int z, int w)
        {
            _grid.Add(new Coordinate4(x, y, z, w));
        }

        public bool IsActive(Coordinate4 c)
        {
            return _grid.Contains(c);
        }

        public void Step()
        {
            var allCellsToCheck = new HashSet<Coordinate4>(_grid).Union(_grid.SelectMany(o => GetNeighbors(o)));
            var newGrid = new HashSet<Coordinate4>(_grid);

            foreach (var c in allCellsToCheck)
            {
                var neighbors = GetNeighbors(c).ToArray();
                var activeNeighbors = neighbors.Where(n => _grid.Contains(n)).ToArray();
                if (_grid.Contains(c) && activeNeighbors.Count() < 2 || activeNeighbors.Count() > 3)
                    newGrid.Remove(c);
                if (!_grid.Contains(c) && activeNeighbors.Count() == 3)
                    newGrid.Add(c);
            }
            _grid = newGrid;
        }

        public IEnumerable<Coordinate4> GetNeighbors(Coordinate4 c)
        {
            var permutations = new[] { -1, 0, 1 }.SelectMany(x => new[] { -1, 0, 1 }.Select(y => (x, y))).SelectMany(o => new[] { -1, 0, 1 }.Select(z => (o.x, o.y, z))).SelectMany(o => new[] { -1, 0, 1 }.Select(w => (o.x, o.y, o.z, w)));
            return permutations.Where(o => o != (0, 0, 0, 0)).Select(o => new Coordinate4(c.X + o.x, c.Y + o.y, c.Z + o.z, c.W + o.w));
        }

        public IEnumerable<Coordinate4> Coordinates => _grid;

        public int ActiveCount => _grid.Count();

    }



    class Solver
    {

        public string Solve1()
        {

            var grid = new Grid();
            var lines = File.ReadAllLines(@"Day17\input.txt").ToArray();
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                        grid.SetActive(x, y, 0);
                }
            }

            for (int step = 1; step <= 6; step++)
            {
                grid.Step();
                //Console.WriteLine($"After {step} cycles");
                //for (var z = grid.Coordinates.Select(o => o.Z).Min(); z <= grid.Coordinates.Select(o => o.Z).Max(); z++)
                //{
                //    Console.WriteLine($"z={z}");
                //    for (var y = grid.Coordinates.Select(o => o.Y).Min(); y <= grid.Coordinates.Select(o => o.Y).Max(); y++)
                //    {
                //        for (var x = grid.Coordinates.Select(o => o.X).Min(); x <= grid.Coordinates.Select(o => o.X).Max(); x++)
                //        {
                //            Console.Write(grid.Coordinates.Contains(new Coordinate(x, y, z)) ? "#" : ".");
                //        }
                //        Console.WriteLine();
                //    }
                //    Console.WriteLine();
                //    Console.WriteLine();
                //}

            }
            return grid.ActiveCount.ToString();
        }



        public string Solve2()
        {
            var grid = new Grid4();
            var lines = File.ReadAllLines(@"Day17\input.txt").ToArray();
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                        grid.SetActive(x, y, 0, 0);
                }
            }

            for (int step = 1; step <= 6; step++)
            {
                grid.Step();
                //Console.WriteLine($"After {step} cycles");
                //for (var z = grid.Coordinates.Select(o => o.Z).Min(); z <= grid.Coordinates.Select(o => o.Z).Max(); z++)
                //{
                //    Console.WriteLine($"z={z}");
                //    for (var y = grid.Coordinates.Select(o => o.Y).Min(); y <= grid.Coordinates.Select(o => o.Y).Max(); y++)
                //    {
                //        for (var x = grid.Coordinates.Select(o => o.X).Min(); x <= grid.Coordinates.Select(o => o.X).Max(); x++)
                //        {
                //            Console.Write(grid.Coordinates.Contains(new Coordinate(x, y, z)) ? "#" : ".");
                //        }
                //        Console.WriteLine();
                //    }
                //    Console.WriteLine();
                //    Console.WriteLine();
                //}

            }
            return grid.ActiveCount.ToString();
        }

    }
}
