using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List <GameObject> visuals; 
    [SerializeField] private List <int> openVisuals = new List<int> ();

    [SerializeField] private RawImage inventoryImage;
    [SerializeField] private Camera inventoryCamera;
    [SerializeField] Transform inventoryWorldCenter;
    [SerializeField] private float clickRadius;
    [SerializeField] private float pullRadius = 5f;

    [SerializeField] private LayerMask diceLayer; //should likely make this its own unique inventory one later ee
    public Transform map;

    [HideInInspector] public static Inventory Instance;

    private GameObject dicePrefab;
    private List<GameObject> diceInv = new();
    
    private Vector3 spawnPos;
    private DiceHolder diceHolder;
    private bool pulling;
    private bool placing;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is multiple inventory scripts in this scene.");
        }
        Instance = this;
        
    }

    private void Start()
    {

        dicePrefab = Resources.Load<GameObject>("Prefabs/DiceInventory");
        spawnPos = map.position + new Vector3(0f, 3f, 0f);
        diceHolder = DiceHolder.Instance;


        maxX = inventoryWorldCenter.GetChild(0).position.x;
        minX = inventoryWorldCenter.GetChild(1).position.x;
        maxZ = inventoryWorldCenter.GetChild(2).position.z;
        minZ = inventoryWorldCenter.GetChild(3).position.z;
    }

    private void FixedUpdate()
    {
        if (pulling)
        {
            HandlePull();
        }
    }

    public int GetDiceCount()
    {
        return diceInv.Count;
    }

    public bool TryGetPosition(out Vector3 worldPos)
    {
        worldPos = Vector3.zero;

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    worldPos = hit.point;
        //    return true;
        //}

        //return false;

        RectTransform rectTransform = inventoryImage.rectTransform;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 pos))
        {
            return false;
        }

        Rect rect = rectTransform.rect;

        float x = (pos.x - rect.x) / rect.width;
        float y = (pos.y - rect.y) / rect.height;

        if (x < 0 || x > 1 || y < 0 || y > 1)
        {
            return false;
        }

        Vector3 vpPos = new(x, y, 0f);
        Ray ray = inventoryCamera.ViewportPointToRay(vpPos);
        Plane plane = new(inventoryWorldCenter.up, inventoryWorldCenter.position);

        if (plane.Raycast(ray, out float distance))
        {
            worldPos = ray.GetPoint(distance);
            return true;
        }

        return false;
    }


    public void Click(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || GameManager.Instance.CurrentState != GameManager.GameStates.Rolling) 
        {
            return;
        }


        if (TryGetPosition(out Vector3 pos))
        {
            Collider[] hits = Physics.OverlapSphere(pos, clickRadius, diceLayer);
            Collider closest = null;
            float minDist = 0f;

            for (int i = 0; i < hits.Length; i++)
            {
                float dist = Vector3.Distance(pos, hits[i].transform.position);
                if (i == 0)
                {
                    minDist = dist;
                    closest = hits[i];

                }

                if (dist < minDist)
                {
                    minDist = dist;
                    closest = hits[i];
                }
            }
            if (closest != null) //if you clicked on an object in the inventory
            {
                if (!placing) 
                {
                    Debug.Log("Dont Swap Places");
                    if (openVisuals.Count <= 0)
                    {
                        Debug.LogError("Trying to display more dice than available cameras");
                        return;
                    }
                    diceHolder.CreateDice(closest.gameObject.GetComponent<FaceChange>().Dice, visuals[openVisuals[0]], openVisuals[0]); //When we change global dice this will have to chagne too'
                    openVisuals.RemoveAt(0);
                    diceInv.Remove(closest.gameObject);
                    Destroy(closest.gameObject);
                }
            }
        }
       
       placing = false;


    }

    public void RightClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pulling = true;

        }
        else if (ctx.canceled)
        {
            pulling = false;

        }

       
    }

    private void HandlePull()
    {
        if (TryGetPosition(out Vector3 pos))
        {
            Collider[] hits = Physics.OverlapSphere(pos, pullRadius, diceLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                Vector3 direction = pos - hits[i].transform.position;
                direction.y = 0f;
                if (Vector3.Distance(pos, direction) > 1f)
                {
                    Rigidbody rb = hits[i].gameObject.GetComponent<Rigidbody>();
                    rb.AddForce(direction * 5f, ForceMode.Impulse);
                    rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 10f);
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -100, 2f), rb.linearVelocity.z);


                }
            }
        }
    }

    public void Scroll(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && pulling)
        {
            pullRadius = Mathf.Clamp(pullRadius + ctx.ReadValue<Vector2>().y, 1f, 10f);
        }
    }
    


    private void CalculatePlacePosition(ref Vector3 newPos)
    {
        newPos.y = 3f;
        if (newPos.x <= minX)
        {
            newPos.y += minX - newPos.x;   
            newPos.x = minX + 1f;
        }
        else if (newPos.x >= maxX)
        {
            newPos.y += newPos.x - maxX;
            newPos.x = maxX - 1f;

        }

        if (newPos.z <= minZ)
        {
            newPos.y += minZ - newPos.z;   
            newPos.z = minZ + 1f;
        }
        else if (newPos.z >= maxZ)
        {
            newPos.y += newPos.z - maxZ; //
            newPos.z = maxZ - 1f;

        }
    }

    public void PlaceDice(DiceDragging dice, bool defaultLoc)
    {
        //dice.diceTF.transform.position = spawnPos;
        Vector3 pos;

        if (TryGetPosition(out Vector3 ePos) || !defaultLoc)
        {
            CalculatePlacePosition(ref ePos);
            pos = ePos;

        }
        else
        {
            pos = spawnPos;
            
        }

        GameObject newDice = Instantiate(dicePrefab, pos, dice.diceTF.rotation);
        GlobalDie gDie = newDice.AddComponent<GlobalDie>();
        FaceChange fc = newDice.AddComponent<FaceChange>();
        gDie.Faces = dice.diceTF.GetComponent<FaceChange>().Dice.Faces; //if we make globaldie not a scriptable object then we'll need to add the global die script instead the face change script and uncomment the below code
        fc.Dice = gDie;

        // MeshRenderer mr = newDice.GetComponent<MeshRenderer>(); 
        // MeshRenderer nmr = dice.diceTF.GetComponent<MeshRenderer>();
        // for (int i = 0; i < 6; i++)
        // {
        //     mr.materials[i].SetTexture("_BaseMap", nmr.materials[i].GetTexture("_BaseMap"));
        // }
        diceInv.Add(newDice);
        
        openVisuals.Add(dice.cameraIndex);
        placing = true;
        Destroy(dice.gameObject);
    }
}
