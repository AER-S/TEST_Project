using Common;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flippingSound;
    [SerializeField] private AudioClip matchingSound;
    [SerializeField] private AudioClip mismatchingSound;
    [SerializeField] private AudioClip gameOverSound;

    public void PlayFlippingSound()
    {
        audioSource.PlayOneShot(flippingSound);
    }
    
    public void PlayMatchingSound()
    {
        audioSource.PlayOneShot(matchingSound);
    }
    
    public void PlayMismatchingSound()
    {
        audioSource.PlayOneShot(mismatchingSound);
    }
    
    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}
