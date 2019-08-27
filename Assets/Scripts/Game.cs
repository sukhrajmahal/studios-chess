using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessEngine
{
    public enum GameType { Standard, FourPlayer, FourPointFive, NineSixty }

    public class Game : MonoBehaviour
    {
        private List<Player> players;
        private Player currentPlayer;
        private List<Move> moveHistory;
        private int turnNumber = 0;
        private InternalBoard internalBoard;
          
        void Start()
        {
            //Initializing Lists
            players = new List<Player>();
            moveHistory = new List<Move>();

            //Initializing Boards
            internalBoard = new InternalBoard();

            //Setting up the players
            foreach (var playerDetail in GlobalVars.playerDetails)
            {
                if (playerDetail.isHuman)
                {
                    players.Add(new HumanPlayer(playerDetail.id, playerDetail.colour, internalBoard));
                }
                else
                {
                    players.Add(new ComputerPlayer(playerDetail.id, playerDetail.colour, internalBoard));
                }
            }

            //Setting up the current Player
            currentPlayer = players[0];
        }

        void Update()
        {
            //Checking if the mouse has been clicked
            if (Input.GetMouseButtonDown(0))
            {
                Point mouseClick = getMouseCoordinates();

                //Checking the current player is human
                if (currentPlayer.GetType() == typeof(HumanPlayer))
                {
                    var thisMove = ((HumanPlayer)currentPlayer).HandleClick(mouseClick);
                    //Checking move is not null
                    if (thisMove != null)
                    {
                        thisMove.TurnNumber = turnNumber;
                        moveHistory.Add(thisMove);
                        endTurn();
                    }
                }
            }

            //Checking if is the computers turn
            if (currentPlayer.GetType() == typeof(ComputerPlayer))
            {
                var thisMove = ((ComputerPlayer)currentPlayer).MakeMove();
                if(thisMove == null)
                {
                    throw new System.Exception("Computer Failed to make move");
                }
                thisMove.TurnNumber = turnNumber;
                endTurn();
            }
        }

        private Point getMouseCoordinates()
        {
            //if the mouse isn't clicked on the board, this default will be returned
            var mouseCoordinate = new Point(-1, -1);

            RaycastHit hit;
            //If the mouse clicked on the
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
            {
                mouseCoordinate.X =  (int)hit.point.x;
                //Inversing the y position because unity thinks bottom left is (0,0)
                var yPos = (int)hit.point.z;
                mouseCoordinate.Y = (GlobalVars.GRID_SIZE - 1) - yPos;
            }
            return mouseCoordinate;
        }

        private void endTurn()
        {
            turnNumber++;
            var currentPlayerIndex = turnNumber % players.Count;
            currentPlayer = players[currentPlayerIndex];
        }
    }
}