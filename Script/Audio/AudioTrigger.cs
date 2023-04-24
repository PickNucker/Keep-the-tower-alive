using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioTrigger
{
    [SerializeField] string[] name;
    [SerializeField][Range(0,1)] float volume = .02f;
    [SerializeField] bool loop;

    public void Play(Vector3 pos)
    {
        if (name.Length == 0) return;

        GameObject go = new GameObject();
        AudioSource ac = go.AddComponent<AudioSource>();

        GameObject parent = GameObject.Find("AudioEffectSpawner");
        go.transform.SetParent(parent.transform);
        go.transform.position = pos;

        ac.volume = volume;
        ac.loop = loop;
        ac.clip = AudioManager.GetSound(name[Random.Range(0, name.Length)]);

        ac.spatialBlend = 1;
        ac.rolloffMode = AudioRolloffMode.Linear;
        ac.dopplerLevel = 0;

        ac.Play();

        if(!loop)
            MonoBehaviour.Destroy(ac.gameObject, ac.clip.length + 0.5f);
    }

    public void Play(Vector3 pos, Transform parent)
    {
        if (name.Length == 0) return;

        GameObject go = new GameObject();
        AudioSource ac = go.AddComponent<AudioSource>();

        go.transform.SetParent(parent.transform);
        go.transform.position = pos;

        ac.volume = volume;
        ac.loop = loop;
        ac.clip = AudioManager.GetSound(name[Random.Range(0, name.Length)]);

        ac.rolloffMode = AudioRolloffMode.Linear;
        ac.dopplerLevel = 0;

        ac.Play();

        if (!loop)
            MonoBehaviour.Destroy(ac.gameObject, ac.clip.length + 0.5f);
    }
}
