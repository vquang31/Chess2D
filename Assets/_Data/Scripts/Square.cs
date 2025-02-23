using UnityEngine;
using UnityEngine.UIElements;

public class Square : MonoBehaviour
{
    public GameObject pieceGameObject;

    protected void OnMouseDown()
    {
        if (BoardManager.instance.selectedPiece == null) // chua select 
        {
            if (pieceGameObject != null &&
                pieceGameObject.GetComponent<Piece>().side == PlayerManager.instance.Turn()) 
            {
                pieceGameObject.GetComponent<Piece>().MouseSelected();
            }
            else
            {
                GameManager.instance.CancelHighlightAndSelectedChess();
            }
        }
        else
        {
            GameManager.instance.CancelHighlightAndSelectedChess();
        }
    }
}
