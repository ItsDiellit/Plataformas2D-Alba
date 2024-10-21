using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;

    public AudioSource _audioSource;
    [SerializeField] private AudioClip _coinAudio;
    [SerializeField] private AudioClip _jumpAudio;

    public AudioClip coinAudio;
    public AudioClip jumpAudio;

    public AudioClip AttackAudio;

    public AudioClip StepsAudio;

    public AudioClip HurtAudio;

    public AudioClip DeadAudio;

    public AudioClip EnemyAudio;

    public AudioClip PauseAudio;

    public AudioClip GameoverAudio;

    public AudioClip PauseMusicAudio;

    public AudioClip HeartAudio;

    public AudioClip ButtonAudio;

    
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    /*public void CoinSFX()
    {
        _audioSource.PlayOneShot(_coinAudio);
    }*/

    public void PlaySFX(AudioSource source,  AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
