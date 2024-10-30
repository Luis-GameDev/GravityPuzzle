using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private int level;

    private string GetScene(int nextLevelFactor)
    {
        //nextLevelFactor is the value that the level is gonna be heighered by
        //e.g.: Next Level = currentLevel + 1; Repeat current = currentLevel + 0
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        level = int.Parse(sceneName);

        int nextSceneInt = level + nextLevelFactor;
        string nextScene = nextSceneInt.ToString();
        return nextScene;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }

    public void RepeatLevel()
    {
        SceneManager.LoadScene(GetScene(0));
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(GetScene(1));
    }
}
