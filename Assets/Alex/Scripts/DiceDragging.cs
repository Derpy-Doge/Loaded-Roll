using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DiceDragging : MonoBehaviour
{

    public Transform diceTF;
    [SerializeField] private float rotationSpeed = 1f;
    private RectTransform rect;
    private DiceVisual currentSlot;
    private bool hovering;
    public bool returning;
    [HideInInspector] public FaceChange visualFC;
    [HideInInspector] public bool Dragging;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        visualFC = diceTF.GetComponent<FaceChange>();
    }

    public void SetSlot(DiceVisual slot)
    {
        currentSlot = slot;
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
            rect.anchoredPosition = Vector2.zero; //change this later
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
        GetComponent<RawImage>().material = null;
        rect.anchoredPosition = Vector2.zero;
        returning = false;
    }
}