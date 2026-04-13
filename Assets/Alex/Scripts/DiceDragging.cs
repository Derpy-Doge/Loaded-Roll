using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DiceDragging : MonoBehaviour
{

    public Transform diceTF;
    public int cameraIndex;
    [SerializeField] private float rotationSpeed = 1f;
    private RectTransform rect;
    private DiceVisual currentSlot;
    private bool hovering;
    public bool returning; //pretty sure this can be hideininspector but ima not change for now
    [HideInInspector] public FaceChange visualFC;
    [HideInInspector] public bool Dragging;
    public RawImage diceTexture;

    private DiceHolder holder;

    void Start()
    {
        holder = DiceHolder.Instance;
        diceTexture = GetComponent<RawImage>();
        rect = GetComponent<RectTransform>();
        visualFC = diceTF.GetComponent<FaceChange>();
    }



    public void SetSlot(DiceVisual slot)
    {
        currentSlot = slot;

        if (rect == null)
        {
            return;
        }

        transform.SetParent(slot.transform);
        if (!returning)
        {
            rect.anchoredPosition = Vector3.zero;
        }
    }

    public void SetSlot(DiceVisual.StorageType type)
    {
        if (type == DiceVisual.StorageType.Inventory)
        {
            currentSlot = null;
        }
    }

    public DiceVisual GetSlot()
    {
        return currentSlot;
    }

    void Update()
    {
        if (!Dragging)
        {
            return;
        }

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.parent as RectTransform, Input.mousePosition, Camera.main, out pos);
        rect.anchoredPosition = pos;
        Vector2 mousePos = Mouse.current.delta.ReadValue();

        float rotX = mousePos.y * rotationSpeed; //could prob multiply these by time.deltatime
        float rotY = mousePos.x * rotationSpeed; 
        float rotZ = -mousePos.x * rotationSpeed; 
        
        holder.GlowSpeed += Mathf.Clamp(Mathf.Sqrt(mousePos.magnitude / 8.5f) * .005f, 0, 5);


        diceTF.Rotate(rotX, rotY, rotZ, Space.World); 

    }

    public void StopDragging()
    {
        Dragging = false;
        if (returning)
        {
            StartCoroutine(Return()); 
        }
        else
        {
            rect.anchoredPosition = Vector2.zero; 
            holder.GlowSpeed = 0f;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    public void ReturnPosition()
    {
        StartCoroutine(Return()); 
    }

    IEnumerator Return()
    {
        float elapsed = 0f;
        Vector2 startPos = rect.anchoredPosition;
        //Debug.Log($"Start Position: {startPos}");
        while (elapsed < 0.15f)//Change to variable later
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.15f;
            rect.anchoredPosition = Vector2.Lerp(startPos, Vector2.zero, t);
            diceTF.Rotate((-startPos.normalized) * 200f * Time.deltaTime);
            yield return null;
        }
        holder.GlowSpeed = 0f;
        GetComponent<RawImage>().material = null;
        rect.anchoredPosition = Vector2.zero;
        returning = false;
    }
}