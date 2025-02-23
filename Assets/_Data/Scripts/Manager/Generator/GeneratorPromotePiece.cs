using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratorPromotePiece : MonoBehaviour
{
    static public GeneratorPromotePiece instance;
    private GameObject labelGameObject;

    private void Awake()
    {
        instance = this;
        labelGameObject = GameObject.Find("Prefab_Label");
    }
    void Start()
    {
        
    }

    public void Generate(Vector2Int position, int side)
    {
        GameObject newLabel = Instantiate(labelGameObject);
        newLabel.name = "LabelPromotePiece";
        newLabel.transform.localPosition = new Vector3(position.x - 4.5f, side * 2 , -1.5f);
        newLabel.transform.parent = transform;

        GameObject newQueen  = Instantiate((side == 1) ? GeneratorChess.instance.queenGameObject0 : GeneratorChess.instance.queenGameObject1);
        GameObject newKnight = Instantiate((side == 1) ? GeneratorChess.instance.knightGameObject0 : GeneratorChess.instance.knightGameObject1);
        GameObject newRook   = Instantiate((side == 1) ? GeneratorChess.instance.rookGameObject0 : GeneratorChess.instance.rookGameObject1);
        GameObject newBishop = Instantiate((side == 1) ? GeneratorChess.instance.bishopGameObject0 : GeneratorChess.instance.bishopGameObject1);

        newQueen.name  = "PromoteQueen";
        newKnight.name = "PromoteKnight";
        newRook.name   = "PromoteRook";
        newBishop.name = "PromoteBishop";

        int i = (side == 1) ? 1 : -1;
        newQueen.transform.localPosition  = new Vector3(position.x - 4.5f, 3.5f * i, -2f);
        newKnight.transform.localPosition = new Vector3(position.x - 4.5f, 2.5f * i, -2f);
        newRook.transform.localPosition   = new Vector3(position.x - 4.5f, 1.5f * i, -2f);
        newBishop.transform.localPosition = new Vector3(position.x - 4.5f, 0.5f * i, -2f);

        if(side == -1)
        {
            newQueen.transform.Rotate(0, 0, 180);
            newKnight.transform.Rotate(0, 0, 180);
            newRook.transform.Rotate(0, 0, 180);
            newBishop.transform.Rotate(0, 0, 180);
        }

        newQueen.transform.parent  = transform;
        newKnight.transform.parent = transform;
        newRook.transform.parent   = transform;
        newBishop.transform.parent = transform;

        newQueen.AddComponent<PromotePiece>();
        newKnight.AddComponent<PromotePiece>();
        newRook.AddComponent<PromotePiece>();
        newBishop.AddComponent<PromotePiece>();

    }

}
