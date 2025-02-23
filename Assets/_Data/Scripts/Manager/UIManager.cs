using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;
    private GameObject announcementTextGameObject;

    private void Awake()
    {
        instance = this;
        announcementTextGameObject = GameObject.Find("AnnouncementText");
    }

    public void SetText(string text)
    {
        announcementTextGameObject.GetComponent<TextMeshPro>().text = text;
    }
    public void SetText(int side)
    {
        string text = ((side == 1) ? "White" : "Black") + " Win!!!";
        announcementTextGameObject.GetComponent<TextMeshPro>().text = text;
        if (side == 1)
            announcementTextGameObject.transform.Rotate(0, 0, 180);
    }

}
