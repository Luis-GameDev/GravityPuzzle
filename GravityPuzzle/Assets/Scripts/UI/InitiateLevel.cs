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
    public bool _levelCompletionState = false;

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
        if(levelIndex != 0)
        {
            SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Error: No Level defined!");
        }
    }
}
