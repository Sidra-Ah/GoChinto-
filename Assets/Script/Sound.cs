using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;
    [Range(0f, 1f)]
    public float Volume;
    [HideInInspector]
    public AudioSource AudioSource;
    public bool Loop;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
