using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        PlayerShoot,
        PlayerJumpStart,
        PlayerJumpEnd,
        PlayerDamage,
        EnemyDeath,
        EnemyShoot,
        EnemySpawn
    }

    private static GameObject soundGameObject;
    private static AudioSource soundAudioSource;

    public static void PlaySound(Sound sound)
    {
        if (soundGameObject == null)
        {
            soundGameObject = new GameObject("Sound");
            soundAudioSource = soundGameObject.AddComponent<AudioSource>();
        }

        Eventer.SoundAudioClip soundData = GetAudioClip(sound);
        soundAudioSource.PlayOneShot(soundData.AudioClip, soundData.Volume);

        //Object.Destroy(soundGameObject, soundData.AudioClip.length);
    }

    private static Eventer.SoundAudioClip GetAudioClip(Sound sound)
    {
        foreach (Eventer.SoundAudioClip item in Eventer.Instance.Sounds)
        {
            if (item.SoundName == sound)
            {
                return item;
            }
        }

        return null;
    }

    public static void DestroySelf()
    {
        Object.Destroy(soundGameObject);
        Object.Destroy(soundAudioSource);
    }
}