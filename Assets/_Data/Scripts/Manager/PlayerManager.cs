using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static public PlayerManager instance;
    private Player player1;
    private Player player2;

    private void Awake()
    {
        instance = this;
        player1 = GameObject.Find("Player1").GetComponent<Player>();
        player2 = GameObject.Find("Player2").GetComponent<Player>();
    }

    void Start()
    {
        player1.yourTurn = true;
        player2.yourTurn = false;
    }

    public void SwitchTurn()
    {
        if (BoardManager.instance.promotion) return;

        RulesManager.instance.checkRule((player1.yourTurn) ? 1 : -1 );

        player1.yourTurn = player2.yourTurn;
        player2.yourTurn = !player1.yourTurn;
        if (EnPassant.instance.active == 2) { 
            EnPassant.instance.CancelActiveEvents();
        }
        else EnPassant.instance.checkEnPassant();



        // Rotate
        GameManager.instance.rotationSystem.Rotate(player1.yourTurn);
        // End of turn
    }

    /// <summary>
    ///  player1 : White -> 1
    ///  //
    ///  player2 : Black -> -1
    /// </summary>
    /// <returns> side </returns>
    public int Turn()
    {
        if (player1.yourTurn) return 1;
        return -1;
    }
}
