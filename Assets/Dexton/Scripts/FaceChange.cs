using UnityEngine;

public class FaceChange : MonoBehaviour
{
    private MeshRenderer _faces;

    void Start()
    {
        _faces = GetComponent<MeshRenderer>();
        _faces.materials[0].SetTexture("Albedo", GlobalDie.Instance.Faces[Vector3.down].Texture);
        _faces.materials[1].SetTexture("Albedo", GlobalDie.Instance.Faces[Vector3.up].Texture);
        _faces.materials[2].SetTexture("Albedo", GlobalDie.Instance.Faces[Vector3.left].Texture);
        _faces.materials[3].SetTexture("Albedo", GlobalDie.Instance.Faces[Vector3.right].Texture);
        _faces.materials[4].SetTexture("Albedo", GlobalDie.Instance.Faces[Vector3.forward].Texture);
        _faces.materials[5].SetTexture("Albedo", GlobalDie.Instance.Faces[Vector3.back].Texture);
    }
}
