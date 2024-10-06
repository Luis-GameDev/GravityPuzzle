using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    private int highestCompletedLevel;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            winScreen.SetActive(true);

            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            int sceneIndex = int.Parse(sceneName);

            if(PlayerPrefs.HasKey("PlayerProgress"))
            {
                highestCompletedLevel = PlayerPrefs.GetInt("Level");
            }

            if(sceneIndex > highestCompletedLevel) 
            {
                if(PlayerPrefs.HasKey("PlayerProgress"))
                {
                    PlayerPrefs.SetInt("Level", sceneIndex);
                    int stars = PlayerPrefs.GetInt("Stars");
                    float starsF = Mathf.Ceil(sceneIndex/2);
                    stars += (int)starsF;
                    PlayerPrefs.SetInt("Stars", stars);
                    PlayerPrefs.Save();
                }
            }   
        }
    }
}
