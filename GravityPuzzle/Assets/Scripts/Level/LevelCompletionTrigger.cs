using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            winScreen.SetActive(true);
        }
    }
}
