using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(int playerId, PieceColour colour, InternalBoard internalBoard) :
            base(playerId, colour, internalBoard)
        { }

        public Move MakeMove()
        {
            //Getting all pieces that are owned by this player
            var pieces = internalBoard.GetAllPieces(colour);
            //Getting a random piece
            System.Random random = new System.Random();
            if (pieces != null && pieces.Count != 0)
            {
                var randomPieceIndex = random.Next(pieces.Count);
                var pieceToMove = pieces[randomPieceIndex];
                //Getting all the possible moves for that piece
                var possibleMoves = internalBoard.GetPossibleMoves(pieceToMove);
                //Getting a random move
                if (possibleMoves.Count != 0)
                {
                    var randomMoveIndex = random.Next(possibleMoves.Count);
                    var thisMove = internalBoard.MovePiece(pieceToMove, possibleMoves[randomMoveIndex].position);
                    thisMove.PlayerID = playerId;
                    return thisMove;
                }
                return null;
            }
            return null;
        }
    }
}