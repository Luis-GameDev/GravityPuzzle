using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitiateLevel : MonoBehaviour
{
    [SerializeField] public int levelIndex;
    private Text levelText;
    public bool levelCompleted = false;
    private bool locked = true;

    public void Start()
    {
        transform.Find("Text").gameObject.TryGetComponent<Text>(out levelText);

        if(levelText != null)
        {
            levelText.text = "Level " + levelIndex;
        }
        else
        {
            Debug.Log("Error: Couldnt find Text Component!");
        }
    }
    public void StartLevel()
    {
        Transform lockedObj = transform.Find("Locked");

        if(lockedObj != null)
        {
            Image lockedImage = lockedObj.GetComponent<Image>();

            if(lockedImage.enabled)
            {
                locked = true;
            }
            else
            {
                locked = false;
            }
        }

        if(levelIndex != 0 && !locked)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.Log("Error: Level not unlocked!");
        }
    }
}
