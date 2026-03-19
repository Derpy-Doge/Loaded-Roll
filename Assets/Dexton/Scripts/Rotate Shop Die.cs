using AYellowpaper.SerializedCollections;
using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public class RotateShopDie : MonoBehaviour
{
    public GameObject shopDie;
    private Quaternion rotation;
    public float lerpSpeed = 5f;
    private Vector3 rotateAmount;
    private int rotateFrames;

    public SerializedDictionary<RotationDirection, Vector3> rotationDictionary = new ();

    public void Awake()
    {
        shopDie = this.gameObject;
    }

    private void Update()
    {
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
