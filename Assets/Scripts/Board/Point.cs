using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Point(int p1, int p2)
        {
            X = p1;
            Y = p2;
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator *(Point p1, int number)
        {
            return new Point(p1.X * number, p1.Y * number);
        }

        public bool IsOnBoard()
        {
            if (X >= 0 && X < GlobalVars.gridSize && Y >= 0 && Y < GlobalVars.gridSize)
            {
                return true;
            }
            return false;
        }
    }
}
