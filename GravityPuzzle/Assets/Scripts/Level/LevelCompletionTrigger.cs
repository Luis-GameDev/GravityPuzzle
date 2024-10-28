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
            other.gameObject.GetComponent<GravityController>()._playerDirection = Direction.Down;
            Physics2D.gravity = new Vector2(0f, -9.81f);
            other.gameObject.GetComponent<GravityController>().Rotation(0);

            winScreen.SetActive(true);

            GameObject.Find("LevelSceneManager").GetComponent<LevelSceneManager>().isGameActive = false;

            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            int sceneIndex = int.Parse(sceneName);

            highestCompletedLevel = PlayerPrefs.GetInt("Level");

            if(sceneIndex > highestCompletedLevel) 
            {
                PlayerPrefs.SetInt("Level", sceneIndex);
                int stars = PlayerPrefs.GetInt("Stars");
                stars += 2;
                PlayerPrefs.SetInt("Stars", stars);
                PlayerPrefs.Save();
            }   
        }
    }
}
