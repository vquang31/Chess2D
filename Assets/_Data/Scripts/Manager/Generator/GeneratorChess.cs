using UnityEngine;

public class GeneratorChess : MonoBehaviour
{
    static public GeneratorChess instance; 
    public GameObject pawnGameObject0;
    public GameObject rookGameObject0;
    public GameObject knightGameObject0;
    public GameObject bishopGameObject0;
    public GameObject queenGameObject0;
    public GameObject kingGameObject0;

    public GameObject pawnGameObject1;
    public GameObject rookGameObject1;
    public GameObject knightGameObject1;
    public GameObject bishopGameObject1;
    public GameObject queenGameObject1;
    public GameObject kingGameObject1;

    private void Awake()
    {
        instance = this;
        // WhitePiece
        pawnGameObject0 = GameObject.Find("Prefab_Pawn0");
        rookGameObject0 = GameObject.Find("Prefab_Rook0");
        knightGameObject0 = GameObject.Find("Prefab_Knight0");
        bishopGameObject0 = GameObject.Find("Prefab_Bishop0");
        queenGameObject0 = GameObject.Find("Prefab_Queen0");
        kingGameObject0 = GameObject.Find("Prefab_King0");
        // BlackPiece
        pawnGameObject1 = GameObject.Find("Prefab_Pawn1");
        rookGameObject1 = GameObject.Find("Prefab_Rook1");
        knightGameObject1 = GameObject.Find("Prefab_Knight1");
        bishopGameObject1 = GameObject.Find("Prefab_Bishop1");
        queenGameObject1 = GameObject.Find("Prefab_Queen1");
        kingGameObject1 = GameObject.Find("Prefab_King1");
    }

    public void Generate()
    {
        for(int y = 0; y < 2; y++)
        {
            for(int x = 1; x <= 8; x++)
            {
                GameObject newPawn = GameObject.Instantiate((y == 0) ? pawnGameObject0 : pawnGameObject1);
                newPawn.name = ((y == 0) ? "White" : "Black") + "Pawn" + x.ToString();
                newPawn.transform.parent = transform.Find("Pieces");
                newPawn.transform.localPosition = new Vector3( x - 4.5f , (y == 0) ? -2.5f : 2.5f, -1);

                Pawn pawn = newPawn.GetComponent<Pawn>();
                pawn.position = new Vector2Int(x  ,  (y == 0) ? 2 : 7);

                SaveDataToSquareAndArray(pawn, newPawn);
            }
            for(int x = 1; x <= 2; x++)
            {
                GameObject newRook   = GameObject.Instantiate((y == 0) ? rookGameObject0   : rookGameObject1);
                GameObject newKnight = GameObject.Instantiate((y == 0) ? knightGameObject0 : knightGameObject1);
                GameObject newBishop = GameObject.Instantiate((y == 0) ? bishopGameObject0 : bishopGameObject1);

                newRook.name = ((y == 0) ? "White" : "Black") + "Rook" + x.ToString();
                newKnight.name = ((y == 0) ? "White" : "Black") + "Knight" + x.ToString();
                newBishop.name = ((y == 0) ? "White" : "Black") + "Bishop" + x.ToString();

                newRook.transform.parent = transform.Find("Pieces");   
                newKnight.transform.parent = transform.Find("Pieces"); 
                newBishop.transform.parent = transform.Find("Pieces"); 

                newRook.transform.localPosition   = new Vector3((x == 1) ? -3.5f : 3.5f, (y == 0) ? -3.5f : 3.5f, -1);
                newKnight.transform.localPosition = new Vector3((x == 1) ? -2.5f : 2.5f, (y == 0) ? -3.5f : 3.5f, -1);
                newBishop.transform.localPosition = new Vector3((x == 1) ? -1.5f : 1.5f, (y == 0) ? -3.5f : 3.5f, -1);
       
                Rook rook = newRook.GetComponent<Rook>();  
                rook.position = new Vector2Int((x == 1) ? 1 : 8, (y == 0) ?  1 : 8);
                Knight knight = newKnight.GetComponent<Knight>();
                knight.position = new Vector2Int((x == 1) ? 2 : 7, (y == 0) ? 1 : 8);
                Bishop bishop = newBishop.GetComponent<Bishop>();
                bishop.position = new Vector2Int((x == 1) ? 3 : 6, (y == 0) ? 1 : 8);
                
                SaveDataToSquareAndArray(rook  , newRook);
                SaveDataToSquareAndArray(knight, newKnight);
                SaveDataToSquareAndArray(bishop, newBishop);
            }
            GameObject newQueen = GameObject.Instantiate((y == 0) ? queenGameObject0 : queenGameObject1);
            GameObject newKing  = GameObject.Instantiate((y == 0) ? kingGameObject0 : kingGameObject1);
            newQueen.name = ((y == 0) ? "White" : "Black") + "Queen1";
            newKing.name  = ((y == 0) ? "White" : "Black") + "King";

            newQueen.transform.parent = transform.Find("Pieces"); 
            newKing.transform.parent = transform.Find("Pieces");

            newQueen.transform.localPosition = new Vector3(-0.5f, (y == 0) ? -3.5f : 3.5f, -1);
            newKing.transform.localPosition  = new Vector3( 0.5f, (y == 0) ? -3.5f : 3.5f, -1);

            Queen queen = newQueen.GetComponent<Queen>();
            queen.position = new Vector2Int(4, (y == 0) ? 1 : 8);

            King king = newKing.GetComponent<King>();
            king.position = new Vector2Int(5, (y == 0) ? 1 : 8);

            SaveDataToSquareAndArray(queen, newQueen);
            SaveDataToSquareAndArray(king, newKing);    

        }
        //BoardManager.instance.showTable();
    }
    public void SaveDataToSquareAndArray(Piece piece, GameObject gameObject)
    {
        BoardManager.instance.board[piece.position.x, piece.position.y] = piece; // Save array
        GameObject squareGameObject = GameObject.Find("Square_" + (char)('a' - 1 + piece.position.x) + piece.position.y.ToString());
        squareGameObject.GetComponent<Square>().pieceGameObject = gameObject;
    }
}
