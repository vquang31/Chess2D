using UnityEngine;

public class HighlightGreen: Highlight
{
    protected override void OnMouseDown()
    {
        GameManager.instance.CancelHighlightAndSelectedChess();
    }
}
