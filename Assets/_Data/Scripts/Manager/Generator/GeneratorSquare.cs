using UnityEngine;

public class GeneratorSquare : MonoBehaviour
{
    static public GeneratorSquare instance;
    private GameObject whiteSquare;
    private GameObject blackSquare;

    private void Awake()
    {
        instance = this;
        whiteSquare = GameObject.Find("Prefab_WhiteSquare");
        blackSquare = GameObject.Find("Prefab_BlackSquare");
    }

    void Start()
    {
        instance = this;
    }

    public void Generate()
    {
        int x = 0;
        for(int i = 1; i <= 8; i++)
        {
            for(int j = 1; j <= 8; j++)
            {
                GameObject newSquare;
                if (x == 0)
                {
                    x = 1;
                    newSquare = GameObject.Instantiate(blackSquare);
                }
                else
                {
                    x = 0;
                   newSquare = GameObject.Instantiate(whiteSquare);
                }
                newSquare.name = "Square_" + (char)('a' + j - 1) + i.ToString();
                newSquare.transform.parent = transform.Find("BoardSquare");
                newSquare.transform.localPosition = new Vector3(j - 4.5f, i - 4.5f , 0);
            }
            if (x == 0) x = 1;
            else x = 0;
        }
    }
}
