using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC.Day01
{
    class Solver
    {
        internal string Solve1()
        {
            var input = File.ReadAllLines(@"Day01\input.txt").Select(int.Parse).ToArray();
            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[i] + input[j] == 2020)
                        return (input[i] * input[j]).ToString();
                }
            }
            return String.Empty;
        }

        internal string Solve2()
        {
            var input = File.ReadAllLines(@"Day01\input.txt").Select(int.Parse).ToArray();
            for (int i = 0; i < input.Length - 2; i++)
            {
                for (int j = i + 1; j < input.Length - 1; j++)
                {
                    for (int k = j + 1; k < input.Length; k++)
                        if (input[i] + input[j] + input[k] == 2020)
                            return (input[i] * input[j] * input[k]).ToString();
                }
            }
            return String.Empty;
        }
    }
}
