using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day08
{
    class GameConsole
    {
        public int Accumulator;
        public int InstructionPointer;

        Dictionary<string, Action<int>> _instructions = new Dictionary<string, Action<int>>();

        public GameConsole()
        {
            _instructions["acc"] = i =>
            {
                Accumulator += i;
                InstructionPointer++;
            };

            _instructions["jmp"] = i =>
            {
                InstructionPointer += i;
            };

            _instructions["nop"] = i =>
            {
                InstructionPointer++;
            };
        }

        public bool TryTerminate(string[] program)
        {
            InstructionPointer = 0;
            var ipHistory = new List<int>();
            while (true)
            {
                ipHistory.Add(InstructionPointer);
                var instruction = program[InstructionPointer].Split(' ');
                _instructions[instruction[0]](int.Parse(instruction[1]));
                if (ipHistory.Contains(InstructionPointer))
                    return false;
                if (InstructionPointer >= program.Length)
                    return true;

            }
        }
    }


    class Solver
    {
        internal string Solve1()
        {
            var c = new GameConsole();
            c.TryTerminate(File.ReadAllLines(@"Day08\input.txt"));

            return c.Accumulator.ToString();
        }

        internal string Solve2()
        {
            
            var program = File.ReadAllLines(@"Day08\input.txt");
            for(int i=0;i<program.Length;i++)
            {
                if (!program[i].StartsWith("acc"))
                {
                    var c = new GameConsole();
                    var bkup = program[i];
                    program[i] = program[i].StartsWith("jmp")? program[i].Replace("jmp","nop") : program[i].Replace("nop", "jmp");
                    if (c.TryTerminate(program))
                        return c.Accumulator.ToString();
                    program[i] = bkup;
                }
            }

            return "This.. is not supposed to happen...";
        }
    }
}
