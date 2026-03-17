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

    // Update is called once per frame
    void Update()
    {
        _faces.materials[currentFace].SetTexture("_MainTex", textures[newFace]);
        
    }
}
