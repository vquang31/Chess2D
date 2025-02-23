using UnityEngine;

public class PromotePiece : MonoBehaviour
{
    void Start()
    {
        
    }
    public void OnMouseDown()
    {        
        string name = gameObject.name;
        Piece newPiece = null;
        if (name.Contains("Queen"))
        {
            newPiece = BoardManager.instance.selectedPiece.AddComponent<Queen>();
        }
        if (name.Contains("Rook"))
        {
            newPiece = BoardManager.instance.selectedPiece.AddComponent<Rook>();
        }
        if (name.Contains("Bishop"))
        {
            newPiece = BoardManager.instance.selectedPiece.AddComponent<Bishop>();
        }
        if (name.Contains("Knight"))
        {
            newPiece = BoardManager.instance.selectedPiece.AddComponent<Knight>();
        }
        BoardManager.instance.selectedPiece.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        newPiece.firstMove = false;
        newPiece.position = PromotePieceManager.instance.newPosition;
        newPiece.side = PromotePieceManager.instance.side;


        BoardManager.instance.promotion = false;
        PromotePieceManager.instance.DeleteEverything();
        PlayerManager.instance.SwitchTurn();
        GameManager.instance.CancelHighlightAndSelectedChess();

    }
}
