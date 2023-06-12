using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchievementButton : MonoBehaviour
{

    public GameObject achievementList;

    public Sprite neutral, hightlight;

    private Image sprite;

    void Awake()
    {
        sprite = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Click()
    {
        if (sprite.sprite == neutral)
        {
            sprite.sprite = hightlight;
            achievementList.SetActive(true);
        }
        else
        {
            sprite.sprite = neutral;
            achievementList.SetActive(false);
        }
    }
}
