using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class King : Piece
{
    public bool threat;
    protected override void Start()
    {
        base.Start();
        threat = false;
    }

    public override List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();
        List<Vector2Int> threatPositions = new List<Vector2Int>();
        threatPositions = GetThreatPositions();

        //
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (!(x == 0 && y == 0))
                {
                    Vector2Int move = new Vector2Int(x, y);
                    if (!threatPositions.Contains(position + move))
                    {
                        if (BoardManager.instance.isCellEmpty(position + move) && IsMoveOke(position + move))
                            validMoves.Add(position + move);
                    }
                }
            }
        }
        // Castling
        if(firstMove == true && threat == false)
        {
            Vector2Int castlingMoveR = new Vector2Int(position.x + 2, position.y);
            Vector2Int castlingMoveL = new Vector2Int(position.x - 2, position.y);

            GameObject rookGameObjectL = GameObject.Find("Square_a" + position.y.ToString()).GetComponent<Square>().pieceGameObject;
            GameObject rookGameObjectR = GameObject.Find("Square_h" + position.y.ToString()).GetComponent<Square>().pieceGameObject;

            if(rookGameObjectL != null)
            {
                if (rookGameObjectL.GetComponent<Piece>().firstMove)  
                {
                    bool check = true;
                    Vector2Int pos;
                    for (int x = 2; x <= 4; x++)
                    {
                        pos = new Vector2Int(x, position.y);
                        if (!BoardManager.instance.isCellEmpty(pos))
                        {
                            check = false;
                            break;
                        }
                        if( x == 3 || x == 4)
                        {
                            if (threatPositions.Contains(pos))
                            {
                                check = false;
                                break;
                            }
                        }
                    }
                    if(check) validMoves.Add(castlingMoveL);
                }
            }

            if (rookGameObjectR != null)
            {
                if (rookGameObjectR.GetComponent<Piece>().firstMove)
                {
                    Vector2Int pos;
                    bool check = true;
                    for (int x = 6; x <= 7; x++)
                    {
                        pos = new Vector2Int(x, position.y);
                        if (!BoardManager.instance.isCellEmpty(pos) || threatPositions.Contains(pos))
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check) validMoves.Add(castlingMoveR);
                }
            }
        }

        return validMoves;
    }
    public override List<Vector2Int> GetValidAttacks(int s)
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        List<Vector2Int> threatPositions = new List<Vector2Int>();
        threatPositions = GetThreatPositions();


        //
        Vector2Int attackMove = new Vector2Int();
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (!(x == 0 && y == 0))
                {
                    attackMove = new Vector2Int(x, y);
                    if(!threatPositions.Contains(position + attackMove))
                    {
                        if (BoardManager.instance.IsCellOccupiedByOpponent(position + attackMove, s) && IsAttackOke(position + attackMove))
                            validMoves.Add(position + attackMove);
                    }
                }
            }
        }

        return validMoves;
    }

    // thuật toán này không ổn
    // do nếu ăn thì phải check xem có an toàn không ( quá lâu  ), tốc độ = 8 * hàm cũ 
    /// <summary>
    ///  một bài học khi nghĩ giải thuật 
    ///  khi mà thấy ko ổn hoặc cần phải code thêm vào trong core để có thể đảm bảo giải thuật chạy đúng
    ///  -> cần tìm kiếm một cách khác
    ///  /// phải thêm hàm :vvv 
    ///  -> hoặc là sửa đổi hàm cũ =)) -> do cách này tối ưu về tốc độ
    /// </summary>
    /// <returns></returns>
    public List<Vector2Int> GetThreatPositions()
    {
        List<Vector2Int> threatPositions = new List<Vector2Int>();
        GameObject pieceGameObjects = GameObject.Find("Pieces");
        foreach (Transform tranform in pieceGameObjects.transform)
        {
            Piece piece = tranform.GetComponent<Piece>();
            if (piece.side != this.side)
            {
                if (piece is Pawn)
                {
                    List<Vector2Int> attackMoves = new List<Vector2Int>();
                    if (piece.position.x > 1) attackMoves.Add(new Vector2Int(piece.position.x - 1, piece.position.y - side));
                    if (piece.position.x < 8) attackMoves.Add(new Vector2Int(piece.position.x + 1, piece.position.y - side));

                    foreach(Vector2Int attackMove in attackMoves)
                    {
                        if(!threatPositions.Contains(attackMove))    
                            threatPositions.Add(attackMove);
                    }
                }
                else
                {
                    if(piece is King)
                    {
                        for(int y = -1; y <= 1; y++)
                        {
                            for(int x = -1; x <= 1; x++)
                            {
                                Vector2Int pos = new Vector2Int(piece.position.x + x, piece.position.y + y) ;
                                if (!(pos.x < 1 || pos.y < 1 || pos.x > 8 || pos.y > 8))
                                {
                                    if (!threatPositions.Contains(pos))
                                        threatPositions.Add(pos);
                                }
                            }
                        }
                    }
                    else // 
                    {
                        List<Vector2Int> positions1 = piece.GetValidMoves();
                        List<Vector2Int> positions2 = piece.GetValidAttacks(-1);
                        List<Vector2Int> positions3 = piece.GetValidAttacks(1);
                        foreach (Vector2Int position in positions1)
                        {
                            if (!threatPositions.Contains(position))
                                threatPositions.Add(position);
                        }
                        foreach (Vector2Int position in positions2)
                        {
                            if (!threatPositions.Contains(position))
                            threatPositions.Add(position);
                        }
                        foreach (Vector2Int position in positions3)
                        {
                            if (!threatPositions.Contains(position))
                            threatPositions.Add(position);
                        }
                    }
                }
            }
        }
        return threatPositions;
    }


    public List<Vector2Int> GetThreatPositions(Vector2Int newPosition)
    {
        List<Vector2Int> threatPositions = new List<Vector2Int>();
        GameObject pieceGameObjects = GameObject.Find("Pieces");
        foreach (Transform tranform in pieceGameObjects.transform)
        {
            Piece piece = tranform.GetComponent<Piece>();
            if (piece.side != this.side)
            {
                if (piece.position == newPosition)
                {
                    continue;
                }
                if (piece is Pawn)
                {
                    List<Vector2Int> attackMoves = new List<Vector2Int>();
                    if (piece.position.x > 1) attackMoves.Add(new Vector2Int(piece.position.x - 1, piece.position.y - side));
                    if (piece.position.x < 8) attackMoves.Add(new Vector2Int(piece.position.x + 1, piece.position.y - side));

                    foreach (Vector2Int attackMove in attackMoves)
                    {
                        if (!threatPositions.Contains(attackMove))
                            threatPositions.Add(attackMove);
                    }
                }
                else
                {
                    if (piece is King)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            for (int x = -1; x <= 1; x++)
                            {
                                Vector2Int pos = new Vector2Int(piece.position.x + x, piece.position.y + y);
                                if (!(pos.x < 1 || pos.y < 1 || pos.x > 8 || pos.y > 8))
                                {
                                    if (!threatPositions.Contains(pos))
                                        threatPositions.Add(pos);
                                }
                            }
                        }
                    }
                    else // 
                    {
                        List<Vector2Int> positions1 = piece.GetValidMoves();
                        List<Vector2Int> positions2 = piece.GetValidAttacks(-1);
                        List<Vector2Int> positions3 = piece.GetValidAttacks(1);
                        foreach (Vector2Int position in positions1)
                        {
                            if(!threatPositions.Contains(position))
                                threatPositions.Add(position);
                        }
                        foreach (Vector2Int position in positions2)
                        {
                            if(!threatPositions.Contains(position))
                                threatPositions.Add(position);
                        }
                        foreach (Vector2Int position in positions3)
                        {
                            if(!threatPositions.Contains(position))
                                threatPositions.Add(position);
                        }
                    }
                }
            }
        }
        return threatPositions;
    }



    public void Castling(Vector2 posCastling)
    {
        Vector2Int newPosition = new Vector2Int(((posCastling.x == 3) ? 4 : 6), position.y);
        Vector2Int oldPosition = new Vector2Int(((posCastling.x == 3) ? 1 : 8), position.y);

        // Board
        BoardManager.instance.board[newPosition.x, newPosition.y] = this;
        BoardManager.instance.board[oldPosition.x, oldPosition.y] = null;

        // square
        Square newSquare = GameObject.Find(SearchingMethod.NameSquare(newPosition)).GetComponent<Square>();
        Square oldSquare = GameObject.Find(SearchingMethod.NameSquare(oldPosition)).GetComponent<Square>();
        newSquare.pieceGameObject = oldSquare.pieceGameObject;
        oldSquare.pieceGameObject = null;
            
        // piece.position
        newSquare.pieceGameObject.GetComponent<Rook>().position = newPosition;
        // physicPosition 
        newSquare.pieceGameObject.gameObject.transform.localPosition = new Vector3(newPosition.x - 4.5f, newPosition.y - 4.5f, -1);
    }

    //public override bool IsMoveOke(Vector2Int newPosition)
    //{
    //    return true;
    //}

}
