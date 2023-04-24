using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    Dictionary<string, AudioClip> ac;

    void Awake()
    {
        instance = this;

        ac = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in Resources.LoadAll<AudioClip>("Audio"))
        {
            ac.Add(clip.name, clip);
        }
    }

    public static AudioClip GetSound(string audio)
    {
        return instance.ac[audio];
    }

}
