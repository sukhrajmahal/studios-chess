using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chessman : MonoBehaviour
{
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }
    public string team;

    public void SetPostion(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual int[,] PossibleMove()
    {
        return new int[8,8];
    }
}
