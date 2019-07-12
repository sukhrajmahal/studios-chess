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
        private ExternalBoard externalBoard;
          
        void Start()
        {
            //Initializing Lists
            players = new List<Player>();
            moveHistory = new List<string>();

            //Initializing Boards
            internalBoard = new InternalBoard();
            externalBoard = new ExternalBoard();

            //Setting up the players
            foreach (var playerInfo in GlobalVars.playerDetails)
            {
                if (playerInfo.isHuman)
                {
                    players.Add(new HumanPlayer(playerInfo.id, playerInfo.colour, internalBoard));
                }
                else
                {
                    players.Add(new ComputerPlayer(playerInfo.id, playerInfo.colour, internalBoard));
                }
            }
        }
    }
}