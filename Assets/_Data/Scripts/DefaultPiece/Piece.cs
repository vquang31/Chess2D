using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public  class Piece : MonoBehaviour
{
    // update khi move
    // physic position
    // position
    // board[,]
    // Square


    /// <summary>
    /// 1 : white
    /// -1 : black
    /// </summary>
    [SerializeField] public int side;

    [SerializeField] public Vector2Int position;

    public bool firstMove = true;
    private void Awake()
    {

    }
    protected virtual void Start()
    {
        CheckSide();
    }
    private void CheckSide()
    {
        if (this.gameObject.name[0] == 'W')
        {
            side = 1;
        }
        else side = -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Position</returns>
    public virtual List<Vector2Int> GetValidMoves()
    {
        return new List<Vector2Int>();
    }

    public virtual List<Vector2Int> GetValidAttacks(int side)
    {
        return new List<Vector2Int>();
    }

    public void OnMouseDown()
    {
        if (PlayerManager.instance.Turn() == side) // nếu cùng side thì chuyển select
            MouseSelected();
        else // khác side thì kiểm tra có ăn đc không || có ăn được thì ăn, không thì hủy select
        {
            GameObject gameObject = GameObject.Find("Highlight");
            foreach(Transform t in gameObject.transform)
            {
                if (t.name.Contains("HighlightA"))
                {
                    Vector2Int posHighlight = new Vector2Int((t.name[11] - 'a' + 1), (t.name[12] - '0'));
                    if(posHighlight == position)
                    {
                        BoardManager.instance.selectedPiece.GetComponent<Piece>().Attack(position);
                        break;
                    }
                }
            }
        }
    }

    public void MouseSelected()
    {
        if (BoardManager.instance.promotion) return; 
        if (BoardManager.instance.selectedPiece == this.gameObject)
        {
            GameManager.instance.CancelHighlightAndSelectedChess();
        }
        else
        {
            // Debug
            Debug.Log(this.gameObject.name);

            BoardManager.instance.selectedPiece = this.gameObject;
            // Highlight
            HighlightManager.instance.HighlightValidMoves(GetValidMoves());
            HighlightManager.instance.HighlightValidAttacks(GetValidAttacks(-side));
            HighlightManager.instance.HighlightSelf(position);

        }

    }

    public virtual void Move(Vector2Int newPos) {

        // update physicPosition
        // this :  BoardManager.instance.selectedPiece.GetComponent<Piece>()
        this.gameObject.transform.localPosition = new Vector3(newPos.x - 4.5f, newPos.y - 4.5f, -1); // change physic position
        Vector2Int oldPos = BoardManager.instance.selectedPiece.GetComponent<Piece>().position;

        //  EnPassant
        if (BoardManager.instance.selectedPiece.GetComponent<Piece>() is Pawn){
            if(newPos.y == ((side == 1) ?  4 : 5) && BoardManager.instance.selectedPiece.GetComponent<Pawn>().firstMove == true) // qua duong
            {
                EnPassant.instance.ActiveEvent(BoardManager.instance.selectedPiece.GetComponent<Pawn>());
            }
            BoardManager.instance.selectedPiece.GetComponent<Piece>().firstMove = false;
            if (newPos.y == ((side == 1 ) ? 8 : 1))
            {
                BoardManager.instance.selectedPiece.GetComponent<Pawn>().Promote(newPos);
            }
        }
        // Castling 
        if (BoardManager.instance.selectedPiece.GetComponent<Piece>() is King) {
            King king = BoardManager.instance.selectedPiece.GetComponent<King>();
            if (king.firstMove)
            {
                if(newPos.x == 3 || newPos.x == 7)
                {
                    king.Castling(newPos);
                }
            }
        }

        BoardManager.instance.selectedPiece.GetComponent<Piece>().firstMove = false;
        //  do not change ORDER
        // update data Piece.position
        this.position = newPos;
        // update data board
        BoardManager.instance.board[newPos.x, newPos.y] = this;
        BoardManager.instance.board[oldPos.x, oldPos.y] = null;
        GameObject newSquareGameObject = GameObject.Find("Square_" + (char)('a' - 1 + newPos.x) + newPos.y.ToString());
        GameObject oldSquareGameObject = GameObject.Find("Square_" + (char)('a' - 1 + oldPos.x) + oldPos.y.ToString());

        // update data square
        Square newSquare = newSquareGameObject.GetComponent<Square>();
        newSquare.pieceGameObject = oldSquareGameObject.GetComponent<Square>().pieceGameObject;
        oldSquareGameObject.GetComponent<Square>().pieceGameObject = null;

        //BoardManager.instance.showTable();
        
        // update Threat position 



        GameManager.instance.CancelHighlightAndSelectedChess();
        PlayerManager.instance.SwitchTurn();
    }

    public virtual void Attack(Vector2Int pos)
    {
        GameObject newSquareGameObject = GameObject.Find(SearchingMethod.NameSquare(pos));
        Square newSquare = newSquareGameObject.GetComponent<Square>();


        if (BoardManager.instance.selectedPiece.GetComponent<Piece>() is Pawn)
        {
            Pawn pawn = BoardManager.instance.selectedPiece.GetComponent<Pawn>(); 
            if (pawn.passing != Vector2Int.zero && pos == pawn.passing)
            {
                BoardManager.instance.board[pawn.passing.x, pawn.passing.y] = null;
                GameObject.Find("Square_" + (char)('a' - 1 + pawn.passing.x) + (pawn.passing.y + ((side == 1) ? -1 : 1)).ToString())
                    .GetComponent<Square>().pieceGameObject.GetComponent<Piece>().Delete();
            }
            else newSquare.pieceGameObject.GetComponent<Piece>().Delete(); // ?
        }
        else
        {
            // delete piece
            newSquare.pieceGameObject.GetComponent<Piece>().Delete(); // ?
        }
        Move(pos);
    }
    public void Delete()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Vua còn bị chiếu thì còn không thể di chuyển được 
    /// </summary>
    /// <param name="newPosition"> vị trí mới của quân cờ</param>
    /// <returns>Bool</returns>
    public virtual bool IsMoveOke(Vector2Int newPosition) {

        if (BoardManager.instance.assumption == true) return true;
        BoardManager.instance.assumption = true;


        string s = ((side == 1) ? "White" : "Black") + "King";
        King myKing = GameObject.Find(s).GetComponent<King>();

        // tư tưởng là giả sử đi vào ô đó và lấy ra những ô bị đe dọa 
        BoardManager.instance.board[newPosition.x, newPosition.y] = this ;
        BoardManager.instance.board[position.x, position.y] = null;
        List<Vector2Int> threatPositions = myKing.GetThreatPositions();
        BoardManager.instance.board[position.x, position.y] = this;
        BoardManager.instance.board[newPosition.x, newPosition.y] = null;

        BoardManager.instance.assumption = false;
        if(this is King)
        {
            if(threatPositions.Contains(newPosition)) return false;
        }
        else
        {
            if (threatPositions.Contains(myKing.position)) return false;
        }
        return true;
    }

    public virtual bool IsAttackOke(Vector2Int newPosition)
    {
        if (BoardManager.instance.assumption == true) return true;
        BoardManager.instance.assumption = true;

        string s = ((side == 1) ? "White" : "Black") + "King";
        King myKing = GameObject.Find(s).GetComponent<King>();

        Piece piece = BoardManager.instance.board[newPosition.x, newPosition.y];

        BoardManager.instance.board[newPosition.x, newPosition.y] = this;
        BoardManager.instance.board[position.x, position.y] = null;
        List<Vector2Int> threatPositions = myKing.GetThreatPositions(newPosition);
        BoardManager.instance.board[position.x, position.y] = this;
        BoardManager.instance.board[newPosition.x, newPosition.y] = piece;

        BoardManager.instance.assumption = false;
        if (this is King)
        {
            if (threatPositions.Contains(newPosition)) return false;
        }
        else
        {
            if (threatPositions.Contains(myKing.position)) return false;
        }
        return true;
    }
}
