using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using System.Collections;




public class AceDie : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{


        
    private AceDieVisual.AceDie aceDie; //aceDie = (AceDieVisual.AceDie) SaveDataController.Instance.current.run.AceDie;
    private int charges;
    private RawImage dieImage;
    [SerializeField] private Transform aceDieTransform;
    [SerializeField] private TMPro.TMP_Text ChargesText;
    [SerializeField] private RectTransform chargesBackground;
    private Coroutine chargesCoroutine;
    private Material glow;
    private Material noChargesGlow;
    private bool hovered;
    private GameObject phyAceDie;
    [SerializeField] private PhysicalAceDie physicalAceDie;

    void Start()
    {
        phyAceDie = physicalAceDie.gameObject;
        dieImage = GetComponent<RawImage>();
        charges = SaveDataController.Instance.current.run.AceDieCharges;
        SaveDataController.Instance.current.run.AceDieCharges = 55;
        noChargesGlow = Resources.Load<Material>("Materials/AReject");
        glow = Resources.Load<Material>("Materials/AceGlow");
        aceDie = (AceDieVisual.AceDie) SaveDataController.Instance.current.run.AceDie;
        ChargesText.text = $"Charges: {charges}"; 

        switch (aceDie)
        {
            case AceDieVisual.AceDie.Gamble:
                break;

            case AceDieVisual.AceDie.Horse:
                break;
        }
    }


     public void OnPointerEnter(PointerEventData eventData)
    {
        if (chargesCoroutine != null)
            StopCoroutine(chargesCoroutine);
        chargesCoroutine = StartCoroutine(ChargesAnim(true));

        if (charges > 0)
        {
            dieImage.material = glow;
            hovered = true;
        }
    }


    IEnumerator ChargesAnim(bool dir)
    {
        
        Vector2 startPos = dir ? new Vector2(0, -512f): new Vector2 (0, -412f);
        Vector2 targetPos = dir ? new Vector2 (0, -412f): new Vector2(0, -512f);
        float duration = 0.25f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            chargesBackground.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        chargesBackground.anchoredPosition = targetPos;


    } 

    public void OnPointerExit(PointerEventData eventData)
    {
        if (chargesCoroutine != null)
            StopCoroutine(chargesCoroutine);
        chargesCoroutine = StartCoroutine(ChargesAnim(false));
        if (dieImage.material == glow)
        {
            hovered = false;
            dieImage.material = null;
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        float rotX = eventData.delta.y; 
        float rotY = eventData.delta.x; 
        float rotZ = -eventData.delta.x; //change this later
        //aceDieTransform.Rotate(rotX, rotY, rotZ, Space.World); 

    }

    private IEnumerator ResetMaterial(UnityEngine.UI.MaskableGraphic image, float time)
    {
        yield return new WaitForSeconds(time);
        if (hovered)
        {
            image.material = glow;
            yield break;
        }

        image.material = null;
    }

    public void Click(InputAction.CallbackContext ctx)
    {

        if (!ctx.performed || !hovered)
        {
            return;
        }
        if (charges < 1)
        {
            dieImage.material = noChargesGlow;

            StartCoroutine(ResetMaterial(dieImage, .25f));
            return;
        }
        


        physicalAceDie.Roll();
        charges--;
        ChargesText.text = $"Charges: {charges}"; 

        Debug.Log("here");
        switch (aceDie)
        {
            case AceDieVisual.AceDie.Gamble:
                if (GameManager.Instance.CurrentState != GameManager.GameStates.Rolling)
                {
                    return;
                }
            

                break;

            case AceDieVisual.AceDie.Horse:
                break;
            
        }
    }

    




}
