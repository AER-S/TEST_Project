using Common;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flippingSound;
    [SerializeField] private AudioClip matchingSound;
    [SerializeField] private AudioClip mismatchingSound;
    [SerializeField] private AudioClip gameOverSound;

    private void OnEnable()
    {
        Card.CardFlipped += PlayFlippingSound;
        GameManager.DoneComparing += OnDoneComparing;
        GameManager.GameEnded += PlayGameOverSound;
    }

    private void OnDisable()
    {
        Card.CardFlipped -= PlayFlippingSound;
        GameManager.DoneComparing -= OnDoneComparing;
        GameManager.GameEnded -= PlayGameOverSound;
    }
    
    private void OnDoneComparing(bool value)
    {
        if(value) PlayMatchingSound();
        else PlayMismatchingSound();
    }

    private void PlayFlippingSound()
    {
        if (!audioSource.isPlaying || (audioSource.isPlaying&& audioSource.clip != flippingSound))
        {
            audioSource.clip = flippingSound;
            audioSource.Play();
        }
    }
    
    private void PlayMatchingSound()
    {
        audioSource.clip = null;
        audioSource.PlayOneShot(matchingSound);
    }
    
    private void PlayMismatchingSound()
    {
        audioSource.clip = null;
        audioSource.PlayOneShot(mismatchingSound);
    }
    
    private void PlayGameOverSound()
    {
        audioSource.clip = null;
        audioSource.PlayOneShot(gameOverSound);
    }
}
