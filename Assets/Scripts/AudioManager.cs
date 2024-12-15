using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source ---------")]
    [SerializeField] AudioSource _musicSource;
    public AudioSource SFXSource;

    [Header("--------- Audio Clip ---------")]
    public AudioClip _background;

    // Массивы для звуков прыжка и ходьбы
    public AudioClip[] _jumpSounds;
    public AudioClip[] _jumpScream;
    public AudioClip[] _walkSounds;
    public AudioClip[] _mobs;
    public AudioClip[] _wind;

    private void Start()
    {
        _musicSource.clip = _background;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayJumpSound()
    {
        if (_jumpSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, _jumpSounds.Length);
            SFXSource.PlayOneShot(_jumpSounds[randomIndex]);
        }
    }

    public void PlayWalkSound()
    {
        if (_walkSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, _walkSounds.Length);
            SFXSource.PlayOneShot(_walkSounds[randomIndex]);
        }
    }
}
