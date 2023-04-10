using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioSource SFXAudio;


    void Start()
    {
        SFXAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(AudioClip[] audioToPLay, float volume)
    {
        SFXAudio.PlayOneShot(audioToPLay[Random.Range(0, audioToPLay.Length)], volume);
    }
    public static void PlaySound(AudioClip audioToPLay, float volume)
    {
        SFXAudio.PlayOneShot(audioToPLay, volume);
    }
}
