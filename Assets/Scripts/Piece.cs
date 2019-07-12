using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public enum PieceColour { Black, White, Red, Yellow };
    public enum PieceType { King, Queen, Bishop, Knight, Rook, Pawn };

    public class Piece
    {
        //Internal Variables
        public readonly int id;

        //Properties
        public PieceType Type { get; set; }
        public Point Position { get; set; }
        public PieceColour Colour { get; set; }

        public Piece(PieceType type, PieceColour colour, Point pos)
        {
            Colour = colour;
            Type = type;
            Position = pos;   
        }    
    }
}
