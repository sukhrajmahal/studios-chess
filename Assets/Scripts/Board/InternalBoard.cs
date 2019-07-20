using System;
using System.Collections;
using System.Collections.Generic;
//Debugging
using UnityEngine;

namespace ChessEngine
{
    //This class is currently implemented in a simple and naive method.
    //Expect major overhauls when AI is developed as current methods are
    //too expensive. 
    public class InternalBoard : IBoard
    {
        private Piece[,] board;
        private ExternalBoard externalBoard;
        private Dictionary<PieceColour, List<Piece>> playerPieceLists;

        public InternalBoard()
        {
            board = new Piece[GlobalVars.gridSize, GlobalVars.gridSize];
            externalBoard = ExternalBoard.Instance;

            //Setting up lists
            playerPieceLists = new Dictionary<PieceColour, List<Piece>>();
            playerPieceLists.Add(PieceColour.White, new List<Piece>());
            playerPieceLists.Add(PieceColour.Black, new List<Piece>());

            if (GlobalVars.gameType == GameType.FourPlayer)
            {
                playerPieceLists.Add(PieceColour.Red, new List<Piece>());
                playerPieceLists.Add(PieceColour.Yellow, new List<Piece>());
            }
        }

        public void AddPiece(Piece piece)
        {
            board[piece.Position.X, piece.Position.Y] = piece;
            playerPieceLists[piece.Colour].Add(piece);
            externalBoard.AddPiece(piece);
        }

        public Move MovePiece(Piece piece, Point newPos)
        {
            //Creating Move object to store info about the move
            var thisMove = new Move();

            //Checking if the move is a captures
            if (board[newPos.X, newPos.Y] != null)
            {
                thisMove.PieceTaken = board[newPos.X, newPos.Y];
            }
            //Collecting information about the move
            thisMove.PieceMoved = piece;
            thisMove.OldPosition = piece.Position;
            thisMove.NewPosition = newPos;

            //Moving piece internally
            board[piece.Position.X, piece.Position.Y] = null;
            board[newPos.X, newPos.Y] = piece;

            //Moving the piece in the external UI
            externalBoard.MovePiece(piece, newPos);
            //Updating Pieces knowledge of the position
            piece.Position = newPos;

            return thisMove;
        }

        public void Promote(Piece piece, PieceType type)
        {
            //The most complex code that you will ever see
            piece.Type = type;
            externalBoard.Promote(piece, type);
        }

        public List<PossibleMove> GetPossibleMoves(Piece piece)
        {
            switch (piece.Type)
            {
                case (PieceType.Pawn):
                    return getPawnMoves(piece);
                case (PieceType.Rook):
                    return getRookMoves(piece);
                case (PieceType.Knight):
                    return getKnightMoves(piece);
                case (PieceType.Bishop):
                    return getBishopMoves(piece);
                case (PieceType.Queen):
                    return getQueenMoves(piece);
                case (PieceType.King):
                    return getKingMoves(piece);
            }
            return null;
        }

        public Piece GetPiece(Point pos)
        {
            if (pos.IsOnBoard())
            {
                return board[pos.X, pos.Y];
            }
            return null;
        }

        public List<Piece> GetAllPieces(PieceColour team)
        {
            return playerPieceLists[team];
        }

        private List<PossibleMove> getPawnMoves(Piece piece)
        {
            var moves = new List<PossibleMove>();

            //Finding the direction in which the pawn will move
            //Default for Black (going south)
            var pawnYForwardDir = 1;

            //if piece is White, change this to -1
            if (piece.Colour == PieceColour.White)
            {
                pawnYForwardDir = -1;
            }

            //Checking the normal forward move
            addMoveIfValid(moves, piece.Position.X, piece.Position.Y + pawnYForwardDir, piece.Colour, false, true);
            //Checking the kill moves to either side
            addMoveIfValid(moves, piece.Position.X + 1, piece.Position.Y + pawnYForwardDir, piece.Colour, true, false);
            addMoveIfValid(moves, piece.Position.X - 1, piece.Position.Y + pawnYForwardDir, piece.Colour, true, false);

            //Checking if the it is the pawns first move, if so allow pawn to move 2 spaces.
            //'1' is the first pawn row from the top. Grid size - 2 is grid size -1 (because we count from 
            //zero) -1 because its the front row (pawns on front row). 
            if (piece.Position.Y == 1 || piece.Position.Y == GlobalVars.gridSize - 2)
            {
                //Checking that the game type isn't a four player game.
                //Four player does not allow pawn to move 2 spaces
                if (GlobalVars.gameType != GameType.FourPlayer)
                {
                    var potentialYPos = piece.Position.Y + (pawnYForwardDir * 2);
                    addMoveIfValid(moves, piece.Position.X, potentialYPos, piece.Colour);
                }
            }

            return moves;
        }

