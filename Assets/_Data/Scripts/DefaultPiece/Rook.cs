using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Rook : Piece
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }
    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        for(int j = 0; j < 2; j++) // j = 0 -> x-axis || j = 1 -> y-axis
        {
            for(int i = 1; i < 8; i++)
            {
                Vector2Int move = new Vector2Int( ((j==0) ? i : 0), ((j == 0) ? 0 : i));
                if (!BoardManager.instance.isCellEmpty(position + move))
                {
                    break;
                }
                if (IsMoveOke(position + move))
                    validMoves.Add(position + move);
            }
            for(int i = -1; i > -8; i--)
            {
                Vector2Int move = new Vector2Int( ((j == 0) ? i : 0), ((j == 0) ? 0 : i));
                if (!BoardManager.instance.isCellEmpty(position + move))
                {
                    break;
                }
                if (IsMoveOke(position + move))
                    validMoves.Add(position + move);
            }
        }

        return validMoves;
    }
    public override List<Vector2Int> GetValidAttacks(int s)
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        Vector2Int attackMove = new Vector2Int();
         
        for(int j = 0; j < 2; j++) // j = 0 -> x-axis || j = 1 -> y-axis
        {
            for(int i = 1; i < 8; i++)
            {
                attackMove = new Vector2Int(((j == 0) ? i : 0),((j == 0) ? 0 : i));
                if (!BoardManager.instance.isCellEmpty(position + attackMove))
                {
                    if(BoardManager.instance.IsCellOccupiedByOpponent(position + attackMove, s) && IsAttackOke(position + attackMove))
                        validMoves.Add(position + attackMove);
                    break;
                }
            }
            for (int i = -1; i > -8; i--)
            {
                attackMove = new Vector2Int(((j == 0) ? i : 0), ((j == 0) ? 0 : i));
                if (!BoardManager.instance.isCellEmpty(position + attackMove))
                {
                    if (BoardManager.instance.IsCellOccupiedByOpponent(position + attackMove, s) && IsAttackOke(position + attackMove))
                        validMoves.Add(position + attackMove);
                    break;
                }
            }
        }
        return validMoves;
    }
}