using UnityEngine;


public class SearchingMethod 
{
    static public GameObject GetGameObjectByPosition(Vector2Int pos)
    {
        string s = NameSquare(pos);
        return GameObject.Find(s).GetComponent<Square>().pieceGameObject;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <returns>string</returns>
    static public string NameSquare(Vector2Int pos)
    {
        return "Square_" + (char)('a' - 1 + pos.x) + pos.y.ToString();
    }

    static public string NameSquare(int x, int y)
    {
        return NameSquare(new Vector2Int(x, y));
    }
}
