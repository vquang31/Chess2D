using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }
    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();


        // Bishop
        for (int x = -1; x <= 1; x += 2) // nừa trái , nửa phải
        {
            for (int y = 1; y < 8; y++)
            {
                Vector2Int move = new Vector2Int(x * y, y);
                if (!BoardManager.instance.isCellEmpty(position + move))
                {
                    break;
                }
                if (IsMoveOke(position + move))
                    validMoves.Add(position + move);
            }

            for (int y = -1; y > -8; y--)
            {
                Vector2Int move = new Vector2Int(x * y, y);
                if (!BoardManager.instance.isCellEmpty(position + move))
                    break;
                if (IsMoveOke(position + move))
                    validMoves.Add(position + move);
            }
        }
        // Rook

        for (int j = 0; j < 2; j++) // j = 0 -> x-axis || j = 1 -> y-axis
        {
            for (int i = 1; i < 8; i++)
            {
                Vector2Int move = new Vector2Int(((j == 0) ? i : 0), ((j == 0) ? 0 : i));
                if (!BoardManager.instance.isCellEmpty(position + move))
                {
                    break;
                }
                if (IsMoveOke(position + move))
                    validMoves.Add(position + move);
            }
            for (int i = -1; i > -8; i--)
            {
                Vector2Int move = new Vector2Int(((j == 0) ? i : 0), ((j == 0) ? 0 : i));
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

        // Bishop
        for (int j = 0; j < 2; j++) // j = 0 -> x-axis || j = 1 -> y-axis
        {
            for (int i = 1; i < 8; i++)
            {
                attackMove = new Vector2Int(((j == 0) ? 1 : -1) * i, i);
                if (!BoardManager.instance.isCellEmpty(position + attackMove))
                {
                    if (BoardManager.instance.IsCellOccupiedByOpponent(position + attackMove, s) && IsAttackOke(position + attackMove))
                    {
                            validMoves.Add(position + attackMove);
                    }
                    break;
                }
            }
            for (int i = -1; i > -8; i--)
            {
                attackMove = new Vector2Int(((j == 0) ? 1 : -1) * i, i);
                if (!BoardManager.instance.isCellEmpty(position + attackMove))
                {
                    if (BoardManager.instance.IsCellOccupiedByOpponent(position + attackMove, s) && IsAttackOke(position + attackMove)) 
                        validMoves.Add(position + attackMove);
                    break;
                }
            }
        }

        // Rook
        for (int j = 0; j < 2; j++) // j = 0 -> x-axis || j = 1 -> y-axis
        {
            for (int i = 1; i < 8; i++)
            {
                attackMove = new Vector2Int(((j == 0) ? i : 0), ((j == 0) ? 0 : i));
                if (!BoardManager.instance.isCellEmpty(position + attackMove))
                {
                    if (BoardManager.instance.IsCellOccupiedByOpponent(position + attackMove, s) && IsAttackOke(position + attackMove))
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
