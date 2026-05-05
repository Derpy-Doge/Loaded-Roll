using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PhysicalAceDie : MonoBehaviour
{
    private AceDieVisual.AceDie aceDie;
    private Rigidbody rb;
    private float calcCooldown = .25f;
    private float timeSinceCalc;
    private bool calculated = true;
    private Face rolledFace;

    [HideInInspector] public bool CurrentlyRolling;

    [SerializeField] private GlobalDie globalDie;
    [SerializeField] private FaceChange faceChange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aceDie = (AceDieVisual.AceDie) SaveDataController.Instance.current.run.AceDie;
        rb = GetComponent<Rigidbody>();
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (aceDie == AceDieVisual.AceDie.Horse)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                if (rb.linearVelocity.y > .5)
                {
                    rb.linearVelocity = new (rb.linearVelocity.x, rb.linearVelocity.y * 1.5f, rb.linearVelocity.z);
                }
            }
        }
    }
    
    void Update()
    {
        timeSinceCalc -= Time.deltaTime;
        if (!calculated && timeSinceCalc <= 0f && (CalculateSpeed() < .2f)) //Add check here 
        {
            calculated = true;
            ReadFaces();
        }
    }
    private float CalculateSpeed()
    {
        timeSinceCalc = calcCooldown;
        float speed = 0f;

            speed += rb.linearVelocity.magnitude;

        

        return speed;
    }

    public void Roll()
    {
        transform.Rotate(new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
        transform.position = new Vector3(0f, -2f, 0f);
        rb.linearVelocity = new Vector3(Random.Range(-16f, 16f), 0f, Random.Range(-16f, 16f));
        rb.angularVelocity = new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), Random.Range(-8f, 8f));
        calculated = false;
        CurrentlyRolling = true;

        
    }

    private void ReadFaces()
    {
        Dictionary<Vector3, Face> sides = new()
        {
            [-transform.up] = globalDie.Faces[Vector3.down],
            [transform.up] = globalDie.Faces[Vector3.up],
            [-transform.right] = globalDie.Faces[Vector3.left],
            [transform.right] = globalDie.Faces[Vector3.right],
            [-transform.forward] = globalDie.Faces[Vector3.back],
            [transform.forward] = globalDie.Faces[Vector3.forward]
        };

        var ordered = sides.Select(item => item.Key).OrderByDescending(item => Vector3.Dot(item, Vector3.up));

        rolledFace = sides[ordered.FirstOrDefault()];
        int num = rolledFace.pips;
        sides[ordered.FirstOrDefault()].Effect.Invoke();
        CurrentlyRolling = false;

    }
}
