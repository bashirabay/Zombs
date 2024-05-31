using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    public AudioClip[] growlSounds;
    public AudioSource soundSource;
    public float minDelay = 5f;
    public float maxDelay = 10f;

    void Start()
    {
        InvokeRepeating("RandomGrowl", Random.Range(minDelay, maxDelay), Random.Range(minDelay, maxDelay));
    }

    public void GrowlSound()
    {
        int n = Random.Range(0, growlSounds.Length);
        soundSource.clip = growlSounds[n];
        soundSource.PlayOneShot(soundSource.clip);

        AudioClip tempClip = growlSounds[n];
        growlSounds[n] = growlSounds[0];
        growlSounds[0] = tempClip;
    }

    void RandomGrowl()
    {
        GrowlSound(); // Corrected to call the GrowlSound() method directly from within the class
    }
}