        private List<PossibleMove> getRookMoves(Piece piece)
        {
            var moves = new List<PossibleMove>();

            //Getting the position of the piece
            var pos = piece.Position;

            //Checking if the piece can move north 
            //Getting the y position and moving it up till it hits zero
            //Subtracting one as we don't want check the tile it is on
            for (var i = pos.Y - 1; i >= 0; i--)
            {
                if (!addMoveIfValid(moves, pos.X, i, piece.Colour))
                {
                    break;
                }
            }
            //Checking if the piece can move east
            for (var i = pos.X + 1; i < GlobalVars.gridSize; i++)
            {
                if (!addMoveIfValid(moves, i, pos.Y, piece.Colour))
                {
                    break;
                }
            }
            //Checking if the piece can move south
            for (var i = pos.Y + 1; i < GlobalVars.gridSize; i++)
            {
                if (!addMoveIfValid(moves, pos.X, i, piece.Colour))
                {
                    break;
                }
            }
            //Checking if the piece can move west
            for (var i = pos.X - 1; i >= 0; i--)
            {
                if (!addMoveIfValid(moves, i, pos.Y, piece.Colour))
                {
                    break;
                }
            }
            return moves;
        }

        private List<PossibleMove> getKnightMoves(Piece piece)
        {
            var moves = new List<PossibleMove>();

            //Getting north north east move
            addMoveIfValid(moves, piece.Position.X + 1, piece.Position.Y - 2, piece.Colour);
            //Getting north east east move
            addMoveIfValid(moves, piece.Position.X + 2, piece.Position.Y - 1, piece.Colour);
            //Getting south east east move
            addMoveIfValid(moves, piece.Position.X + 2, piece.Position.Y + 1, piece.Colour);
            //Getting south south east move
            addMoveIfValid(moves, piece.Position.X + 1, piece.Position.Y + 2, piece.Colour);

            //Getting north north west move
            addMoveIfValid(moves, piece.Position.X - 1, piece.Position.Y - 2, piece.Colour);
            //Getting north west west move
            addMoveIfValid(moves, piece.Position.X - 2, piece.Position.Y - 1, piece.Colour);
            //Getting south west west move
            addMoveIfValid(moves, piece.Position.X - 2, piece.Position.Y + 1, piece.Colour);
            //Getting south south west move
            addMoveIfValid(moves, piece.Position.X - 1, piece.Position.Y + 2, piece.Colour);

            return moves;
        }

        private List<PossibleMove> getBishopMoves(Piece piece)
        {
            var moves = new List<PossibleMove>();
            //Getting north east moves
            int possibleX = piece.Position.X + 1;
            int possibleY = piece.Position.Y - 1;
            while (possibleX < GlobalVars.gridSize && possibleY >= 0)
            {
                if (!addMoveIfValid(moves, possibleX, possibleY, piece.Colour))
                {
                    break;
                }
                possibleX++;
                possibleY--;
            }
            //Getting south east positions
            possibleX = piece.Position.X + 1;
            possibleY = piece.Position.Y + 1;
            while (possibleX < GlobalVars.gridSize && possibleY < GlobalVars.gridSize)
            {
                if (!addMoveIfValid(moves, possibleX, possibleY, piece.Colour))
                {
                    break;
                }
                possibleX++;
                possibleY++;
            }
            //Getting north west moves
            possibleX = piece.Position.X - 1;
            possibleY = piece.Position.Y - 1;
            while (possibleX >= 0 && possibleY >= 0)
            {
                if (!addMoveIfValid(moves, possibleX, possibleY, piece.Colour))
                {
                    break;
                }
                possibleX--;
                possibleY--;
            }
            //Getting south west moves
            possibleX = piece.Position.X - 1;
            possibleY = piece.Position.Y + 1;
            while (possibleX >= 0 && possibleY < GlobalVars.gridSize)
            {
                if (!addMoveIfValid(moves, possibleX, possibleY, piece.Colour))
                {
                    break;
                }
                possibleX--;
                possibleY++;
            }

            return moves;
        }

