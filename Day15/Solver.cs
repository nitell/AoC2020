using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day15
{
    

    class Solver
    {

        public string Solve1()
        {            
            var history = new List<int>(File.ReadAllText(@"Day15\input.txt").Split(',').Select(int.Parse));
            while (history.Count < 2020)
            {                
                var lastSpoken = history.Last();
                var beforeThat = history.Select((c,i)=>(c,i)).OrderByDescending(o=>o.i).Where(i => i.c == lastSpoken).Take(2).Select(i=>i.i).ToArray();
                switch(beforeThat.Length)
                {
                    case 1:
                        history = history.Append(0).ToList();
                        break;
                    case 2:
                        history = history.Append(beforeThat.First() - beforeThat.Last()).ToList();
                        break;                  
                }              
            }

            return history.Last().ToString();
        }



        public string Solve2()
        {            
            var input = File.ReadAllText(@"Day15\input.txt").Split(',').Select(int.Parse).ToArray();
            var step = input.Length + 1;
            var dict = input.Select((c, i) => (c, i)).ToDictionary(o => o.c, o => (-1, o.i+1));
            var lastNumber = input.Last();
            while (step <= 30000000)
            {
                if (dict[lastNumber].Item1 == -1)
                {
                    lastNumber = 0;
                    Speak(dict, lastNumber, step);
                }   
                else
                {
                    lastNumber = dict[lastNumber].Item2 - dict[lastNumber].Item1;
                    Speak(dict, lastNumber, step);
                }
                step++;
            }

            return lastNumber.ToString();



        }

        private void Speak(Dictionary<int, (int, int)> dict, int lastNumber, int step)
        {
            if (!dict.ContainsKey(lastNumber))
                dict[lastNumber] = (-1, step);
            else
                dict[lastNumber] = (dict[lastNumber].Item2, step);
        }
    }    
}
