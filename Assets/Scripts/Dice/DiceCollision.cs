using System.Collections.Generic;
using UnityEngine;

public class DiceCollision : MonoBehaviour
{
    public AudioSource sfxSource;


    public List<AudioClip> diceDice;
    public List<AudioClip> diceCardboard;
    public List<AudioClip> diceRock;//floor

    void Start()
    {
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            if (sfxSource == null)
            {
                Debug.LogError("AudioSource component not found on " + gameObject.name);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected: " + collision.gameObject.name);
        if (collision == null) return;

        if (collision.gameObject == null) return;

        if (collision.gameObject.CompareTag("Dice"))
        {
            sfxSource.PlayOneShot(diceDice[Random.Range(0, diceDice.Count)]);
        }
        else if (collision.gameObject.CompareTag("Wall"))//cardboard
        {
            sfxSource.PlayOneShot(diceCardboard[Random.Range(0, diceCardboard.Count)]);
        }
        else // if its not a wall or a dice its probably the floor
        {
            sfxSource.PlayOneShot(diceRock[Random.Range(0, diceRock.Count)]);
        }
    }
}
