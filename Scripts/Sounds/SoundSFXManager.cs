using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSFXManager : MonoBehaviour
{
    public static SoundSFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }

    }

    public void PlaySoundEffectClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {//Method to play quick sound effects without too many audiosources being created
        AudioSource audiosource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        //Audioclip
        audiosource.clip = audioClip;
        //Volume
        audiosource.volume = volume;
        //Sound itslef
        audiosource.Play();
        //get Length of Audioclip
        float clipLength = audiosource.clip.length;
        //Obliterate clip
        Destroy(audiosource.gameObject, clipLength);

    }

    public void PlayRandomSoundEffectClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //assign  random Index
        int rand = Random.Range(0, audioClip.Length);
        
        //Method to play quick sound effects without too many audiosources being created
        AudioSource audiosource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        //Audioclip
        audiosource.clip = audioClip[rand];
        //Volume
        audiosource.volume = volume;
        //Sound itslef
        audiosource.Play();
        //get Length of Audioclip
        float clipLength = audiosource.clip.length;
        //Obliterate clip
        Destroy(audiosource.gameObject, clipLength);

    }

}

