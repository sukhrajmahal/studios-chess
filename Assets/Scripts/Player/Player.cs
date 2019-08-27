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
                    setUpFourPlayerGame();
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
            for (var i = 0; i < GlobalVars.GRID_SIZE; i++)
            {
                internalBoard.AddPiece(new Piece(PieceType.Pawn, colour, new Point(i, pawnStartY)));
            }
        }

        private void setUpFourPlayerGame()
        {
            switch (colour)
            {
                case (PieceColour.White):
                    internalBoard.AddPiece(new Piece(PieceType.Bishop, colour, new Point(0, 7)));
                    internalBoard.AddPiece(new Piece(PieceType.Knight, colour, new Point(1, 7)));
                    internalBoard.AddPiece(new Piece(PieceType.King, colour, new Point(2, 7)));
                    internalBoard.AddPiece(new Piece(PieceType.Rook, colour, new Point(3, 7)));
                    for (var i = 0; i < 4; i++)
                    {
                        internalBoard.AddPiece(new Piece(PieceType.Pawn, colour, new Point(i, 6)));
                    }
                    break;
                case (PieceColour.Black):
                    internalBoard.AddPiece(new Piece(PieceType.Bishop, colour, new Point(7, 0)));
                    internalBoard.AddPiece(new Piece(PieceType.Knight, colour, new Point(6, 0)));
                    internalBoard.AddPiece(new Piece(PieceType.King, colour, new Point(5, 0)));
                    internalBoard.AddPiece(new Piece(PieceType.Rook, colour, new Point(4, 0)));
                    for (var i = 4; i < GlobalVars.GRID_SIZE; i++)
                    {
                        internalBoard.AddPiece(new Piece(PieceType.Pawn, colour, new Point(i, 1)));
                    }
                    break;                  
                case (PieceColour.Red):
                    internalBoard.AddPiece(new Piece(PieceType.Bishop, colour, new Point(0, 0)));
                    internalBoard.AddPiece(new Piece(PieceType.Knight, colour, new Point(0, 1)));
                    internalBoard.AddPiece(new Piece(PieceType.King, colour, new Point(0, 2)));
                    internalBoard.AddPiece(new Piece(PieceType.Rook, colour, new Point(0, 3)));
                    for (var i = 0; i < 4; i++)
                    {
                        internalBoard.AddPiece(new Piece(PieceType.Pawn, colour, new Point(1, i)));
                    }
                    break;
                case (PieceColour.Yellow):
                    internalBoard.AddPiece(new Piece(PieceType.Bishop, colour, new Point(7, 7)));
                    internalBoard.AddPiece(new Piece(PieceType.Knight, colour, new Point(7, 6)));
                    internalBoard.AddPiece(new Piece(PieceType.King, colour, new Point(7, 5)));
                    internalBoard.AddPiece(new Piece(PieceType.Rook, colour, new Point(7, 4)));
                    for (var i = 4; i < GlobalVars.GRID_SIZE; i++)
                    {
                        internalBoard.AddPiece(new Piece(PieceType.Pawn, colour, new Point(6, i)));
                    }
                    break;
            }
        }
    }
}
