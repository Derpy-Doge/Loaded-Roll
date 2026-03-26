using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;




public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Camera> cameras = new ();
    private List <int> openCameras = new List<int> ();

    [SerializeField] private RawImage inventoryImage;
    [SerializeField] private Camera inventoryCamera;
    [SerializeField] Transform inventoryWorldCenter;
    [SerializeField] private float clickRadius;
    [SerializeField] private float pullRadius = 5f;

    [SerializeField] private LayerMask diceLayer; //should likely make this its own unique inventory one later
    public Transform map;

    [HideInInspector] public static Inventory Instance;

    private GameObject dicePrefab;
    private List<GameObject> diceInv = new();
    
    private Vector3 spawnPos;
    private DiceHolder diceHolder;
    private bool pulling;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("There is multiple inventory scripts in this scene.");
        }
        Instance = this;

        openCameras.Add(5);

        dicePrefab = Resources.Load<GameObject>("Prefabs/DiceInventory");
        spawnPos = map.position + new Vector3(0f, 3f, 0f);
        diceHolder = DiceHolder.Instance;
    }

    private void FixedUpdate()
    {
        if (pulling)
        {
            HandlePull();
        }
    }

    public bool TryGetPosition(out Vector3 worldPos)
    {
        worldPos = Vector3.zero;

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
        if (!ctx.performed)
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
            if (closest != null)
            {
                Debug.Log(closest.gameObject.name);
            }
        }

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

    public void PlaceDice(DiceDragging dice)
    {
        //dice.diceTF.transform.position = spawnPos;
        GameObject newDice = Instantiate(dicePrefab, spawnPos, Quaternion.identity);
        MeshRenderer mr = newDice.GetComponent<MeshRenderer>(); 
        MeshRenderer nmr = dice.diceTF.GetComponent<MeshRenderer>();
        for (int i = 0; i < 6; i++)
        {
            mr.materials[i].SetTexture("_BaseMap", nmr.materials[i].GetTexture("_BaseMap"));
        }
        diceInv.Add(newDice);
        Destroy(dice.gameObject);
        
    }



}
