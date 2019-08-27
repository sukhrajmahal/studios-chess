using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessEngine
{
    public class ExternalBoard : MonoBehaviour
    {
        //Allowing access of External Board
        public static ExternalBoard Instance { get; set; }

        //Getting the prefabs and materials from unity
        public Material pieceSelectMat;
        public Material whitePieceMat;
        public Material blackPieceMat;
        public Material redPieceMat;
        public Material yellowPieceMat;
        public GameObject kingPrefab, queenPrefab;
        public GameObject bishopPrefab, knightPrefab;
        public GameObject rookPrefab, pawnPrefab;

        //Board mapping
        private GameObject[,] board;

        //Dictionaries to map Piece type and colours to materials and Game Objects.
        private Dictionary<PieceColour, Material> colourDict;
        private Dictionary<PieceType, GameObject> typeDict;

        //Board Constants
        private const float TILE_SIZE = 1.0f;
        private const float TITLE_OFFSET = 0.5f;

        void Awake()
        {
            Instance = this;

            //Initializing Dictionaries and Array
            colourDict = new Dictionary<PieceColour, Material>();
            typeDict = new Dictionary<PieceType, GameObject>();
            board = new GameObject[GlobalVars.GRID_SIZE, GlobalVars.GRID_SIZE];

            //Setting up dictionaries
            colourDict.Add(PieceColour.Black, blackPieceMat);
            colourDict.Add(PieceColour.White, whitePieceMat);
            colourDict.Add(PieceColour.Red, redPieceMat);
            colourDict.Add(PieceColour.Yellow, yellowPieceMat);

            typeDict.Add(PieceType.King, kingPrefab);
            typeDict.Add(PieceType.Queen, queenPrefab);
            typeDict.Add(PieceType.Bishop, bishopPrefab);
            typeDict.Add(PieceType.Knight, knightPrefab);
            typeDict.Add(PieceType.Rook, rookPrefab);
            typeDict.Add(PieceType.Pawn, pawnPrefab);
        }

        public void AddPiece(Piece piece)
        {
            //Creating a new Game Object
            Point position = piece.Position;
            GameObject go = Instantiate(typeDict[piece.Type], GetTileCentre(position), 
                Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
            go.transform.SetParent(transform);
            //Setting the colour of the piece
            RenderPiece(go, colourDict[piece.Colour]);
            //Adding the Game Object to board
            board[position.X, position.Y] = go;
        }

        public Move MovePiece(Piece piece, Point newPos)
        {
            Point oldPos = piece.Position;
            //Checking there is game object here
            GameObject selectedPiece = board[oldPos.X, oldPos.Y];
            if (selectedPiece != null)
            {
                //Checking if the move is a captures
                if (board[newPos.X, newPos.Y] != null)
                {
                    //Remove the captured piece
                    Destroy(board[newPos.X, newPos.Y]);
                }
                //Moving the 3d object
                selectedPiece.transform.position = GetTileCentre(newPos);
                //Moving the object in the array
                board[newPos.X, newPos.Y] = selectedPiece;
                board[oldPos.X, oldPos.Y] = null;
            }
            return null;
        }

        public void Promote(Piece piece)
        {
            //Destroying the Game Object
            Destroy(board[piece.Position.X, piece.Position.Y]);
            //Creating a new piece.
            AddPiece(piece);
        }

        private Vector3 GetTileCentre(Point pos)
        {
            //Creating a zero vector
            Vector3 origin = Vector3.zero;
            //The board in memory thinks top left is (0,0) while
            //unity thinks bottom left is (0,0). Therefore the unity 
            //way needs y to be inverted before we get the new position
            var yPos = (GlobalVars.GRID_SIZE - 1) - pos.Y;

            origin.x += (TILE_SIZE * pos.X) + TITLE_OFFSET;
            origin.z += (TILE_SIZE * yPos) + TITLE_OFFSET;
            return origin;
        }

        private void RenderPiece(GameObject piece, Material material)
        {
            var pieceComponents = piece.GetComponentsInChildren(typeof(Renderer));
            foreach (Renderer component in pieceComponents)
            {
                component.material = material;
            }
        }
    }
}
