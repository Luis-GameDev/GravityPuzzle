using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBox : MonoBehaviour
{
    private Vector3 boxStartPos;
    private Quaternion boxStartRotation;

    void Start()
    {
        GameObject box = GameObject.FindGameObjectWithTag("Box");

        if (box != null)
        {
            boxStartPos = box.transform.position;
            boxStartRotation = box.transform.rotation;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetTheBox()
    {
        GameObject box = GameObject.FindGameObjectWithTag("Box");

        if (box != null)
        {
            box.transform.position = boxStartPos;
            box.transform.rotation = boxStartRotation;

            Rigidbody2D rb2D = box.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.velocity = Vector2.zero;            
                rb2D.angularVelocity = 0f; 
                rb2D.Sleep(); 
            }
        }
        else
        {
            Debug.LogWarning("Error 404: No Box found");
        }
    }
}
