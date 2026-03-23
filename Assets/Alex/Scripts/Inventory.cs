using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Inventory : MonoBehaviour
{
    [SerializeField] private RawImage inventoryImage;
    [SerializeField] private Camera inventoryCamera;
    [SerializeField] Transform inventoryWorldCenter;
    [SerializeField] private float clickRadius;
    [SerializeField] private LayerMask diceLayer; //should likely make this its own unique inventory one later


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

        Vector3 vpPos = new (x, y, 0f);
        Ray ray = inventoryCamera.ViewportPointToRay(vpPos);
        Plane plane = new (inventoryWorldCenter.up, inventoryWorldCenter.position);

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
        else
        {
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
    }

}
