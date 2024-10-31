using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform contentObject;
    [SerializeField] private int maxAmountOfLevel = 5;
    [SerializeField] private Text starsAmountText;
    private int highestCompletedLevel = 0;
    private int amountOfStars = 0;
    private string amountOfStarsString;
    public List<GameObject> levelPrefabsList = new List<GameObject>();

    void Awake()
    {   
        if(PlayerPrefs.GetInt("Tutorial") != 1)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            highestCompletedLevel = PlayerPrefs.GetInt("Level");
            amountOfStars = PlayerPrefs.GetInt("Stars");

            amountOfStarsString = amountOfStars.ToString();
            starsAmountText.text = amountOfStarsString;

            for(int i = 1; i <= maxAmountOfLevel; i++)
            {
                GameObject newLevel = Instantiate(levelPrefab, contentObject);
                newLevel.GetComponent<InitiateLevel>().levelIndex = i;
                if(i <= highestCompletedLevel)
                {
                    Transform checkmark = newLevel.transform.Find("Completed");
                    newLevel.GetComponent<InitiateLevel>().levelCompleted = true;

                    if(checkmark != null)
                    {
                        Image checkmarkImage = checkmark.GetComponent<Image>();

                        if(checkmarkImage != null)
                        {
                            checkmarkImage.enabled = true;
                        }
                    }
                }
                else if(i > highestCompletedLevel+1 && i != 1)
                {
                    Transform locked = newLevel.transform.Find("Locked");

                    if(locked != null)
                    {
                        Image lockedImage = locked.GetComponent<Image>();

                        if(lockedImage != null)
                        {
                            lockedImage.enabled = true;
                        }
                    }
                }
                levelPrefabsList.Add(newLevel);
            }
        }
    }
}
