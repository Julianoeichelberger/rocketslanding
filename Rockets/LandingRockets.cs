using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LandingRockets
{
    public class PlataformSizeExceededLimits : Exception
    {
    }  
    public class Response
    {
        public const string Ok = "ok for landing";
        public const string OutOfPlatform = "out of platform";
        public const string Clash = "clash";
    } 

    public class Coordinate
    {
        private readonly List<Point> SurroundedBy;
        public Coordinate(int x, int y)
        {
            this.Point = new Point(x, y);

            this.SurroundedBy = new List<Point>
            {
                new Point(x - 1, y - 1),
                new Point(x - 1, y),
                new Point(x - 1, y + 1),
                new Point(x, y + 1),
                new Point(x, y - 1),
                new Point(x + 1, y - 1),
                new Point(x + 1, y),
                new Point(x + 1, y + 1),
            };
        }  
        public bool EqualsTo(Point point)
        { 
            return this.Point.Equals(point);
        } 

        public bool ConflictWith(Coordinate compare)
        { 
            if (this.EqualsTo(compare.Point)) { return true; }

            return this.SurroundedBy
                .Where(p => compare.EqualsTo(p)).Any(); 
        }

        public Point Point { get; } 
    }

    public class Plataform
    {
        private const int InitialX = 5;
        private const int InitialY = 5;

        private readonly int LimitX;
        private readonly int LimitY;

        public Plataform(int SizeX = 10, int SizeY = 10)
        {
            this.LimitX = InitialX + SizeX;
            this.LimitY = InitialY + SizeY; 
            if (this.LimitX > Landing.TotalAreaX || this.LimitY > Landing.TotalAreaY)
                throw new PlataformSizeExceededLimits();
        }

        public bool IsInRange(Coordinate coordinate)
        {
            return ((coordinate.Point.X >= InitialX) &&
                    (coordinate.Point.X <= LimitX) &&
                    (coordinate.Point.Y >= InitialY) &&
                    (coordinate.Point.Y <= LimitY));
        }
    }

    public class Landing
    {
        public const int StartAreaX = 0;
        public const int StartAreaY = 0;
        public const int TotalAreaX = 100;
        public const int TotalAreaY = 100;

        private readonly Plataform plataform;  
        private Coordinate? Reserved;  
        private readonly object _lock = new object();

        public Landing(Plataform plataform)
        {
            this.plataform = plataform; 
        } 

        public string Check(Coordinate coordinate)
        {
            lock (_lock)
            { 
                if (!this.plataform.IsInRange(coordinate))
                {
                    return Response.OutOfPlatform;
                }

                if (this.Reserved != null && 
                    this.Reserved.ConflictWith(coordinate))
                {
                    return Response.Clash;
                }; 
                 
                this.Reserved = coordinate;

                return Response.Ok;
            }; 
        } 
    }
}