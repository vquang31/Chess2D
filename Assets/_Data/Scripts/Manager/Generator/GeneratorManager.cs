using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    static GeneratorManager instance;
    private GeneratorChess generatorChess;
    private GeneratorSquare generatorSquare;
    private void Awake()
    {
        instance = this;
        generatorSquare = GetComponent<GeneratorSquare>();
        generatorChess = GetComponent<GeneratorChess>();

    }
    void Start()
    {
        generatorSquare.Generate();
        Debug.Log("Generate SQUARE");
        generatorChess.Generate();
        Debug.Log("Generate CHESS");
    }

   
}
