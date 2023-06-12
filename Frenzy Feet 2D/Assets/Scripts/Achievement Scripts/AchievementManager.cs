using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AchievementManager : MonoBehaviour
{
    public GameObject achievementPrefab;

    private AchievementButton activeButton;

    public ScrollRect scrollRect;

    public GameObject achievementMenu;

    public GameObject visualAchievement;

    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    public Sprite unlockedSprite;

    public Text totalPoints;

    public CoinAchievements coinAchievements;

    private static AchievementManager instance;               

    public static AchievementManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManager>(); 
            }
            return AchievementManager.instance; 
        }
    }

    private int fadeTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll(); //need to remove before build

        activeButton = GameObject.Find("General Button").GetComponent<AchievementButton>(); 
        CreateAchievement("General", "Loose Change", "Collect 10 coins", 5, 10);
        CreateAchievement("General", "Grand Scheme of Things", "Collect 1,000 coins", 10, 1000);
        CreateAchievement("General", "I see $$", "Collect 10,000 coins", 20, 10000);
        CreateAchievement("General", "How you *lakh* that?", "Collect 1,00,000", 25, 100000);
        CreateAchievement("General", "First Million", "Collect 10,00,000 coins", 35, 1000000);

        foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
        {
            achievementList.SetActive(true);

            achievementMenu.SetActive(true);
        }

        activeButton.Click();
    }

    // Update is called once per frame
    void Update()
    {

    }

        public void EarnAchievement(string title)
        {
            if (achievements[title].EarnAchievement())
            {
                GameObject achievement = (GameObject)Instantiate(visualAchievement);
                SetAchievementInfo("Earn Canvas", achievement, title);
                totalPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
                StartCoroutine(FadeAchievement(achievement));
            }
        }

        public IEnumerator HideAchievement(GameObject achievement)
        {
            yield return new WaitForSeconds(3);
            Destroy(achievement);

        }

        public void CreateAchievement(string parent, string title, string description, int points, int progress)
        {
            GameObject achievement = (GameObject)Instantiate(achievementPrefab);

            Achievement newAchievement = new Achievement(name, description, points, achievement, progress);

            achievements.Add(title, newAchievement);

            SetAchievementInfo(parent, achievement, title, progress);
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

        public void ChangeCategory(GameObject button)
        {
            AchievementButton achievementButton = button.GetComponent<AchievementButton>();

            scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();

            achievementButton.Click();
            activeButton.Click();
            activeButton = achievementButton;
        }

        private IEnumerator FadeAchievement(GameObject achievement)
        {
            CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();

            float rate = 1.0f / fadeTime;

            int startAlpha = 0;
            int endAlpha = 1;
            for (int i = 0; i < 2; i++)
            {
                float progress = 0.0f;

                while (progress < 1.0f)
                {
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

                    progress += rate * Time.deltaTime;

                    yield return null;
                }

                yield return new WaitForSeconds(2);
                startAlpha = 1;
                endAlpha = 0;
            }

            Destroy(achievement);
        }

        public void GoBack()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

