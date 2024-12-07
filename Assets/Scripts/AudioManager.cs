using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source ---------")]
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------- Audio Clip ---------")]
    public AudioClip _background;
    public AudioClip _jump;
    public AudioClip _jumpScream;
    public AudioClip _mobs;
    public AudioClip _wind;

    private void Start()
    {
        _musicSource.clip = _background;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
