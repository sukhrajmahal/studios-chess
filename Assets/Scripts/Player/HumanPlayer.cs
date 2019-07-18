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

        public void HandleClick(Point clickPos)
        {
            //If there has been no piece selected
            if (selectedPiece == null)
            {
                selectedPiece = internalBoard.GetPiece(clickPos);
                //If the user hasn't selected anything valid, do nothing
                if (selectedPiece == null)
                {
                    return;
                }
                //Getting the list of possible moves
                possibleMoves = internalBoard.GetPossibleMoves(selectedPiece);
                //Setting the highlights on the board
                highlightsManager.HighLightAllowedMoves(possibleMoves);
            }
            else
            {
                //If the person has clicked on a place is a valid move
                if (possibleMoves != null)
                {
                    //Checking by checking if click falls onto any possible move
                    foreach (var possibleMove in possibleMoves)
                    {
                        Point movePos = possibleMove.position;
                        if (clickPos.X == movePos.X && clickPos.Y == movePos.Y)
                        {
                            internalBoard.MovePiece(selectedPiece, clickPos);
                            break;
                        }
                    }
                }

                //Clearing all moves
                selectedPiece = null;
                possibleMoves = null;
                highlightsManager.HideHighlights();
            }
        }
    }
}
