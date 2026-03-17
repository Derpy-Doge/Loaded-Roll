using UnityEngine;

public class FaceChange : MonoBehaviour
{
    private MeshRenderer _faces;
    public Texture2D[] textures;

    void Awake()
    {
        _faces = GetComponent<MeshRenderer>();
        _faces.materials[0].SetTexture("_MainTex", GlobalDie.Instance.Faces[Vector3.down].Texture);
        _faces.materials[1].SetTexture("_MainTex", GlobalDie.Instance.Faces[Vector3.up].Texture);
        _faces.materials[2].SetTexture("_MainTex", GlobalDie.Instance.Faces[Vector3.left].Texture);
        _faces.materials[3].SetTexture("_MainTex", GlobalDie.Instance.Faces[Vector3.right].Texture);
        _faces.materials[4].SetTexture("_MainTex", GlobalDie.Instance.Faces[Vector3.forward].Texture);
        _faces.materials[5].SetTexture("_MainTex", GlobalDie.Instance.Faces[Vector3.back].Texture);
    }
}
