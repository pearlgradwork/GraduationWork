using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public string GameLevel;
   
    public void OpenScene()
    {
        SceneManager.LoadScene(GameLevel);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Main Menu");
    }
}