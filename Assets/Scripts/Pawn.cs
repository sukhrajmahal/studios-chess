using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman
{
    public override int[,] PossibleMove()
    {
        Chessman c;
        int[,] r = new int[8, 8];
        //for white team
        if (team.Equals("White"))
        {
            //Left
            if(CurrentX != 0 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if(c != null && !c.team.Equals("White"))
                {
                    r[CurrentX - 1, CurrentY + 1] = 2;
                }
            }
            //Right
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.team.Equals("White"))
                {
                    r[CurrentX + 1, CurrentY + 1] = 2;
                }
            }
            //Middle
            if(CurrentY!= 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                if(c == null)
                {
                    r[CurrentX, CurrentY + 1] = 1;
                }
            }
        }
        else if(team.Equals("Black"))
        {
            //Left
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX +1, CurrentY + 1];
                if (c != null && !c.team.Equals("Black"))
                {
                    r[CurrentX + 1, CurrentY + 1] = 2;
                }
            }
            //Right
            if (CurrentX != 0 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && !c.team.Equals("Black"))
                {
                    r[CurrentX + 1, CurrentY - 1] = 2;
                }
            }
            //Middle
            if (CurrentX != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
                if (c == null)
                {
                    r[CurrentX + 1, CurrentY] = 1;
                }
            }
        }
        else if (team.Equals("Red"))
        {
            //Left
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && !c.team.Equals("Red"))
                {
                    r[CurrentX + 1, CurrentY - 1] = 2;
                }
            }
            //Right
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX -1 , CurrentY - 1];
                if (c != null && !c.team.Equals("Red"))
                {
                    r[CurrentX - 1, CurrentY - 1] = 2;
                }
            }
            //Middle
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = 1;
                }
            }
        }
        else
        {
            //Left
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && !c.team.Equals("Yellow"))
                {
                    r[CurrentX - 1, CurrentY - 1] = 2;
                }
            }
            //Right
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.team.Equals("Yellow"))
                {
                    r[CurrentX - 1, CurrentY + 1] = 2;
                }
            }
            //Middle
            if (CurrentX != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX -1, CurrentY];
                if (c == null)
                {
                    r[CurrentX -1, CurrentY] = 1;
                }
            }
        }
        return r;
    }
}
