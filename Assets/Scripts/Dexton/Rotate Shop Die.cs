using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

public class RotateShopDie : MonoBehaviour
{
    public GameObject shopDie;
    private Vector3 rotateAmount;
    private int rotateFrames;

    public SerializedDictionary<RotationDirection, Vector3> rotationDictionary = new ();

    public void Awake()
    {
        shopDie = this.gameObject;
    }

    private void FixedUpdate()
    {
        if (rotateFrames > 0)
        {
            shopDie.transform.Rotate(rotateAmount.normalized*5, Space.World);
        }
        rotateFrames--;
    }

    public void Rotate(string direction)
    {
        if (rotateFrames > 0) { return; }

        Debug.Log("Rotating ");

        rotateAmount = rotationDictionary[Enum.Parse<RotationDirection>(direction)];
        rotateFrames = 18;
    }

    public enum RotationDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
