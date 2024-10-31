using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetProgress : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Image>().color = Color.green;
    }
    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        gameObject.GetComponent<Image>().color = Color.red;
    }
}
