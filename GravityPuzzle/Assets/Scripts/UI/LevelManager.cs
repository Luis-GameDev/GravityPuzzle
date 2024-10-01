using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform contentObject;
    [SerializeField] private int maxAmountOfLevel = 30;
    public List<GameObject> levelPrefabsList = new List<GameObject>();
    void Awake()
    {
        for(int i = 1; i <= maxAmountOfLevel; i++)
        {
            GameObject newLevel = Instantiate(levelPrefab, contentObject);
            newLevel.GetComponent<InitiateLevel>().levelIndex = i;
            levelPrefabsList.Add(newLevel);
            print(i);
        }
    }
}
