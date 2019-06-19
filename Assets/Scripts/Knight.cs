using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman
{
    public override int[,] PossibleMove()
    {
        int[,] r = new int[8, 8];
        //Checking if first turn
        if (BoardManager.Instance.globalTurns < 4)
        {
            return r;
        }
        //Up Left
        KnightMove(CurrentX - 1, CurrentY + 2, ref r);
        //Up Right
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);
        //Left up
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);
        //Right up
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);
        //Down Left
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);
        //Down Right
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);
        //Left Down
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);
        //Right Down
        KnightMove(CurrentX + 2, CurrentY - 1, ref r);
        return r;
    }

    private void KnightMove(int x, int y, ref int[,] r)
    {
        Chessman c;
        if(x >= 0 && x < 8 && y>= 0 && y < 8 )
        {
            c = BoardManager.Instance.Chessmans[x, y];
            if (c == null)
            {
                r[x, y] = 1;
            }
            else if(!c.team.Equals(team))
            {
                r[x, y] = 2;
            }
        }
    }
}
