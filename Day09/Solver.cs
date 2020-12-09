using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day09
{
    class Solver
    {
        internal string Solve1()
        {

            var data = File.ReadAllLines(@"Day09\input.txt").Select(Int64.Parse).ToArray();
            var size = 25;
            for (int i = size; i < data.Length; i++)
            {
                var candidates = data.Skip(i - size).Take(size).ToArray();
                var permutatedSums = PermutateAndSum(candidates).ToArray();
                if (!permutatedSums.Contains(data[i]))
                    return data[i].ToString();
            }

            throw new Exception("This should not happen");
        }

        private IEnumerable<Int64> PermutateAndSum(Int64[] candidates)
        {
            for (int i = 0; i < candidates.Length - 1; i++)
                for (int j = i + 1; j < candidates.Length; j++)
                    yield return candidates[i] + candidates[j];
        }

        internal string Solve2()
        {            
            var data = File.ReadAllLines(@"Day09\input.txt").Select(Int64.Parse).ToArray();
            var size = 25;
            for (int i = size; i < data.Length; i++)
            {
                for (int j = 1; i + j < data.Length; j++) //Could break if sum > 1930745883 but this is quick enough...
                {
                    var candidates = data.Skip(i).Take(j).ToArray();
                    if (candidates.Sum(c => c) == 1930745883)
                        return (candidates.Min() + candidates.Max()).ToString();
                }
            }
            throw new Exception("This should not happen");
        }
    }
}
