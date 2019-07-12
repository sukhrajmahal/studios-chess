using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessEngine
{
    interface IBoard
    {
        void AddPiece(Piece piece);
        void MovePiece(Piece piece, Point newPos);
        void Promote(Piece piece, PieceType type);
    }
}
