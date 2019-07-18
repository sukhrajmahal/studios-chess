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
