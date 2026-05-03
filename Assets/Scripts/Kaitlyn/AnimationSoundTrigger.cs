using UnityEngine;

public class AnimationSoundTrigger : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    public AudioClip markerSound;
    public AudioClip eraserSound;
    public AudioClip sawSound;

    public void PlayMarkerSound()
    {
        audioSource1.clip = markerSound;
        audioSource1.Play();
        if (audioSource2 != null)
        {
            audioSource2.clip = sawSound;
            audioSource2.Play();
        }
    }

    public void PlayEraserSound()
    {
        audioSource1.clip = eraserSound;
        audioSource1.Play();
    }
}
