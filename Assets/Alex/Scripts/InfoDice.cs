using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InfoDice : MonoBehaviour
{
    private Canvas canvas;
    private bool _dragging;
    private RectTransform _infoImage;
    private RectTransform rect;
    [SerializeField] private RawImage[] faces = new RawImage[6];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = transform.parent.GetComponent<RectTransform>();
        canvas = transform.parent.GetComponent<Canvas>();
        _infoImage = GetComponent<RectTransform>();
    }

    public void RightClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            
            if (DiceHolder.Instance.hoveredSlot != null && DiceHolder.Instance.hoveredSlot.currentDice != null)
            {
                GlobalDie die = DiceHolder.Instance.hoveredSlot.currentDice.visualFC.Dice;
                gameObject.SetActive(true);
                _dragging = true;
                int i = 0;
                foreach (var face in die.Faces.Values)
                {
                    faces[i].texture = face.Texture;
                    i++;
                }
                //_infoImage.anchoredPosition = DiceHolder.Instance.hoveredSlot.GetComponent<RectTransform>().anchoredPosition + new Vector2(0f, 100f);
            }
        }
        else if (ctx.canceled)
        {
            if (_dragging)
            {
                gameObject.SetActive(false);
                _dragging = false;
                _dragging = false;
            }
            
        }

        

    }


    void Update()
    {
        // if (_dragging)
        // {
        //     Vector2 pos;
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, Camera.main, out pos);
        //     _infoImage.anchoredPosition = pos - new Vector2(_infoImage.rect.width/2f, _infoImage.rect.height / 2f);
        //     Debug.Log("sojgjkzxogjsok");
        // }
    }
}
