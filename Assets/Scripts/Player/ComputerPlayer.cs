using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(int playerId, PieceColour colour, InternalBoard internalBoard) :
            base(playerId, colour, internalBoard)
        { }

        public void makeMove()
        {

        }
    }
}