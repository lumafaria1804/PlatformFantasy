using UnityEngine;
using System;
using SysDiag = System.Diagnostics;

public class ActorSFX : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerSFX(AudioClip clip)
    {
        if (clip == null) return;

        if (!audioSource.isPlaying)
           audioSource.PlayOneShot(clip);
    }
}
