using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{
    public override int[,] PossibleMove()
    {
        int[,] r = new int[8, 8];
        int tempX;
        int tempY;
        Chessman c;

        //Checking if first turn
        if (BoardManager.Instance.globalTurns < 4)
        {
            return r;
        }

        //Moving Left Up
        tempX = CurrentX;
        tempY = CurrentY;
        while (true)
        {
            tempX--;
            tempY++;
            if(tempX < 0 || tempY >= 8)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[tempX, tempY];
            if (c != null)
            {
                if (!c.team.Equals(team))
                {
                    r[tempX, tempY] = 2;
                }
                break;
            }
            r[tempX, tempY] = 1;
        }
        //Moving Right up
        tempX = CurrentX;
        tempY = CurrentY;
        while (true)
        {
            tempX++;
            tempY++;
            if (tempX >= 8 || tempY >= 8)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[tempX, tempY];
            if(c != null)
            {
                if(!c.team.Equals(team))
                {
                    r[tempX, tempY] = 2;
                }
                break;
            }
            r[tempX, tempY] = 1;
        }
        //Moving Left Down
        tempX = CurrentX;
        tempY = CurrentY;
        while (true)
        {
            tempX--;
            tempY--;
            if (tempX < 0 || tempY < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[tempX, tempY];
            if (c != null)
            {
                if (!c.team.Equals(team))
                {
                    r[tempX, tempY] = 2;
                }
                break;
            }
            r[tempX, tempY] = 1;
        }
        //Right Down
        tempX = CurrentX;
        tempY = CurrentY;
        while (true)
        {
            tempX++;
            tempY--;
            if(tempX >= 8 || tempY < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[tempX, tempY];
            if (c != null)
            {
                if (!c.team.Equals(team))
                {
                    r[tempX, tempY] = 2;
                }
                break;
            }
            r[tempX, tempY] = 1;
        }

        return r;
    }
}
