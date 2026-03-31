using UnityEngine;

public class FaceChange : MonoBehaviour
{
    private MeshRenderer _faces;
    public GlobalDie Dice; // When swaping the die in shop change the face change die

    void Start()
    {
        _faces = GetComponent<MeshRenderer>();
        if (Dice != null)
        {
            UpdateDiceFaces();
        }
    }

    public void UpdateDiceFaces()
    {
        _faces.materials[0].SetTexture("_BaseMap", Dice.Faces[Vector3.forward].Texture); //eeee
        _faces.materials[1].SetTexture("_BaseMap", Dice.Faces[Vector3.down].Texture);
        _faces.materials[2].SetTexture("_BaseMap", Dice.Faces[Vector3.right].Texture);
        _faces.materials[3].SetTexture("_BaseMap", Dice.Faces[Vector3.back].Texture); 
        _faces.materials[4].SetTexture("_BaseMap", Dice.Faces[Vector3.left].Texture);
        _faces.materials[5].SetTexture("_BaseMap", Dice.Faces[Vector3.up].Texture);
    }
}
