using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    static public BoardManager instance;
    public HighlightManager highlightManager;
    public GameObject pieceGameObjects;
    public bool assumption = false; // trường hợp giả sử 
    public bool promotion = false;

    public Piece[,] board = new Piece[9, 9];  // truy xuất nhanh để kiểm tra chess 


    public GameObject selectedPiece;


    private void Awake()
    {
        instance = this;
        highlightManager = GetComponent<HighlightManager>();
    }
    void Start()
    {
        pieceGameObjects = GameObject.Find("Pieces");
    }


    public bool isCellEmpty(Vector2Int pos)
    {
        if( pos.x < 1 || pos.y < 1 || pos.x > 8 || pos.y > 8) return false;
        if (board[pos.x, pos.y] == null) return true;
        return false;
    }

    // side : side of Opponent
    public bool IsCellOccupiedByOpponent(Vector2Int pos, int side)
    {
        if (pos.x < 1 || pos.y < 1 || pos.x > 8 || pos.y > 8) return false;
        return !isCellEmpty(pos) && board[pos.x, pos.y].side == side;
    }

    public void showTable()
    {
        for(int y = 1; y < 9; y++)
        {
            for(int x = 1; x < 9; x++)
            {
                if(board[x, y] != null)
                {
                    Debug.Log(x + " " + y + ": " + board[x,y].name);
                }
            }
        }
    }
}
