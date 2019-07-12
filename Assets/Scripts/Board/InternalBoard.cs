using System;
using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public class InternalBoard : IBoard
    {
        private Piece[,] board;
        private ExternalBoard externalBoard;

        public InternalBoard()
        {
            board = new Piece[GlobalVars.gridSize, GlobalVars.gridSize];
            externalBoard = ExternalBoard.Instance;
        }

        public void AddPiece(Piece piece)
        {
            board[piece.Position.x, piece.Position.y] = piece;
            externalBoard.AddPiece(piece);
        }

        public void MovePiece(Piece piece, Point newPos)
        {
            //Checking if the move is a captures
            if (board[newPos.x, newPos.y] != null)
            {
                //Do something here
            }
            board[piece.Position.x, piece.Position.y] = null;
            board[newPos.x, newPos.y] = piece;
            piece.Position = newPos;
        }

        public void Promote(Piece piece, PieceType type)
        {
            //The most complex code that you will ever see
            piece.Type = type;
        }

        public List<PossibleMove> GetPossibleMoves(Piece piece)
        {
            List<PossibleMove> possibleMoves = new List<PossibleMove>();
            return possibleMoves;            
        }
    }
}
