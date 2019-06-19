using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;

public class Buttons : MonoBehaviour
{
    // Setting up functions
    public static Buttons Instance { set; get; }
    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //Main Menu Functions

    public void NewGame(string sceneName)
    {
        Application.LoadLevel("ChessBoard");
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Chess game scene buttons

    public void Back()
    {
        Application.LoadLevel("OpeningScene");
    }

    public void Save()
    {
        
    }
}
