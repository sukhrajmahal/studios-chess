using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public struct PossibleMove
    {
        private Point position;
        private bool killMove;

        public PossibleMove(Point position, bool killMove)
        {
            this.position = position;
            this.killMove = killMove;
        }
    }
}
