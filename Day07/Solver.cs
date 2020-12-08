using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day07
{
    class BagGraph
    {
        Dictionary<string, Bag> Bags { get; } = new Dictionary<string, Bag>();
        internal void Add(BagParse bag)
        {
            var children = bag.CanContain.Select(c =>
            {
                if (!Bags.TryGetValue(c.Item2, out var b))
                {
                    b = new Bag(c.Item2);
                    Bags[c.Item2] = b;
                }
                return (c.Item1, b);
            });

            if (!Bags.TryGetValue(bag.Name, out var parent))
            {
                parent = new Bag(bag.Name);
                Bags[bag.Name] = parent;
            }
            foreach (var c in children)
                Bags[bag.Name].Children.Add(c);
        }

        internal IEnumerable<Bag> CanContain(string name)
        {
            return Bags.Values.Where(b => b.CanContain(name));
        }

        internal int CountRequiredBags(string name)
        {
            return Bags[name].RequiredBags-1;
        }
    }

    class Bag
    {
        public string Name { get; }
        public HashSet<(int, Bag)> Children { get; }
        public int RequiredBags => Children.Any() ? 1+ Children.Sum(c => c.Item1 * c.Item2.RequiredBags) : 1; 

        public Bag(string name)
        {
            Name = name;
            Children = Enumerable.Empty<(int, Bag)>().ToHashSet();
        }

        public bool CanContain(string name)
        {
            return Children.Any(c => c.Item2.Name == name || c.Item2.CanContain(name));
        }
    }


    class BagParse
    {
        public BagParse(string input)
        {
            var noBagsMatch = new Regex(@"^(\S+ \S+) contain no other bags.$").Match(input);
            if (noBagsMatch.Success)
            {
                Name = noBagsMatch.Groups[1].Value;
                CanContain = Enumerable.Empty<(int, string)>();
            }
            else
            {
                var match = new Regex(@"^(.+) bags contain (([0-9])+ (\S+ \S+) (bag|bags)(, )*)+.$").Match(input);
                Name = match.Groups[1].Value;
                CanContain = match.Groups[4].Captures.Select((c, i) => (int.Parse(match.Groups[3].Captures[i].Value), c.Value)).ToArray();
            }
        }

        public string Name { get; }
        public IEnumerable<(int, string)> CanContain { get; }
    }


    class Solver
    {
        internal string Solve1()
        {
            var bags = new BagGraph();
            var records = File.ReadAllLines(@"Day07\input.txt");
            foreach (var r in records)
            {
                var bag = new BagParse(r);
                bags.Add(bag);
            }


            return bags.CanContain("shiny gold").Count().ToString();
        }

        internal string Solve2()
        {
            var bags = new BagGraph();
            var records = File.ReadAllLines(@"Day07\input.txt");
            foreach (var r in records)
            {
                var bag = new BagParse(r);
                bags.Add(bag);
            }


            return bags.CountRequiredBags("shiny gold").ToString();
        }
    }
}
