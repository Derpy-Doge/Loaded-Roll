using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RollDice : MonoBehaviour
{
    private Transform dice;
    private Rigidbody rb;
    [SerializeField] private float diceSpinCooldown;
    [SerializeField] private Transform diceCamera;
    [SerializeField] private bool follow;

    [SerializeField] private List<Transform> dices = new();
    private Dictionary<Transform, Rigidbody> diceRB = new();


    void Start()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            diceRB.Add(dices[i], dices[i].GetComponent<Rigidbody>());
        }
        rb = gameObject.GetComponent<Rigidbody>();
        dice = gameObject.transform;
    }

    void Update()
    {
        if (follow)
        {
           diceCamera.position = new Vector3(dice.position.x, diceCamera.position.y, dice.position.z); 
        }
       //
    }

    public void AHHAGBAH()
    {
        StartCoroutine(Roll());
    }

    IEnumerator Roll()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            dices[i].position = new Vector3(i - 2f, -2f, i - 2f);
            dices[i].Rotate( new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
            diceRB[dices[i]].linearVelocity = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));
            diceRB[dices[i]].angularVelocity = new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), Random.Range(-8f, 8f));
            yield return new WaitForSeconds(diceSpinCooldown);
        }

    }





}
