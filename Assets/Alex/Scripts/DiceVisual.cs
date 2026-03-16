using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections;


public class DiceVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{

    public Transform diceTF;
    [SerializeField] private float rotationSpeed = .2f;


    [HideInInspector] public int boxIndex;
     public bool containsDice = false;
    [HideInInspector] public RectTransform diceImage;
    private bool hovering = false;
    private bool holding = false;
    private DiceHolder dHolder;

    void Start()
    {
        if (containsDice)
        {
            diceImage = transform.GetChild(0).GetComponent<RectTransform>();

        }
        dHolder = DiceHolder.Instance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Box Index: {boxIndex}");
        hovering = true;
        

        dHolder.hovered = boxIndex;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        dHolder.hovered = -1;

    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!containsDice)
        {
            return;
        }

        //Debug.Log($"x: {eventData.delta.x} y: {eventData.delta.y}");

        if (hovering || holding)
        {
            float rotX = eventData.delta.y * rotationSpeed;
            float rotY = eventData.delta.x * rotationSpeed;
            float rotZ = eventData.delta.x * rotationSpeed;
            diceTF.Rotate(rotX, rotY, rotZ, Space.World);
        }


    }

    void Update()
    {
        if (holding)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(diceImage.parent as RectTransform, Input.mousePosition, null, out pos);
            diceImage.anchoredPosition = pos;  
        }
    }

    public void Click()
    {
        if (!hovering && !holding)
        {
            return;
        }

        if (holding)
        {
            holding = false;
            dHolder.holding = -1;
            int hoveredIndex = dHolder.hovered;
            if (hoveredIndex != boxIndex && hoveredIndex != -1) //means its a different box
            {
                Debug.Log("test");
                dHolder.CheckAvailability(boxIndex, hoveredIndex);
            }
            else
            {
                StartCoroutine(Return(diceImage.anchoredPosition));
            }
        }
        else
        {
            holding = true;
            dHolder.holding = boxIndex;

            
        }
    }

    IEnumerator Return(Vector2 startPos)
    {
        float elapsed = 0f;
        while (elapsed < 0.25f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.25f;

            diceImage.anchoredPosition = Vector2.Lerp(startPos, Vector2.zero, t);
            diceTF.Rotate((-startPos.normalized) * 200f * Time.deltaTime);
            yield return null;
        }

        diceImage.anchoredPosition = Vector2.zero;
    }
    

}
