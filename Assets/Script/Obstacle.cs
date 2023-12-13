using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour
{
    public static Action OnCollisionWithPlayer;
    public GameObject ExplosionPrefeb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(OnCollisionWithPlayer != null)
            {
                OnCollisionWithPlayer();
            }
            
        }
        if (other.tag == "Asteriod")
        {
            Instantiate(ExplosionPrefeb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
