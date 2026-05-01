using UnityEngine;

public class NewDice : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void AddToInventory()
    {
        
    }

    void MakeDie(int amountToMake, GameObject diePrefab)
    {
        for (int i = 0; i < amountToMake; i++)
        {
            GameObject instance = Instantiate(diePrefab, this.transform);
            instance.name = "Die " + (i + 1);
            //instance.AddComponent<GlobalDie>();
            //instance.AddComponent<FaceChange>();
            //GlobalDie die = instance.GetComponent<GlobalDie>();
            //die.Faces = currentRun.Deese[i];//
            //FaceChange face = faceChange[i];
        }
    }
}
