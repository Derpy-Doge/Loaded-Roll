using UnityEngine;

public class FaceChange : MonoBehaviour
{
    private MeshRenderer _faces;
    public Texture2D[] textures;
    public int currentFace, newFace;

    void Awake()
    {
        _faces = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        _faces.materials[currentFace].SetTexture("_MainTex", textures[newFace]);
    }
}
