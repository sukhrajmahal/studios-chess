using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private int[,] allowedMoves { set; get; }

    public Chessman[,] Chessmans { set; get; }
    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
	private const float TITLE_OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> chessmanPrefabs;
    public List<GameObject> activeChessman;

    string[] teamArray = new string[4] { "White", "Black", "Red", "Yellow" };
    public int numTurn = 0;
    public int numKings = 4;
    public int globalTurns = 0;

    private Material previousMat;
    public Material selectedMat;
    public Material whiteMat;
    public Material blackMat;
    public Material redMat;
    public Material yellowMat;

    private Camera mainCamera;
    int cameraRotationCount = 1;

    private void Start()
    {
        if (GlobalVars.gameType == GlobalVars.GameType.Standard)
        {
            SpawnNormalGame();
        }
        else if (GlobalVars.gameType == GlobalVars.GameType.FourPlayer)
        {
            SpawnFourPlayerGame();
        }
        else
        {
            SpawnNineSixtyGame();
        }
        Instance = this;
        mainCamera = Camera.main;
    }

	private void Update()
	{
		UpdateSelection ();
		DrawChessBoard ();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChessman == null)
                {
                    //Selecting the chessman
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    //Moving the chessman
                    MoveChessman(selectionX, selectionY);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(cameraRotationCount >=4)
            {
                cameraRotationCount = 0;
            }
            RotateCamera(cameraRotationCount++);
        }
  
        if(Input.GetKeyDown(KeyCode.B))
        {
            RotateCamera(numTurn);
        }
    }

    private void SelectChessman(int x, int y)
    {
        if(Chessmans [x,y] == null)
        {
            return;
        }
        if(!Chessmans[x,y].team.Equals(teamArray[numTurn]))
        {
            return;
        }
        bool hasAtleastOneMove = false;
        allowedMoves = Chessmans[x, y].PossibleMove();
        //Checking if selection has a move. If not, the item will not be selected
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves[i,j] == 1 || allowedMoves[i,j] == 2)
                {
                    hasAtleastOneMove = true;
                }
            }
        }

        if(hasAtleastOneMove == false)
        {
            return;
        }
        //Checking finished
        selectedChessman = Chessmans[x, y];
        RenderSelectedPiece();
        BoardHighlights.Instance.HighLightAllowedMoves(allowedMoves);
    }

    private void MoveChessman(int x, int y)
    {
        if (allowedMoves[x,y] != 0)
        {
            Chessman c = Chessmans[x, y];
            if (c != null && !c.team.Equals(teamArray[numTurn]))
            {
                //Capturing a piece
                //Code for ending the game if king
                if(c.GetType()==typeof(King))
                {
                    numKings--;
                    if(numKings == 1)
                    {
                        EndGame();
                        return;
                    }
                }
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCentre(x, y);
            selectedChessman.SetPostion(x, y);
            Chessmans[x, y] = selectedChessman;
            EndTurn();
        }
        UnrenderSelectedPiece();
        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;
        
    } 

	private void UpdateSelection()
	{
		if (!Camera.main)
		{
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("ChessPlane"))) 
		{
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		} 
		else 
		{
			selectionX = -1;
			selectionY = -1;
		}
	}

	private void DrawChessBoard()
	{
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++)
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j <= 8; j++) 
			{
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine);
			}
		}
			
		//Drawing the selection
		if (selectionX >= 0 && selectionY >= 0)
		{
			Debug.DrawLine(Vector3.forward * (selectionY +1) + Vector3.right * selectionX, Vector3.forward * selectionY  + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX, Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
        }
	}

    private void SpawnChessman(int index, int x, int y)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCentre(x,y), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPostion(x, y);
        activeChessman.Add(go);
    }

    private void SpawnChessman(int index, int x, int y, string color)
    {
        GameObject go;
        if (color.Equals("White"))
        {
            go = Instantiate(chessmanPrefabs[index], GetTileCentre(x, y), Quaternion.Euler(-90f, 0f, 90f)) as GameObject;
        }
        else if(color.Equals("Black"))
        {
            go = Instantiate(chessmanPrefabs[index], GetTileCentre(x, y), Quaternion.Euler(-90f, 0f, 180f)) as GameObject;
        }
        else if(color.Equals("Red"))
        {
            go = Instantiate(chessmanPrefabs[index], GetTileCentre(x, y), Quaternion.Euler(-90f, 0f, -90f)) as GameObject;
        }
        else if(color.Equals("Yellow"))
        {
            go = Instantiate(chessmanPrefabs[index], GetTileCentre(x, y), Quaternion.Euler(-90f, 0f, 90f)) as GameObject;
        }
        else if(color.Equals("Black King"))
        {
            go = Instantiate(chessmanPrefabs[index], GetTileCentre(x, y), Quaternion.Euler(-90f, 0f, 90f)) as GameObject;
        }
        else
        {
            go = Instantiate(chessmanPrefabs[index], GetTileCentre(x, y), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
        }
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPostion(x, y);
        activeChessman.Add(go);
    }

    private void SpawnNormalGame()
    {

    }

    private void SpawnFourPlayerGame()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        //Spawning the white team
        SpawnChessman(0, 2, 0); //King
        SpawnChessman(1, 3, 0); //Rook
        SpawnChessman(2, 0, 0); //Bishop
        SpawnChessman(3, 1, 0, "White"); //Knight
        //Pawns
        for(int i = 0; i <= 3; i++)
        {
            SpawnChessman(4,i, 1);
        }

        //Spawning the black team
        SpawnChessman(5, 0, 5, "Black King"); //King
        SpawnChessman(6, 0, 4); //Rook
        SpawnChessman(7, 0, 7); //Bishop
        SpawnChessman(8, 0, 6, "Black"); //Knight
        //Pawns
        for (int i = 0; i <= 3; i++)
        {
            SpawnChessman(9, 1, (7-i));
        }

        //Spawning the Red team
        SpawnChessman(10, 5, 7); //King
        SpawnChessman(11, 4, 7); //Rook
        SpawnChessman(12, 7, 7); //Bishop
        SpawnChessman(13, 6, 7, "Red"); //Knight
        //Pawns
        for (int i = 0; i <= 3; i++)
        {
            SpawnChessman(14, (4+i), 6);
        }

        //Spawning the Yellow team
        SpawnChessman(15, 7, 2, "Yellow"); //King
        SpawnChessman(16, 7, 3); //Rook
        SpawnChessman(17, 7, 0); //Bishop
        SpawnChessman(18, 7, 1); //Knight
        //Pawns
        for (int i = 0; i <= 3; i++)
        {
            SpawnChessman(19, 6, i);
        }
    }

    private void SpawnNineSixtyGame()
    {

    }

    private Vector3 GetTileCentre(int x, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TITLE_OFFSET;
        origin.z += (TILE_SIZE * z) + TITLE_OFFSET;
        return origin;
    }

    private void EndGame()
    {
        Debug.Log(teamArray[numTurn] + " has won");
        foreach (GameObject go in activeChessman)
        {
            if(go.GetType() == typeof(King))
            {
                Debug.Log(go.transform.name);
            }
            Destroy(go);
        }
        numTurn = 0;
        numKings = 4;
        BoardHighlights.Instance.HideHighlights();
        Start();
    }

    private void EndTurn()
    {
        //Updating the turn counts
        if (numTurn == 3)
        {
            numTurn = 0;
        }
        else
        {
            numTurn++;
        }
        globalTurns++;
    }

    private void RotateCamera(int angle)
    {
        Vector3 position = new Vector3(4f, 4.35f, -1.49f);
        Quaternion rotation = new Quaternion();
        if (angle == 0)
        {
            position = new Vector3(4f, 4.35f, -1.49f);
            rotation.eulerAngles = new Vector3(45f, 0f, 0f);
        }
        else if (angle == 1)
        {
            position = new Vector3(-1.49f, 4.35f, 4f);
            rotation.eulerAngles = new Vector3(45f, 90f, 0f);
        }
        else if (angle == 2)
        {
            position = new Vector3(4f, 4.35f, 9.57f);
            rotation.eulerAngles = new Vector3(45f, 180f, 0f);
        }
        else
        {
            position = new Vector3(9.57f, 4.35f, 4f);
            rotation.eulerAngles = new Vector3(45f, -90f, 0f);
        }
        mainCamera.transform.position = position;
        mainCamera.transform.rotation = rotation;
    }

    private void RenderSelectedPiece()
    {
        Component[] renderers = selectedChessman.GetComponentsInChildren(typeof(Renderer));
        previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
        selectedMat.mainTexture = previousMat.mainTexture;
        foreach (Renderer childRenderer in renderers)
        {
            childRenderer.material = selectedMat;
        }
    }

    //Cleaner way of doing this?
    private void UnrenderSelectedPiece()
    {
        Material temp = whiteMat;
        if (selectedChessman.team.Equals("Black"))
        {
            temp = blackMat;
        }
        else if (selectedChessman.team.Equals("Red"))
        {
            temp = redMat;
        }
        else if (selectedChessman.team.Equals("Yellow"))
        {
            temp = yellowMat;
        }
        Component[] renderers = selectedChessman.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer childRenderer in renderers)
        {
            childRenderer.material = temp;
        }
    }
}
