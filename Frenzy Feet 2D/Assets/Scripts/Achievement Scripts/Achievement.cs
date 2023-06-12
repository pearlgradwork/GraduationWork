using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement 
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    private bool unlocked;

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

    private int points;

    public int Points
    {
        get { return points;}
        set { points = value; } 
    }
    private GameObject achievementRef;

    private int currentProgression;
    private int maxProgression;

    public Achievement(string name, string description, int points, GameObject achievementRef, int maxProgression)
    {
        this.name = name;
        this.description = description;
        this.unlocked = false;
        this.points = points;
        this.achievementRef = achievementRef;
        this.maxProgression = maxProgression;
        LoadAchievement();
    }

    public bool EarnAchievement()
    {
        if (!unlocked && CheckProgress())
        {
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
            SaveAchievement(true);
            return true;
        }
        return false;
    }

    public void SaveAchievement(bool value)
    {
        unlocked = value;

        int tmpPoints = PlayerPrefs.GetInt("Points");

        PlayerPrefs.SetInt("Points", tmpPoints += points);

        PlayerPrefs.SetInt("Progression" + Name, currentProgression);

        if(value == true)
        {
            PlayerPrefs.SetInt(Name, 1);
        }
        else
        {
            PlayerPrefs.SetInt(Name, 0);
        }

        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(Name) == 1 ? true : false;

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

        achievementRef.transform.GetChild(0).GetComponent<Text>().text = Name + " " + currentProgression + "/" + maxProgression;

        SaveAchievement(false);

        if(maxProgression == 0)
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
