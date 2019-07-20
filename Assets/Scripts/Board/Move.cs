using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public class Move
    {
        public int PlayerID { get; set; }
        public Piece PieceMoved { get; set; }
        public Piece PieceTaken { get; set; }
        public Point OldPosition { get; set; }
        public Point NewPosition { get; set; }
        public int TurnNumber { get; set; }
    }
}
