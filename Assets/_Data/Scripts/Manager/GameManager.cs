using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    static public GameManager instance;
    public RotationSystem rotationSystem;
    private void Awake()
    {
        instance = this;
        rotationSystem = gameObject.GetComponent<RotationSystem>();
    }

    public void CancelHighlightAndSelectedChess()
    {
        HighlightManager.instance.ClearHighlights();
        if (BoardManager.instance.promotion) return;
        BoardManager.instance.selectedPiece = null;
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
