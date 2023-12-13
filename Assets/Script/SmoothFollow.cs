using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmoothFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float SmoothingSpeed;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.IsGameStarted)
        {
            Vector3 NewPos = Vector3.Lerp(transform.position, target.transform.position + offset, SmoothingSpeed);
            transform.position = NewPos;
        }
       
    }
}
