using UnityEngine;

public class Box : MonoBehaviour
{
    // Hàm in ra màn hình debug
    public void IN()
    {
        Debug.Log("Đã nhấn vào Box: " + gameObject.name);
    }

    // Gọi IN() khi nhấp vào GameObject này
    private void OnMouseDown()
    {
        IN();
    }
}
/*  History
 *  Begin: 8 / 2 / 2025
 *      
 *  -- 8 / 2 / 2025     
 *      Generator Square+Piece
 *  -- 9 / 2 / 2025
 *      Learn ClickEvent
 *  
 *  -- 10 / 2 / 2025
 *      Display validMove
 *      Highlight + HighlightManager
 *  
 *  -- 11 / 2 / 2025 
 *      PlayerManger + Turn-base    
 *      Rule's Move of piece
 *  -- 12 / 2 / 2025 
 *      Rules:
 *          Pawn EnPassant : tốt qua đường
 *          PawnMove (NOT DONE)
 *  -- 13 / 2 / 2025
 * 
 * 
 *  -- 14 / 2 / 2025
 *      KingValidMove
 *      
 *  -- 15 / 2 / 2025
 *      King.Castling 
 *      
 *  -- 16 / 2 / 2025
 *      Piece.IsMoveOke  
 *      Piece.IsAttackOke
 *           ??????? -->> chuẩn bị xong 
 *              GIẢI THÍCH: 2 HÀM NÀY ĐÓNG VAI TRÒ kiểm tra những nước đi hợp lệ 
 *                          (đang nằm trên đường chiếu tướng của có quân có nước đi dài như Rook, Bishop, Queen)
 *      RotationSystem
 *      
 *      Còn: Promotion, Rules, restartGame, sound Game :vv
 *  -- 17 / 2 / 2025    
 *      Promotion, Rules(Win + Draw)
 *  -- 18 / 2 / 2025
 *      fix bug: phong cấp chiếu tướng
 *      ---- NEW: hàm Destroy đánh dấu một component và được xóa cuối cùng của một frame
 *      Có thể sử dụng DestroyImmediate
 *      Có thể khiến cho một số logic có thể sai 
 *  -- 23 / 2 / 2025
 *      fix bug moveKing  IsAttackOke() and IsMoveOke() 233-240
 *      thêm checkThreat
 */