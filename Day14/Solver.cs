using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day14
{
    class Solver
    {

        public string Solve1()
        {
            var input = File.ReadAllLines(@"Day14\input.txt");
            var mask = new BitMask(new string('X', 36).ToArray());
            var memory = new Dictionary<long, long>();
            foreach(var l in input)
            {
                var maskRegex = new Regex(@"^mask \= (.+)$").Match(l);
                if (maskRegex.Success)
                {
                    mask = new BitMask(maskRegex.Groups[1].Value.ToArray());
                }
                else
                {
                    var m = new Regex(@"^mem\[([0-9]+)\] \= ([0-9]+)$").Match(l);
                    var memAddress = Int64.Parse(m.Groups[1].Value);
                    var value = mask.GetValue(Int64.Parse(m.Groups[2].Value));
                    memory[memAddress] = value;
                }

            }


            return memory.Values.Sum().ToString();
        }



        public string Solve2()
        {
            var input = File.ReadAllLines(@"Day14\input.txt");
            var mask = new BitMask(new string('X', 36).ToArray());
            var memory = new Dictionary<long, long>();
            foreach (var l in input)
            {
                var maskRegex = new Regex(@"^mask \= (.+)$").Match(l);
                if (maskRegex.Success)
                {
                    mask = new BitMask(maskRegex.Groups[1].Value.ToArray());
                }
                else
                {
                    var m = new Regex(@"^mem\[([0-9]+)\] \= ([0-9]+)$").Match(l);
                    var memAddresses = mask.GetAdresses(Int64.Parse(m.Groups[1].Value));
                    foreach (var address in memAddresses)
                        memory[address] = Int64.Parse(m.Groups[2].Value);                    
                }

            }
            return memory.Values.Sum().ToString();
        }

      
    }

    internal class BitMask
    {
        private char[] _m;

        public BitMask(char[] m)
        {
            _m = m;                            
        }

        public Int64 GetValue(long v)
        {
            var bitValue = GetBitArray(v);
            

            for (int i = 35; i >= 0; i--)
            {
                if (_m[i] == '1')
                    bitValue[i] = true;
                if (_m[i] == '0')
                    bitValue[i] = false;
            }

            var result = bitValue.Reverse().Select((b, i) => b ? (long) Math.Pow(2, i) : 0).Sum();
            return result;
        }

        internal IEnumerable<long> GetAdresses(long v, int startindex = 35)
        {
            var bitValue = GetBitArray(v);
            return GetAdresses(bitValue, 35);
        }

        private IEnumerable<long> GetAdresses(bool[] bitValue, int startindex)
        {            
            for (int i = startindex; i >= 0; i--)
            {
                switch (_m[i])
                {
                    case '0':
                        return GetAdresses(bitValue, i - 1);
                    case '1':
                        bitValue[i] = true;
                        return GetAdresses(bitValue, i - 1);
                    case 'X':
                        var low = (bool[]) bitValue.Clone();
                        low[i] = false;
                        var high = (bool[]) bitValue.Clone();
                        high[i] = true;
                        return GetAdresses(low, i - 1).Union(GetAdresses(high, i - 1));                            
                }
            }
            var result = bitValue.Reverse().Select((b, i) => b ? (long)Math.Pow(2, i) : 0).Sum();
            return new[] { result };
        }
             

        private bool[] GetBitArray(long v)
        {
            var bytes = BitConverter.GetBytes(v);
            var bits = new BitArray(bytes);
            var arr = new bool[64];
            bits.CopyTo(arr, 0);
            return arr.Reverse().Skip(64-36).ToArray();
        }
    }
}
