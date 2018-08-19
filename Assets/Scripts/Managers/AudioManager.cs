using System.Collections;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;

    public void PlaySoundEffect(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip, 1);
    }
    public void PlaySoundEffect(AudioClip clip, float secondsToPlay)
    {
        StartCoroutine(SoundEffectCoroutine(clip, secondsToPlay));
    }

    public void StopPlayingSoundEffect()
    {
        _effectSource.Stop();
    }


    public void PlayMusic(AudioClip sound, bool loop)
    {
        _musicSource.loop = loop;
        _musicSource.clip = sound;
        _musicSource.Play();
    }
    public void StopPlayingMusic()
    {
        _musicSource.Stop();
    }


    IEnumerator SoundEffectCoroutine(AudioClip clip, float seconds)
    {
        Debug.Log("SoundEffectCoroutine");
        //_effectSource.clip = clip;
        //_effectSource.Play();
        _effectSource.PlayOneShot(clip, 1);
        yield return new WaitForSeconds(seconds);
        _effectSource.Stop();
    }
}
