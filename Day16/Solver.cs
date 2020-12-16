using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day16
{
    internal class Rule
    {
        public Rule(string s)
        {
            var match = new Regex(@"^(.+)\: ([0-9]+)-([0-9]+) or ([0-9]+)-([0-9]+)$").Match(s);
            Name = match.Groups[1].Value;
            From1 = Int64.Parse(match.Groups[2].Value);
            To1 = Int64.Parse(match.Groups[3].Value);
            From2 = Int64.Parse(match.Groups[4].Value);
            To2 = Int64.Parse(match.Groups[5].Value);
        }

        public string Name { get; }
        public Int64 From1 { get; }
        public Int64 To1 { get; }
        public Int64 From2 { get; }
        public Int64 To2 { get; }

        public bool IsValid(Int64 v) => (v >= From1 && v <= To1) || (v >= From2 && v <= To2);

    }

    class Solver
    {

        public string Solve1()
        {
            var input = File.ReadAllLines(@"Day16\input.txt").ToArray();
            var rules = input.Take(Array.IndexOf(input, "your ticket:") - 1).Select(s => new Rule(s)).ToArray();
            var yourTicket = new Ticket(input.Skip(Array.IndexOf(input, "your ticket:") + 1).Take(1).First());
            var nearbyTickets = input.Skip(Array.IndexOf(input, "nearby tickets:") + 1).Select(s => new Ticket(s)).ToArray();

            var errorRate = nearbyTickets.SelectMany(t => t.Values).Where(v => rules.All(r => !r.IsValid(v))).Sum();
            return errorRate.ToString();
        }



        public string Solve2()
        {
            var input = File.ReadAllLines(@"Day16\input.txt").ToArray();
            var rules = input.Take(Array.IndexOf(input, "your ticket:") - 1).Select(s => new Rule(s)).ToArray();
            var yourTicket = new Ticket(input.Skip(Array.IndexOf(input, "your ticket:") + 1).Take(1).First());
            var nearbyTickets = input.Skip(Array.IndexOf(input, "nearby tickets:") + 1).Select(s => new Ticket(s)).ToArray();
            var validTickets = nearbyTickets.Where(t => t.Values.All(v => rules.Any(r => r.IsValid(v)))).ToArray();

            var fieldNames = new Dictionary<int, string>();
            var candidates = Enumerable.Range(0, nearbyTickets.First().Values.Length).Select(i => rules.Where(r => validTickets.All(v => r.IsValid(v.Values[i]))).ToArray()).ToArray();



            //Horrible way to remove duplicates
            var success = false;
            do
            {
                var withOnlyOne = candidates.FirstOrDefault(o => o.Length == 1 && candidates.Any(c2 => c2.Length > 1 && c2.Contains(o[0])));
                if (withOnlyOne != null)
                {
                    success = true;
                    for (int i = 0; i < candidates.Length; i++)
                    {
                        if (candidates[i].Length > 1)
                        candidates[i] = candidates[i].Except(withOnlyOne).ToArray();
                    }

                }
                else
                {
                    success = false;
                }

            } while (success);


            return candidates.Select((o, i) => (o[0].Name, i)).Where(o => o.Name.StartsWith("departure")).Select(o => yourTicket.Values[o.i]).Aggregate((a, b) => a * b).ToString();
       
        }
    }

    internal class Ticket
    {
        public Int64[] Values { get; }

        public Ticket(string v)
        {
            Values = v.Split(',').Select(Int64.Parse).ToArray();
        }
    }
}
