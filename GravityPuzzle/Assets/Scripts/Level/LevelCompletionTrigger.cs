using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    void OnTriggerEnter(Collider other)
    {
        print("Detected a collision!");
        if(other.gameObject.tag == "Player")
        {
            print("Detected a collision with the Player!");
            winScreen.SetActive(true);
        }
    }
}
