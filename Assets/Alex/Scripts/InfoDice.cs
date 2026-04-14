using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InfoDice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    private Canvas canvas;
    private bool _dragging;
    private bool _hovering;
    private RectTransform _infoImage;
    private RectTransform rect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = transform.parent.GetComponent<RectTransform>();
        canvas = transform.parent.GetComponent<Canvas>();
        _infoImage = GetComponent<RectTransform>();
    }

 

    public void Click(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_hovering)
            {
                _dragging = true;
            }
        }
        else if (ctx.canceled)
        {
            _dragging = false;

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hovering = false;
    }

    public void OnPointerMove(PointerEventData eventData) //use gsame movement in dice dragging
    {
    }

    void Update()
    {
        if (_dragging)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, Camera.main, out pos);
            _infoImage.anchoredPosition = pos - new Vector2(_infoImage.rect.width/2f, _infoImage.rect.height / 2f);
            Debug.Log("sojgjkzxogjsok");
        }
    }
}
