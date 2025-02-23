using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }
    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        List<Vector2Int> moves = new List<Vector2Int>()
        {
            new Vector2Int( -2, 1),
            new Vector2Int(-2,-1),
            new Vector2Int(-1,2),
            new Vector2Int(-1,-2),
            new Vector2Int(1,2),
            new Vector2Int(1,-2),
            new Vector2Int(2, 1),
            new Vector2Int(2, -1)            
        };

        foreach (var move in moves) { 
            if(BoardManager.instance.isCellEmpty(position + move) && IsMoveOke(position + move))
                validMoves.Add(position + move);
        }


        return validMoves;
    }
    public override List<Vector2Int> GetValidAttacks(int s)
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        List<Vector2Int> attackMoves = new List<Vector2Int>()
        {
            new Vector2Int(-2,1),
            new Vector2Int(-2,-1),
            new Vector2Int(-1,2),
            new Vector2Int(-1,-2),
            new Vector2Int(1,2),
            new Vector2Int(1,-2),
            new Vector2Int(2, 1),
            new Vector2Int(2, -1)
        };

        foreach (var move in attackMoves)
        {
            if (BoardManager.instance.IsCellOccupiedByOpponent(position + move, s) && IsAttackOke(position + move))
                validMoves.Add(position + move);
        }

        return validMoves;
    }

}
