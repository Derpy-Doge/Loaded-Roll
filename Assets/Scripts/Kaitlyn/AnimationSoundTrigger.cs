using UnityEngine;

public class AnimationSoundTrigger : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip markerSound;
    public AudioClip eraserSound;

    public void PlayMarkerSound()
    {
       audioSource.PlayOneShot(markerSound);
    }

    public void PlayEraserSound()
    {
       audioSource.PlayOneShot(eraserSound);
    }
}
