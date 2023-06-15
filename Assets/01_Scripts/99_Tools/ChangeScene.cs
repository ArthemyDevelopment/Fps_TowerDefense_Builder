using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int GoToScene;


    public void changeScene()
    {
        SceneManager.LoadScene(GoToScene);
    }
}
