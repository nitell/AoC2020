using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day12
{
    public class WayPointAndShip
    {
        public Point WaypointOffset { get; private set; } = new Point(10, 1);

        public Point ShipPos { get; private set; } = new Point(0, 0);
        

        internal void Act(string s)
        {
            var cmd = s[0];
            var param = int.Parse(new String(s.Skip(1).ToArray()));

            switch (cmd)
            {
                case 'N':
                    WaypointOffset = new Point(WaypointOffset.X, WaypointOffset.Y + param);
                    break;
                case 'S':
                    WaypointOffset = new Point(WaypointOffset.X, WaypointOffset.Y - param);
                    break;
                case 'E':
                    WaypointOffset = new Point(WaypointOffset.X + param, WaypointOffset.Y);
                    break;
                case 'W':
                    WaypointOffset = new Point(WaypointOffset.X - param, WaypointOffset.Y);
                    break;
                case 'R':
                    Rotate(param);
                    break;
                case 'L':
                    Rotate(-param);
                    break;
                case 'F':
                    ShipPos = new Point(ShipPos.X + WaypointOffset.X * param, ShipPos.Y + WaypointOffset.Y * param);
                    break;
            }

        }

        private void Rotate(int param)
        {
            var toRotate = (Math.Abs(param) % 360);
            if (param < 0)
                toRotate = 360 - toRotate;
         
            WaypointOffset = new Point(WaypointOffset.X * Cos(toRotate) + WaypointOffset.Y * Sin(toRotate), -WaypointOffset.X * Sin(toRotate) + WaypointOffset.Y * Cos(toRotate));
        }

        private int Sin(int toRotate)
        {
            switch(toRotate)
            {
                case 0:
                    return 0;
                case 90:
                    return 1;
                case 180:
                    return 0;
                case 270:
                    return -1;
            }
            throw new ArgumentException();
        }

        private int Cos(int toRotate)
        {
            switch (toRotate)
            {
                case 0:
                    return 1;
                case 90:
                    return 0;
                case 180:
                    return -1;
                case 270:
                    return 0;
            }
            throw new ArgumentException();
        }
    }

    public class Ship
    {
        char[] directions = new[] { 'E', 'S', 'W', 'N' };
        int currentDirection;       
        public Point CurrentPos { get; private set; } =  new Point(0, 0);

        internal void Act(string s)
        {
            var cmd = s[0];
            var param = int.Parse(new String(s.Skip(1).ToArray()));

            switch(cmd)
            {
                case 'N':
                    CurrentPos = new Point(CurrentPos.X, CurrentPos.Y + param);
                    break;
                case 'S':
                    CurrentPos = new Point(CurrentPos.X, CurrentPos.Y - param);
                    break;
                case 'E':
                    CurrentPos = new Point(CurrentPos.X+param, CurrentPos.Y);
                    break;
                case 'W':
                    CurrentPos = new Point(CurrentPos.X -param, CurrentPos.Y);
                    break;
                case 'R':
                    ChangeDirection(param);
                    break;
                case 'L':
                    ChangeDirection(-param);
                    break;
                case 'F':
                    Act($"{directions[currentDirection]}{param}");
                    break;
            }

        }

        private void ChangeDirection(int param)
        {
            currentDirection = (currentDirection + (param / 90)) % 4;
            if (currentDirection < 0)
                currentDirection = 4 + currentDirection;
        }
    }


    class Solver
    {
     
        public string Solve1()
        {
            var rows = File.ReadAllLines(@"Day12\input.txt");
            var ship = new Ship();
            foreach (var r in rows)
                ship.Act(r);

            return (Math.Abs(ship.CurrentPos.X) + Math.Abs(ship.CurrentPos.Y)).ToString();
        }

        public string Solve2()
        {
            var rows = File.ReadAllLines(@"Day12\input.txt");
            var ship = new WayPointAndShip();
            foreach (var r in rows)
                ship.Act(r);

            return (Math.Abs(ship.ShipPos.X) + Math.Abs(ship.ShipPos.Y)).ToString();
        }
    }
}
