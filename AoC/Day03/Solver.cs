using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC.Day03
{
    class TreeRow
    {
        private readonly string _s;

        public TreeRow(string s)
        {
            _s = s;
        }

        public bool IsTree(int pos)
        {
            return _s[pos % _s.Length] == '#';
        }
    }

    class Solver
    {
        Int64 CountTreeHits(IEnumerable<TreeRow>rows, int moveRight)
        {
            var pos = 0;
           
            var treeHits = rows.Where((r, index) => r.IsTree(index * moveRight));
            return treeHits.Count();
        }

        internal string Solve1()
        {
            var rows = File.ReadAllLines(@"Day03\input.txt").Select(s => new TreeRow(s));
            return CountTreeHits(rows,3).ToString();
        }

        internal string Solve2()
        {
            var rows = File.ReadAllLines(@"Day03\input.txt").Select(s => new TreeRow(s));
            return (CountTreeHits(rows, 1) * CountTreeHits(rows, 3) * CountTreeHits(rows, 5) * CountTreeHits(rows, 7) * CountTreeHits(rows.Where((r, i) => i % 2 == 0), 1)).ToString();                       
        }
    }
}
