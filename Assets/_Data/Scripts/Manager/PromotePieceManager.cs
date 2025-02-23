using UnityEngine;

public class PromotePieceManager : MonoBehaviour
{
    static public PromotePieceManager instance;
    private GeneratorPromotePiece generatorPromotePiece;
    public int side;
    public Vector2Int newPosition;

    GameObject queenGameObject;
    GameObject rookGameObject;
    GameObject bishopGameObject;
    GameObject knightGameObject;
    GameObject labelGameObject;
    private void Awake()
    {
        instance = this;
        side = 0;
        generatorPromotePiece = GetComponent<GeneratorPromotePiece>();
    }


    public void Run(Pawn pawn, Vector2Int newPos)
    {
        side = pawn.side;
        newPosition = newPos;
        generatorPromotePiece.Generate(newPos, pawn.side);
        queenGameObject = GameObject.Find("PromoteQueen");
        rookGameObject = GameObject.Find("PromoteRook");
        bishopGameObject = GameObject.Find("PromoteBishop");
        knightGameObject = GameObject.Find("PromoteKnight");
        labelGameObject = GameObject.Find("LabelPromotePiece");

        Destroy(queenGameObject.gameObject.GetComponent<Piece>());
        Destroy(knightGameObject.gameObject.GetComponent<Piece>());
        Destroy(rookGameObject.gameObject.GetComponent<Piece>());
        Destroy(bishopGameObject.gameObject.GetComponent<Piece>());

    }

    public void DeleteEverything()
    {
        side = 0;
        newPosition = Vector2Int.zero;
        Destroy(queenGameObject);
        Destroy(knightGameObject);
        Destroy(rookGameObject);
        Destroy(bishopGameObject);
        Destroy(labelGameObject);
    }

}
