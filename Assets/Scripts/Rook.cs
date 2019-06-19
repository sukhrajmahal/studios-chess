using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman
{
    public override int[,] PossibleMove()
    {
        int[,] r = new int[8, 8];
        //Checking if first turn
        if (BoardManager.Instance.globalTurns < 4)
        {
            return r;
        }
        Chessman c;
        int i;

        //Moving right
        i = CurrentX;
        while(true)
        {
            i++;
            if(i >= 8)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if(c == null)
            {
                r[i, CurrentY] = 1;
            }
            else
            {
                if(!c.team.Equals(team))
                {
                    r[i, CurrentY] = 2;
                }
                break;
            }
        }

        //Moving left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = 1;
            }
            else
            {
                if (!c.team.Equals(team))
                {
                    r[i, CurrentY] = 2;
                }
                break;
            }
        }

        //Moving up
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = 1;
            }
            else
            {
                if (!c.team.Equals(team))
                {
                    r[CurrentX, i] = 2;
                }
                break;
            }
        }

        //Moving down
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = 1;
            }
            else
            {
                if (!c.team.Equals(team))
                {
                    r[CurrentX, i] = 2;
                }
                break;
            }
        }
        return r;
    }
}
