using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RollDice : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float diceSpinCooldown;
    [SerializeField] private Transform diceCamera;
    [SerializeField] private bool follow;

    [SerializeField] private List<Transform> dices = new();
    private Dictionary<Transform, Rigidbody> diceRB = new();
    private bool calculated;
    private float timeSinceCalc;
    private float calcCooldown = .25f;


    void Start()
    {

        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            diceRB.Add(dices[i], dices[i].GetComponent<Rigidbody>());
        }
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        timeSinceCalc -= Time.deltaTime;
        if (!calculated && timeSinceCalc <= 0f && CalculateSpeed() >= .5f)
        {
            Debug.Log("test");
            calculated = true;
            ReadFaces();
        }
    }

    public void AHHAGBAH()
    {
        StartCoroutine(Roll());
    }

    private float CalculateSpeed()
    {
        timeSinceCalc = calcCooldown;
        float speed = 0f;

        foreach (var dice in dices)
        {
            speed += diceRB[dice].linearVelocity.magnitude;
        }

        return speed;
    }

    IEnumerator Roll() //Make this seeded
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            dices[i].position = new Vector3(i - 2f, -2f, i - 2f);
            dices[i].Rotate(new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
            diceRB[dices[i]].linearVelocity = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));
            diceRB[dices[i]].angularVelocity = new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), Random.Range(-8f, 8f));
            yield return new WaitForSeconds(diceSpinCooldown);
        }

    }

    private void ReadFaces()
    {
        string[] faces = new string[dices.Count];
        for (int i = 0; i < dices.Count; i++)
        {

        }
    }











}
