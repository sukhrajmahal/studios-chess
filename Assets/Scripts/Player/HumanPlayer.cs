using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessEngine
{
    public class HumanPlayer : Player
    {
        private Piece selectedPiece;
        private List<PossibleMove> possibleMoves;
        private BoardHighlights highlightsManager;

        public HumanPlayer(int playerId, PieceColour colour, InternalBoard internalBoard) : 
            base(playerId, colour, internalBoard)
        {
            highlightsManager = BoardHighlights.Instance;
        }

        public Move HandleClick(Point clickPos)
        {
            //If there has been no piece selected
            if (selectedPiece == null)
            {
                selectedPiece = internalBoard.GetPiece(clickPos);
                //If the user hasn't selected anything valid, do nothing
                if (selectedPiece == null || selectedPiece.Colour != colour)
                {
                    return null;
                }
                //Getting the list of possible moves
                possibleMoves = internalBoard.GetPossibleMoves(selectedPiece);
                //Setting the highlights on the board
                highlightsManager.HighLightAllowedMoves(possibleMoves);
                return null;
            }
            else
            {
                Move thisMove = null;

                //If the person has clicked on a place is a valid move
                if (possibleMoves != null)
                {
                    //Checking by checking if click falls onto any possible move
                    foreach (var possibleMove in possibleMoves)
                    {
                        Point movePos = possibleMove.position;
                        if (clickPos.X == movePos.X && clickPos.Y == movePos.Y)
                        {
                            //Storing the old position of piece so that it can be stored in the move log
                            Point pieceOldPos = selectedPiece.Position;

                            thisMove = internalBoard.MovePiece(selectedPiece, clickPos);
                            thisMove.PlayerID = playerId;

                            //Checking that the piece isn't being promoted
                            checkPromotion(thisMove);
                        }
                    }
                }

                //Clearing all moves
                selectedPiece = null;
                possibleMoves = null;
                highlightsManager.HideHighlights();
                return thisMove;
            }
        }

        private void checkPromotion(Move move)
        {
            if (move.PieceMoved.Type == PieceType.Pawn)
            {
                var piece = move.PieceMoved;
                var pieceColour = piece.Colour;
                if (pieceColour == PieceColour.Black && piece.Position.Y == GlobalVars.gridSize - 1)
                {
                    showPromotionDialog(piece);
                }
                else if (pieceColour == PieceColour.White && piece.Position.Y == 0)
                {
                    showPromotionDialog(piece);
                }
                else if (pieceColour == PieceColour.Red && piece.Position.X == GlobalVars.gridSize - 1)
                {
                    showPromotionDialog(piece);
                }
                else if (pieceColour == PieceColour.Yellow && piece.Position.X == 0)
                {
                    showPromotionDialog(piece);
                }
            }
        }

        private void showPromotionDialog(Piece piece)
        {
            internalBoard.Promote(piece, PieceType.Queen);
        }
    }
}
