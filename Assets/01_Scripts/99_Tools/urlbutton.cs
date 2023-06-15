using UnityEngine;
using System.Collections;

public class urlbutton : MonoBehaviour
{
    public string Url;
    public void OpenURL()
    {
        Application.OpenURL(Url);
    }

    public void SetUrl(string url)
    {
        Url = url;
    }

}