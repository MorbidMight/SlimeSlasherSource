using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();    
    }
    
    public void playDeath()
    {
        source.PlayOneShot(clips[0]);
    }

    public void playSplosion()
    {
        source.PlayOneShot(clips[1]);
    }
    
    public void playGun()
    {
        source.PlayOneShot(clips[2]);
    }

    public void playHurt()
    {
        source.clip = clips[3];
        source.Play();
    }

    public void playMove()
    {
        source.PlayOneShot(clips[4]);
    }

    public void playReturn()
    {
        source.clip = clips[5];
        source.Play();
    }

    public void playThrow()
    {
        source.clip = clips[6];
        source.Play();
    }
}
