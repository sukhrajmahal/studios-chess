using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public struct PossibleMove
    {
        public Point position;
        public bool isKillMove;
        public Piece possibleKill;

        public PossibleMove(Point position)
        {
            this.position = position;
            this.isKillMove = false;
            this.possibleKill = null;
        }

        public PossibleMove(Point position, Piece killablePiece)
        {
            this.position = position;
            this.isKillMove = true;
            this.possibleKill = killablePiece;
        }
    }
}