        private List<PossibleMove> getKingMoves(Piece piece)
        {
            var moves = new List<PossibleMove>();
            //Checking if piece can move north
            addMoveIfValid(moves, piece.Position.X, piece.Position.Y - 1, piece.Colour);
            //Checking north east
            addMoveIfValid(moves, piece.Position.X + 1, piece.Position.Y - 1, piece.Colour);
            //Checking east
            addMoveIfValid(moves, piece.Position.X + 1, piece.Position.Y, piece.Colour);
            //Checking south east
            addMoveIfValid(moves, piece.Position.X + 1, piece.Position.Y + 1, piece.Colour);
            //Checking south
            addMoveIfValid(moves, piece.Position.X, piece.Position.Y + 1, piece.Colour);
            //Checking south west
            addMoveIfValid(moves, piece.Position.X - 1, piece.Position.Y + 1, piece.Colour);
            //Checking west
            addMoveIfValid(moves, piece.Position.X - 1, piece.Position.Y, piece.Colour);
            //Checking north west
            addMoveIfValid(moves, piece.Position.X - 1, piece.Position.Y - 1, piece.Colour);

            return moves;
        }

        private List<PossibleMove> getQueenMoves(Piece piece)
        {
            List<PossibleMove> bishipMoves = getBishopMoves(piece);
            List<PossibleMove> rookMoves = getRookMoves(piece);
            //Joining the moves from rooks and bishops together.
            bishipMoves.AddRange(rookMoves);
            return bishipMoves;
        }

        //A wrapper around the full addMoveIfValid, allowing non pawn pieces to 
        //Call a simpler method
        private bool addMoveIfValid  (List<PossibleMove> moves, int x, int y, PieceColour colour)
        {
            return addMoveIfValid(moves, x, y, colour, false, false);
        }

        /// <summary>
        /// Checks if a given x and y position result in a valid move for a piece and if it is
        /// a standard or kill move. It will add it to the list of moves accordingly. 
        /// </summary>
        /// <param name="moves"> List of moves to add potential move to </param>
        /// <param name="x"> X coordinate of the point to be evaluated </param>
        /// <param name="y"> Y coordinate of the point to be evaluated </param>
        /// <param name="colour"> The team that the piece is on </param>
        /// <param name="killOnly"> Whether or not only kill moves should be evaluated </param>
        /// <returns> Boolean, returns false if the are no further possible moves in 
        /// this direction as it has been blocked. Returns true if search can
        /// continue</returns>
        private bool addMoveIfValid(List<PossibleMove> moves, int x, int y, PieceColour colour, bool killOnly, bool nonKillOnly)
        {
            //Checking that the x and y points are within the limitations of the board
            //This is useful for knights, kings and other pieces that don't have unlimited 
            //moving like rooks. Checking here will reduce the overall number of if statements
            if (x < 0 || x >= GlobalVars.gridSize || y < 0 || y >= GlobalVars.gridSize)
            {
                return false;
            }

            Piece pieceAtOption = board[x, y];
            if (pieceAtOption == null && !killOnly)
            {
                moves.Add(new PossibleMove(new Point(x, y)));
                return true;
            }
            else if (pieceAtOption != null && colour != pieceAtOption.Colour && !nonKillOnly)
            {
                //Add a possible kill move
                moves.Add(new PossibleMove(new Point(x, y), pieceAtOption));
            }
            //We have hit a piece, therefore no more moves will available in this 
            //direction. Signal that no more moves should be made. 
            return false;
        }
    }
}
