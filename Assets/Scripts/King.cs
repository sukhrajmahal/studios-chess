using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    public override int[,] PossibleMove()
    {
        int[,] r = new int[8, 8];
        //Checking if first turn
        if (BoardManager.Instance.globalTurns < 4)
        {
            return r;
        }
        KingMove(CurrentX - 1, CurrentY + 1, ref r);
        KingMove(CurrentX, CurrentY + 1, ref r);
        KingMove(CurrentX + 1, CurrentY + 1, ref r);
        KingMove(CurrentX + 1, CurrentY, ref r);
        KingMove(CurrentX + 1, CurrentY - 1, ref r);
        KingMove(CurrentX, CurrentY - 1, ref r);
        KingMove(CurrentX - 1, CurrentY - 1, ref r);
        KingMove(CurrentX - 1, CurrentY, ref r);
        return r;
    }


    //Suggested Imporvement. Add a this method in the super class.
    private void KingMove(int x, int y, ref int[,] r)
    {

        Chessman c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = BoardManager.Instance.Chessmans[x, y];
            if (c == null)
            {
                r[x, y] = 1;
            }
            else if (!c.team.Equals(team))
            {
                r[x, y] = 2;
            }
        }
    }
}
