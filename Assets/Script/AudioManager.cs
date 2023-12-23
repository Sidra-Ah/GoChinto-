using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[System.Serializable]
//public class Sound
//{
//    public string Name;
//    public AudioClip AudioClip;
//    [Range(0f, 1f)]
//    public float Volume;
//    [HideInInspector]
//    public AudioSource AudioSource;
//    public bool Loop;
//}
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach (Sound s in sounds)
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.AudioClip;
            s.AudioSource.volume = s.Volume;
            s.AudioSource.loop = s.Loop;
        }

    }

    public void Play(string Name)
    {
        Sound saudio = Array.Find(sounds, s => s.Name == Name);
        if (saudio == null)
        {
            Debug.LogWarning("Can not the Audio file");
        }
        else
        {
            Debug.Log("Playing sound: " + Name);
            saudio.AudioSource.Play();
        }

    }


    public void PauseGameMusic()
    {
        foreach (Sound s in sounds)
        {
            if (s.Name == "GameMusic") // Adjust the name based on your actual game music sound
            {
                s.AudioSource.Pause();
                break; // Assuming there's only one "GameMusic" sound, you can break out of the loop
            }
        }
    }

    public void ResumeGameMusic()
    {
        foreach (Sound s in sounds)
        {
            if (s.Name == "GameMusic") // Adjust the name based on your actual game music sound
            {
                s.AudioSource.UnPause();
                break; // Assuming there's only one "GameMusic" sound, you can break out of the loop
            }
        }
    }
}
