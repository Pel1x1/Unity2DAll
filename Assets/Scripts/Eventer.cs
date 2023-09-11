using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventer : MonoBehaviour
{
    public static Eventer Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public SoundAudioClip[] Sounds;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound SoundName;
        public AudioClip AudioClip;
        [Range(0f, 1f)]
        public float Volume;
    }

    void OnApplicationQuit()
    {
        SoundManager.DestroySelf();
    }
}
