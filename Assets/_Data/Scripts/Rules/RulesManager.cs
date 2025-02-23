using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    static public RulesManager instance;
    private void Awake()
    {
        instance = this;    

    }
    void Start()
    {
        
    }

    public void checkRule(int sideAttack)
    {
        string s = ((sideAttack == 1) ? "Black" : "White") + "King";
        King opponentKing = GameObject.Find(s).GetComponent<King>();
        List<Vector2Int> threatPositions = opponentKing.GetThreatPositions();

        List<Vector2Int> validMoves = new List<Vector2Int>();
        GameObject pieces = GameObject.Find("Pieces");
        foreach (Transform transform in pieces.transform) { 
            Piece piece = transform.GetComponent<Piece>();
            if (piece.side != sideAttack)
            {
                //Debug.Log(piece.name + "-----"); 
            
                List<Vector2Int>  moves = piece.GetValidMoves();
                List<Vector2Int>  attacks = piece.GetValidAttacks(sideAttack);
                foreach (Vector2Int move in moves) { 
                    validMoves.Add(move);
                }

                foreach(Vector2Int attack in attacks)
                {
                    validMoves.Add(attack);
                }

            }
        }
        if (validMoves.Count == 0) {
            if (threatPositions.Contains(opponentKing.position))
            {
                string text = ((sideAttack == 1) ? "White" : "Black") + " win !!!!";
                Debug.Log(text);
                UIManager.instance.SetText(sideAttack);
            }
            else
            {
                Debug.Log("Draw");
                UIManager.instance.SetText("Draw!!!");
            }
        }
        CheckThreat(1);
        CheckThreat(-1);
        return;
    }

    public void CheckThreat(int side)
    {
        string s = ((side == 1) ? "Black" : "White") + "King";
        King king = GameObject.Find(s).GetComponent<King>();
        List<Vector2Int> threatPositions = king.GetThreatPositions();
        if (threatPositions.Contains(king.position))
        {
            Debug.Log("Threat-"+ ((side == 1) ? "Black" : "White") + "King !!!");
            king.threat = true;
        }
        else
        {
            king.threat = false;
        }
    }
}
