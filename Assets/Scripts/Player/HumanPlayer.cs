using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public class HumanPlayer : Player
    {
        private Piece selectedPiece;
        private List<PossibleMove> possibleMoves;

        public HumanPlayer(int playerId, PieceColour colour, InternalBoard internalBoard) : 
            base(playerId, colour, internalBoard)
        { }

        public void handleClick()
        {

        }
    }
}
