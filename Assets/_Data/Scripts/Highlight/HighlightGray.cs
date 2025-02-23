using UnityEngine;

public class HighlightGray : Highlight
{
    protected override void OnMouseDown()
    {
        string s = this.gameObject.name;
        Vector2Int pos = new Vector2Int((s[11] - 'a' + 1), (s[12] - '0'));
        BoardManager.instance.selectedPiece.GetComponent<Piece>().Move(pos);
    }
}
    