using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectDice : MonoBehaviour
{
    private RawImage RollArea;
    [SerializeField] private Camera RollCamera;
    [SerializeField] private Transform rollCenter;
    [SerializeField] private LayerMask diceLayer;
    [SerializeField] private float clickRadius = 2.5f;
    private Vector3 positiondgkdsjgosdj;


    private void Start()
    {
        RollArea = GetComponent<RawImage>();
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

        RectTransform rectTransform = RollArea.rectTransform;

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
        Ray ray = RollCamera.ViewportPointToRay(vpPos);
        Plane plane = new(rollCenter.up, rollCenter.position);

        if (plane.Raycast(ray, out float distance))
        {
            worldPos = ray.GetPoint(distance);
            return true;
        }

        return false;
    }


    public void Click(InputAction.CallbackContext ctx)
    {

        if (!ctx.performed || GameManager.Instance.CurrentState != GameManager.GameStates.Select)
        {
            return;
        }

        //Debug.Log("test686686");

        if (TryGetPosition(out Vector3 pos))
        {
            Debug.Log("test686686");
            Debug.Log(pos);
            Collider[] hits = Physics.OverlapSphere(pos, clickRadius);
            float minDist = hits.Min(hit => Vector3.Distance(pos, hit.transform.position));
            Collider closest = hits.First(hit => Vector3.Distance(pos, hit.transform.position) == minDist);
            
            if (closest != null) //if you clicked on an object in the inventory
            {
                closest.gameObject.SetActive(false);
                Debug.Log("test");
            }
        }


    }
}
