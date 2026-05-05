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
        noChargesGlow = Resources.Load<Material>("Materials/AReject");
        glow = Resources.Load<Material>("Materials/AceGlow");
        aceDie = (AceDieVisual.AceDie) SaveDataController.Instance.current.run.AceDie;

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

        if (charges > 0)
        {
            hovered = true;
            dieImage.material = glow;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
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
