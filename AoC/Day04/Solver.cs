using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day04
{   
    class Solver
    {       
        internal string Solve1()
        {
            var required = new[] {"byr","iyr","eyr","hgt","hcl","ecl","pid"};

            var records = File.ReadAllText(@"Day04\input.txt").Split(Environment.NewLine + Environment.NewLine);
            var keys = records.Select(s => s.Replace(Environment.NewLine," ").Split(' ').Select(sp => sp.Split(':')[0]));
            return keys.Where(k => required.All(r => k.Contains(r))).Count().ToString();
                
        }

        internal string Solve2()
        {
            Dictionary<string, Predicate<string>> rules = new()
            {
                ["byr"] = s => s.Length == 4 && IsBetween(s, 1920, 2002),
                ["iyr"] = s => s.Length == 4 && IsBetween(s, 2010, 2020),
                ["eyr"] = s => s.Length == 4 && IsBetween(s, 2020, 2030),
                ["hgt"] = s => new Regex("^[0-9]+(cm|in)$").IsMatch(s) && (s.EndsWith("cm") ? IsBetween(s.Substring(0, s.Length - 2), 150, 193) : IsBetween(s.Substring(0, s.Length - 2), 59, 76)),
                ["hcl"] = s => new Regex("^#[0-9a-f]{6}$").IsMatch(s),
                ["ecl"] = s => new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(s),
                ["hcl"] = s => new Regex("^#[0-9a-f]{6}$").IsMatch(s),
                ["pid"] = s => new Regex(@"^\d{9}$").IsMatch(s)
            };

            var records = File.ReadAllText(@"Day04\input.txt").Split(Environment.NewLine + Environment.NewLine);
            var keyValueRows = records.Select(s => s.Replace(Environment.NewLine, " ").Split(' ').Select(sp =>
            {
                var keyValue = sp.Split(':');
                return (Key: keyValue[0], Value: keyValue[1]);
            }));

            return keyValueRows.Where(kvs => rules.All(r => kvs.Any(kv => kv.Key == r.Key))).Where(kvs => kvs.All(kv => kv.Key == "cid" || rules[kv.Key](kv.Value))).Count().ToString();
        }

        bool IsBetween(string s, int min, int max)
        {
            return int.Parse(s) >= min && int.Parse(s) <= max;
        }
    }
}
