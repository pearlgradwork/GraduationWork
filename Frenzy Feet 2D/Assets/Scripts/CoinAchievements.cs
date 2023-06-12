using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAchievements : MonoBehaviour
{
    private AchievementManager achievementManager;
    
    private ScoreManager theScoreManager;

    private pickupPoints pickUpPoints;

    private bool unlocked;

    private GameObject achievementRef;

    public GameObject visualAchievement;

    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    private Text totalPoints;

    private int points;

    private int currentProgression;
    private int maxProgression;

    public string achievementName1;
    public string achievementName2;
    public string achievementName3;
    public string achievementName4;
    public string achievementName5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            gameObject.SetActive(false);
           
            AchievementManager.Instance.EarnAchievement(achievementName1);
            AchievementManager.Instance.EarnAchievement(achievementName2);
            AchievementManager.Instance.EarnAchievement(achievementName3);
            AchievementManager.Instance.EarnAchievement(achievementName4);
            AchievementManager.Instance.EarnAchievement(achievementName5);

            SaveAchievement(true);
        }
    }

    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInfo("Earn Canvas", achievement, title);
            totalPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
        }
    }

    public void SetAchievementInfo(string parent, GameObject achievement, string title, int progression = 0)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);

        string progress = progression > 0 ? " " + PlayerPrefs.GetInt("Progression" + title) + "/" + progression.ToString() : string.Empty;

        achievement.transform.GetChild(0).GetComponent<Text>().text = title + progress;
        achievement.transform.GetChild(1).GetComponent<Text>().text = achievements[title].Description;
        achievement.transform.GetChild(2).GetComponent<Text>().text = achievements[title].Points.ToString();
    }

    public void SaveAchievement(bool value)
    {
        unlocked = value;

        int tmpPoints = PlayerPrefs.GetInt("Points");

        PlayerPrefs.SetInt("Points", tmpPoints += points);

        PlayerPrefs.SetInt("Progression" + name, currentProgression);

        if (value == true)
        {
            PlayerPrefs.SetInt(name, 1);
        }
        else
        {
            PlayerPrefs.SetInt(name, 0);
        }

        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

        if (unlocked)
        {
            AchievementManager.Instance.totalPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            currentProgression = PlayerPrefs.GetInt("Progression" + name);
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
        }
    }

    public bool CheckProgress()
    {
        currentProgression++;

        achievementRef.transform.GetChild(0).GetComponent<Text>().text = name + " " + currentProgression + "/" + maxProgression;

        SaveAchievement(false);

        if (maxProgression == 0)
        {
            return true;
        }

        if (currentProgression >= maxProgression)
        {
            return true;
        }

        return false;
    }
}