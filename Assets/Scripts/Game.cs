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
        private List<string> moveHistory;
        private int turnNumber = 0;
        private InternalBoard internalBoard;
          
        void Start()
        {
            //Initializing Lists
            players = new List<Player>();
            moveHistory = new List<string>();

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
                    ((HumanPlayer)currentPlayer).HandleClick(mouseClick);
                }
            }
        }

        private Point getMouseCoordinates()
        {
            //if the mouse isn't clicked on the board, this default will be returned
            Point mouseCoordinate = new Point(-1, -1);

            RaycastHit hit;
            //If the mouse clicked on the
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
            {
                mouseCoordinate.x =  (int)hit.point.x;
                //Inversing the y pos because unity thinks bottom left is (0,0)
                var yPos = (int)hit.point.z;
                mouseCoordinate.y = (GlobalVars.gridSize - 1) - yPos;
            }
            return mouseCoordinate;
        }
    }
}