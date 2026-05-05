using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class DiceCollision : MonoBehaviour
{
    public AudioSource sfxSource;


    public List<AudioClip> diceDice;
    public List<AudioClip> diceCardboard;
    public List<AudioClip> diceRock;//floor
    [SerializeField] private List<AudioClip> neigh;
    [HideInInspector] public bool isHorse;


    void Awake()
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

        if (isHorse)
        {
            int horseIndex = Random.Range(0, neigh.Count);
            sfxSource.PlayOneShot(neigh[horseIndex]);
            if (horseIndex == neigh.Count - 1)
            {
                SendMessage("AddPoints", Random.Range(5, 11) * 100 * 5);
            }
            SendMessage("AddPoints", Random.Range(5, 11) * 100);
            return;
        }

        if (collision.gameObject.CompareTag("Dice"))
        {
            sfxSource.PlayOneShot(diceDice[Random.Range(0, diceDice.Count)]);
        }
        else if (collision.gameObject.CompareTag("Wall"))//cardboard
        {
            if(collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.name == "Bounds")
            {
                sfxSource.PlayOneShot(diceRock[Random.Range(0, diceRock.Count)]);
                return;
            }
            else
            {
                sfxSource.PlayOneShot(diceCardboard[Random.Range(0, diceCardboard.Count)]);
            }            
        }
        else // if its not a wall or a dice its probably the floor
        {
            sfxSource.PlayOneShot(diceRock[Random.Range(0, diceRock.Count)]);
        }
    }

    public IEnumerator PlsNoSound()
    {
        DiceCollision dc = this;
        dc.enabled = false;
        yield return new WaitForSeconds(2f);
        dc.enabled = true;
    }
}
