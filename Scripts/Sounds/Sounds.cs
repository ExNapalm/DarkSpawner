using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds

{
    public string sfxID;
    public int sfxNum;
    public AudioClip sfx;
    [HideInInspector] public AudioSource source;
}
