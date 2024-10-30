using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private Transform startingPos;
    private GameObject player;
    private int level;
    public bool isGameActive = true;
    void Awake()
    {
        InstantiatePlayer();
    }

    void FixedUpdate()
    {
        if(player == null && isGameActive == true)
        {
            InstantiatePlayer();
        }
        else if(isGameActive == false && player != null)
        {
            Destroy(player);
        }
    }

    void InstantiatePlayer()
    {
        startingPos = GameObject.Find("SpawnPoint").transform;
        player = Instantiate(playerPrefab, startingPos);
    }
}
