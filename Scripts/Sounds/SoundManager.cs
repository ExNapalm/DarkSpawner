using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{//Holds all Sound Effects
    public Sounds[] aSFX;

    public Sounds GetSFX(string soundname)
    {
        foreach (Sounds s in aSFX)
        {
            if (soundname == s.sfxID)
            {
                return s;
            }

        }

        return null;
    }

    public Sounds GetSFX(int soundid)
    {
        foreach (Sounds s in aSFX)
        {
            if (soundid == s.sfxNum)
            {
                return s;
            }

        }

        return null;
    }
}
