using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Pawn : Piece
{

    public Vector2Int passing;
    protected override void Start()
    {
        base.Start();
        passing = Vector2Int.zero;
    }
    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        int direction = side;

        Vector2Int forward = new Vector2Int(0,  direction);
        if (BoardManager.instance.isCellEmpty(position + forward) && IsMoveOke(position + forward)) 
            validMoves.Add(position + forward);

        if (firstMove)
        {
            Vector2Int forward2 = new Vector2Int(0,  direction * 2);
            if(BoardManager.instance.isCellEmpty(position + forward) &&   
               BoardManager.instance.isCellEmpty(position + forward2) &&
               IsMoveOke(position + forward2)) 
                validMoves.Add(position + forward2);
        }

        return validMoves;
    }

    public override List<Vector2Int> GetValidAttacks(int s)
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();
        // Attack
        int direction = side;
        List<Vector2Int> attackMoves = new List<Vector2Int>();
        if (position.x > 1) attackMoves.Add(new Vector2Int( - 1,  direction));
        if (position.x < 8) attackMoves.Add(new Vector2Int(   1,  direction));

        foreach (var attackMove in attackMoves)
        {
            if (BoardManager.instance.IsCellOccupiedByOpponent(position + attackMove, s) && IsAttackOke(position + attackMove))
                validMoves.Add(position + attackMove);
        }
   
        // qua duong
        if(passing != Vector2Int.zero && IsAttackOke(passing)) validMoves.Add(passing); 

        return validMoves;
    }


    public void Promote(Vector2Int newPos)
    {
        Destroy(BoardManager.instance.selectedPiece.GetComponent<Pawn>());
        PromotePieceManager.instance.Run(this, newPos);
        BoardManager.instance.promotion = true;
    }

}
