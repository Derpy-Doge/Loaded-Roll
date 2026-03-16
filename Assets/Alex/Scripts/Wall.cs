using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.position * -1000f);
        }
    }
}
