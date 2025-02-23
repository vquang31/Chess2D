using Unity.VisualScripting;
using UnityEngine;

public class RotationSystem : MonoBehaviour
{
    static public RotationSystem instance;
    [SerializeField] bool rotate = true;
    private GameObject mainCamera;
    private GameObject pieces;
    private void Awake()
    {
        instance = this;
        mainCamera = GameObject.Find("Main Camera");
        pieces = GameObject.Find("Pieces");
    }

    public void Rotate(bool turnPlayer1)
    {
        if (!rotate) return;
        int i = (turnPlayer1 ? 1 : -1);  
        mainCamera.transform.Rotate(0, 0, 180 * i );
        foreach(Transform transform in pieces.transform)
        {
            transform.Rotate(0, 0, 180 * i);
        }

    }
}
