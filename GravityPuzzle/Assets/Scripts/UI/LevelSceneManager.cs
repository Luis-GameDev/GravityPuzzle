using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private Transform startingPos;
    private GameObject player;
    void Awake()
    {
        InstantiatePlayer();
    }

    void FixedUpdate()
    {
        if(player == null)
        {
            InstantiatePlayer();
        }
    }

    void InstantiatePlayer()
    {
        startingPos = GameObject.Find("SpawnPoint").transform;
        player = Instantiate(playerPrefab, startingPos);
    }
}
