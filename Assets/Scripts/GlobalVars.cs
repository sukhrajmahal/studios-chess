using System.Collections;
using System.Collections.Generic;

namespace ChessEngine
{
    public struct PlayerInformation
    {
        public int id;
        public PieceColour colour;
        public bool isHuman;

        public PlayerInformation(int id, PieceColour colour, bool isHuman)
        {
            this.id = id;
            this.colour = colour;
            this.isHuman = isHuman;
        }
    }

    public static class GlobalVars
    {
        public static GameType gameType = GameType.Standard;
        public const int gridSize = 8;
        //Giving the number the players (length) and if they are human or not.
        public static List<PlayerInformation> playerDetails = new List<PlayerInformation>()
        {
            new PlayerInformation(0, PieceColour.White, true),
            new PlayerInformation(1, PieceColour.Black, true)
        };
    }
}