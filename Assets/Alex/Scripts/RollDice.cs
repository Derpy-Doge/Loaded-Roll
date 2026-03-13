using UnityEngine;
using System.Collections;
public class RollDice : MonoBehaviour
{
    private Transform dice;
    private Rigidbody rb;
    [SerializeField] private Transform diceCamera;
    [SerializeField] private bool follow;


    void Start()
    {
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

    public void Roll()
    {
        dice.position = new Vector3(0f, -2f, 0f);
        dice.Rotate( new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
        rb.linearVelocity = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));
        rb.angularVelocity = new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), Random.Range(-8f, 8f));

    }
    
    
}
