using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public class Player
    {
        //Information About the player
        protected int playerId;
        protected readonly PieceColour colour;
        protected InternalBoard internalBoard;

        public Player(int playerId, PieceColour colour, InternalBoard internalBoard)
        {
            this.playerId = playerId;
            this.colour = colour;
            this.internalBoard = internalBoard;

            //Setting up the pieces depending on the game and colour
            switch (GlobalVars.gameType)
            {
                case (GameType.Standard):
                    setUpStandardGame();
                    break;
                case (GameType.FourPlayer):
                    break;
                case (GameType.NineSixty):
                    break;
                case (GameType.FourPointFive):
                    break;
            }
        }

        private void setUpStandardGame()
        {
            //Default X positions that would be for black
            var backStartY = 0;
            var pawnStartY = 1;

            //Changing the starting if the player is white
            if (colour == PieceColour.White)
            {
                backStartY = 7;
                pawnStartY = 6;
            }

            //Placing the back row on the board
            internalBoard.AddPiece(new Piece(PieceType.Rook, colour, new Point(0, backStartY)));
            internalBoard.AddPiece(new Piece(PieceType.Knight, colour, new Point(1, backStartY)));
            internalBoard.AddPiece(new Piece(PieceType.Bishop, colour, new Point(2, backStartY)));
            internalBoard.AddPiece(new Piece(PieceType.Queen, colour, new Point(3, backStartY)));
            internalBoard.AddPiece(new Piece(PieceType.King, colour, new Point(4, backStartY)));
            internalBoard.AddPiece(new Piece(PieceType.Bishop, colour, new Point(5, backStartY)));
            internalBoard.AddPiece(new Piece(PieceType.Knight, colour, new Point(6, backStartY)));
            internalBoard.AddPiece(new Piece(PieceType.Rook, colour, new Point(7, backStartY)));

            //Adding pawns 
            for (var i = 0; i < GlobalVars.gridSize; i++)
            {
                internalBoard.AddPiece(new Piece(PieceType.Pawn, colour, new Point(i, pawnStartY)));
            }
        }
    }
}
