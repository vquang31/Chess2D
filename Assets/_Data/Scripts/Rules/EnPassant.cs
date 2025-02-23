using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnPassant : MonoBehaviour
{
    static public EnPassant instance;


    /// <summary>
    /// 0 : not Active
    /// 
    /// 1 : prepare Active next Turn
    /// 
    /// 2 : Active, next turn -> active = 0 and reset
    /// /// 
    /// Do this because maybe it can acctiveEvent continuous 2time/ 2turn
    /// </summary>
    public int active;
    public int sideAttack;

    public Pawn pawnTarget;

    public List<Pawn> pawnAttacks;
    void Start()
    {
        instance = this;
        active = 0;
        sideAttack = 0;
        pawnTarget = null;
    }

    public void checkEnPassant()
    {
        if (active == 1) active = 2;
    }


    public void ActiveEvent(Pawn p)
    {
        //Debug.Log(p.name);
        active = 1;
        this.sideAttack = ( p.side == 1 ) ? -1 : 1;
        this.pawnTarget = p;

        // process

        List<Vector2Int> pos = new List<Vector2Int> ();
        if (pawnTarget.position.x + 1 <= 8 )
            pos.Add(new Vector2Int(pawnTarget.position.x + 1, (sideAttack == 1) ? 5: 4));
        if (pawnTarget.position.x - 1 >= 1)
            pos.Add(new Vector2Int(pawnTarget.position.x - 1, (sideAttack == 1) ? 5 : 4));
        
        foreach (Vector2Int position in pos)
        {
            string s = "Square_" + (char)(position.x + 'a' - 1) + position.y.ToString(); 
            Square square = GameObject.Find(s).GetComponent<Square>();
            if (square.pieceGameObject != null) {  // square đó phải chứa object
                if( square.pieceGameObject.GetComponent<Piece>() is Pawn && 
                    square.pieceGameObject.GetComponent<Piece>().side == sideAttack) // square chứa Pawn có side = sideAttack
                {
                    Pawn pawnAttack = square.pieceGameObject.GetComponent<Pawn>();
                    pawnAttack.passing = new Vector2Int(pawnTarget.position.x, pawnTarget.position.y + ((sideAttack == 1) ? -1 : 1 ));
                    pawnAttacks.Add(pawnAttack);
                    //Debug.Log(pawnAttack);
                }
            }
        }
    }
    public void CancelActiveEvents()
    {
        active = 0;
        this.sideAttack = 0; 
        pawnTarget = null;
        foreach (Pawn pawn in pawnAttacks)
        {
            pawn.passing = Vector2Int.zero;
        }
        pawnAttacks.Clear();
    }

}


