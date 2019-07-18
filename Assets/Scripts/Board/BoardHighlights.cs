using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessEngine;

public class BoardHighlights : MonoBehaviour
{
    public static BoardHighlights Instance { set; get; }

    public GameObject highlightPrefab;
    public Material killHighlight;
    public Material highlightMaterial;
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHighlightObject()
    {
        GameObject go = highlights.Find(g => !g.activeSelf);
        if(go == null)
        {
            go = Instantiate(highlightPrefab);
            highlights.Add(go);
        }
        return go;
    }

    public void HighLightAllowedMoves(List<PossibleMove> moves)
    {
        foreach (var move in moves)
        {
            GameObject highlight = GetHighlightObject();
            if (move.isKillMove)
            {
                highlight.GetComponent<MeshRenderer>().material = killHighlight;
            }
            else
            {
                highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
            }
            highlight.SetActive(true);
            //Again unity thinks 0,0 is at bottom left. So we inverse the y. (size of grid 
            //-1 because we start at 0. Then subtract y pos to inverse
            var yPos = (GlobalVars.gridSize - 1 - move.position.Y);
            highlight.transform.position = new Vector3(move.position.X + 0.5f, 0, yPos + 0.5f);
        }
    }

    public void HideHighlights()
    {
        foreach (var highlight in highlights)
        {
            highlight.SetActive(false);
        }
    }
}
