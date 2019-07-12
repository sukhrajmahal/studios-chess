using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public struct PossibleMove
    {
        public Point position;
        public bool isKillMove;

        public PossibleMove(Point position, bool killMove)
        {
            this.position = position;
            this.isKillMove = killMove;
        }
    }
}
