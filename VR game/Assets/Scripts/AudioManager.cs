using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        int i = 0;
            while (i < clips.Length)
            {
                if (!audioSource.isPlaying && //button is pressed)
                {
                    audioSource.clip = clips[i];
                    audioSource.Play();
                    i++;
                }
            }
    }
}
