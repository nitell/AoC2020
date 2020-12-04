using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC.Day02
{
    class Solver
    {
        internal string Solve1()
        {
            var valid = File.ReadAllLines(@"Day02\input.txt").Where(s =>
            {
                var split = s.Split(' ');
                var minmax = split[0].Split('-');
                var minOccurs = int.Parse(minmax[0]);
                var maxOccurs = int.Parse(minmax[1]);
                var letter = split[1][0];
                var password = split[2];

                var letterCount = password.Where(c => c == letter).Count();
                return letterCount >= minOccurs && letterCount <= maxOccurs;
            });

            return valid.Count().ToString();
        }

        internal string Solve2()
        {

            var valid = File.ReadAllLines(@"Day02\input.txt").Where(s =>
            {
                var split = s.Split(' ');
                var minmax = split[0].Split('-');
                var pos1 = int.Parse(minmax[0]);
                var pos2 = int.Parse(minmax[1]);
                var letter = split[1][0];
                var password = split[2];


                return password[pos1 - 1] == letter ^ password[pos2 - 1] == letter;
            });

            return valid.Count().ToString();
        }
    }
}
