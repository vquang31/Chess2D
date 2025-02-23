using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    static public HighlightManager instance;
    public GameObject highlightRedPrefab;
    public GameObject highlightGreenPrefab;
    public GameObject highlightGrayPrefab;
    public List<GameObject> highlights = new();
    private void Awake()
    {
        instance = this;
        highlightRedPrefab = GameObject.Find("Prefab_HighlightRed");
        highlightGreenPrefab = GameObject.Find("Prefab_HighlightGreen");
        highlightGrayPrefab = GameObject.Find("Prefab_HighlightGray");
    }



    public void HighlightValidMoves(List<Vector2Int> positions)
    {
        ClearHighlights();
        foreach (var pos in positions)
        {
            GameObject highlight = Instantiate(highlightGrayPrefab, new Vector3(pos.x - 4.5f, pos.y - 4.5f, -0.5f), Quaternion.identity);
            highlight.name = "HighlightM_" + (char)('a' + pos.x - 1) + pos.y.ToString();
            highlight.transform.parent = GameObject.Find("Highlight").transform;
            highlights.Add(highlight);
        }
    }


    public void HighlightValidAttacks(List<Vector2Int> positions) 
    { 
        foreach(var pos in positions)
        {
            GameObject highlight = Instantiate(highlightRedPrefab, new Vector3(pos.x - 4.5f, pos.y - 4.5f, -0.5f), Quaternion.identity);
            highlight.name = "HighlightA_" + (char)('a' + pos.x - 1) + pos.y.ToString();
            highlight.transform.parent = GameObject.Find("Highlight").transform;
            highlights.Add(highlight);
        }   
    }

    public void HighlightSelf(Vector2 pos)
    {
        GameObject highlight = Instantiate(highlightGreenPrefab, new Vector3(pos.x - 4.5f, pos.y - 4.5f, -0.5f), Quaternion.identity);
        highlight.name = "HighlightS_" + (char)('a' + pos.x - 1) + pos.y.ToString();
        highlight.transform.parent = GameObject.Find("Highlight").transform;
        highlights.Add(highlight);
    }

    public void ClearHighlights()
    {
        foreach (GameObject highlight in highlights)
            Destroy(highlight);
        highlights.Clear();
    }

}
