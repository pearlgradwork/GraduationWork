using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public string GameLevel;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OpenScene()
    {
        SceneManager.LoadScene(GameLevel);
    }
}