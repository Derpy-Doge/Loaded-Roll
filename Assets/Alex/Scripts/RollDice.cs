using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RollDice : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float diceSpinCooldown;
    [SerializeField] private Transform diceCamera;
    [SerializeField] private bool follow;

    [SerializeField] private List<Transform> dices = new();
    private Dictionary<Transform, Rigidbody> diceRB = new();
    private bool calculated = true;
    private float timeSinceCalc;
    private float calcCooldown = .25f;

<<<<<<< HEAD
    [SerializeField] private AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, int> _faces; //
=======
>>>>>>> d76d091b592b3af9a68b12202c848be80d753879


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
        if (!calculated && timeSinceCalc <= 0f)
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
            calculated = false;
            yield return new WaitForSeconds(diceSpinCooldown);
        }

    }

    [ContextMenu("Read Faces")]
    private void ReadFaces()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            Dictionary<Vector3, Face> sides = new()
            {
                [-dices[i].transform.up] = GlobalDie.Instance.Faces[Vector3.down],
                [dices[i].transform.up] = GlobalDie.Instance.Faces[Vector3.up],
                [-dices[i].transform.right] = GlobalDie.Instance.Faces[Vector3.left],
                [dices[i].transform.right] = GlobalDie.Instance.Faces[Vector3.right],
                [-dices[i].transform.forward] = GlobalDie.Instance.Faces[Vector3.back],
                [dices[i].transform.forward] = GlobalDie.Instance.Faces[Vector3.forward]
            };

            var ordered = sides.Select(item => item.Key).OrderByDescending(item => Vector3.Dot(item, Vector3.up));
<<<<<<< HEAD
            var num = sides[ordered.FirstOrDefault()];
=======
            int num = sides[ordered.FirstOrDefault()].pips;
            sides[ordered.FirstOrDefault()].Effect.Invoke();
>>>>>>> d76d091b592b3af9a68b12202c848be80d753879

            Debug.Log(num);
        }
    }
}
