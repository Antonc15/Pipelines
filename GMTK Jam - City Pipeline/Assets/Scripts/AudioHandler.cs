using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    #region Singleton
    private static AudioHandler _instance;

    public static AudioHandler Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    public void MakeSound(GameObject soundObject, Vector3 pos, float pitch)
    {
        GameObject sound = Instantiate(soundObject, pos, Quaternion.identity, transform);

        AudioSource source = sound.GetComponent<AudioSource>();
        source.pitch = pitch;

        Destroy(sound, source.clip.length / source.pitch);
    }
}
