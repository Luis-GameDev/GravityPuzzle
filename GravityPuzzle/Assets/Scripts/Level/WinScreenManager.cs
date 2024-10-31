using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    private int level;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName == "Tutorial") return;
        level = int.Parse(sceneName);

        GetComponent<Text>().text = "Level " + level;
    }
}
