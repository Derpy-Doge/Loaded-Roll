using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private RectTransform NewGameOptions;
    [SerializeField] private GameObject ButtonParent;
    [HideInInspector] public List<Vector2> buttonAnchoredPositions = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SaveDataController.Instance.current.run.IsGamePlayed)
        {
            Debug.Log("sojgojsoghjosjg");
            continueButton.SetActive(true);
        }

        foreach (Transform button in ButtonParent.transform)
        {
            RectTransform rect = button.GetComponent<RectTransform>();
            buttonAnchoredPositions.Add(rect.anchoredPosition);
        }
    }

    public void SwapScenes(string name)
    {

        if (name == "123456789")
        {
            foreach (Transform button in ButtonParent.transform)
            {
                StartCoroutine(Fall(button));
            }
            AceDieVisual.Instance.UpdateTextBoxes();
            StartCoroutine(ICANTNAMETHINGSPLEASEHELP(NewGameOptions, new(-200f, 0f), 90f));
            //SceneManager.LoadScene("TestWithInv");
            return;

        }

        else if (name == "987654321")
        {
            if (!AceDieVisual.Instance.CanContinue)
            {
                return;
            }

            SaveDataController.Instance.current.run = new Run();
            SaveDataController.Instance.current.run.IsGamePlayed = true;
            SaveDataController.Instance.current.run.AceDie = (int)AceDieVisual.Instance.currentAceDie;
            SceneManager.LoadScene("TestWithInv");
        }


        SceneManager.LoadScene(name);

    }
    public void Falls(Transform button)
    {
        StartCoroutine(Fall(button, false));
    }
    public IEnumerator Fall(Transform button)
    {
        RectTransform rect = button as RectTransform;
        if (rect == null || !rect is RectTransform)
        {
            yield break;
        }
        float fallSpeed = Random.Range(1250f, 1500f);
        float dir = Random.value > 0.5f ? 1f : -1f;
        float horizontalSpeed = Random.Range(50f, 200f) * dir;
        float swayOffset = Random.Range(0f, Mathf.PI * 2f);

        while (rect.anchoredPosition.y > -1100)
        {
            float sway = Mathf.Sin(Time.time * 2f + swayOffset) * 30f;
            Vector2 movement = new Vector2(horizontalSpeed + sway, -fallSpeed) * Time.deltaTime;
            rect.anchoredPosition += movement;
            rect.Rotate(0, 0, dir * Random.Range(80f, 125f) * Time.deltaTime);
            yield return null;
        }
    }


    public IEnumerator Fall(Transform button, bool ignoreME)
    {
        RectTransform rect = button as RectTransform;
        if (rect == null || !rect is RectTransform)
        {
            yield break;
        }
        float fallSpeed = Random.Range(8250f, 8500f);
        float dir = Random.value > 0.5f ? 1f : -1f;
        float horizontalSpeed = Random.Range(50f, 200f) * dir;
        float swayOffset = Random.Range(0f, Mathf.PI * 2f);

        while (rect.anchoredPosition.y > -2000)
        {
            float sway = Mathf.Sin(Time.time * 2f + swayOffset) * 30f;
            Vector2 movement = new Vector2(horizontalSpeed + sway, -fallSpeed) * Time.deltaTime;
            rect.anchoredPosition += movement;
            rect.Rotate(0, 0, dir * Random.Range(80f, 125f) * Time.deltaTime);
            yield return null;
        }
    }

    public void Bouncing(RectTransform rect)
    { 
        Debug.Log("bouncing");
        StartCoroutine(ICANTNAMETHINGSPLEASEHELP(rect, new Vector2(0,0), 0f));
    }
    public IEnumerator ICANTNAMETHINGSPLEASEHELP(RectTransform rect, Vector2 finalPos, float angle)
    {
        float duration = 1f;

        RectTransform canvasRect = this.GetComponent<RectTransform>();
        float left = canvasRect.rect.xMin;
        float right = canvasRect.rect.xMax;
        float top = canvasRect.rect.yMax;

        float startX = Random.Range(left + 200, Mathf.Lerp(left, right, 0.6f)); //Change the screen width later to use canvas
        float startY = top + 300f;
        
        Vector2 start = new (startX, startY);

        Vector2 control = (start + finalPos) / 2 + new Vector2(Random.Range(-100f, 100f), Random.Range(150f, 400f));

        rect.anchoredPosition = start;

        float dir = Random.value > 0.5f ? 1f : -1f;
        float startRot = rect.eulerAngles.z;
        int spins = Random.Range(1, 3);
        float targetRot = 0f + 360f * spins * dir;

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            t = 1f - Mathf.Pow(1f - t, 2f);

            Vector2 pos = Mathf.Pow(1 - t, 2) * start + 2 * (1 - t) * t * control + t * t * finalPos;
            rect.anchoredPosition = pos;

            float rot = Mathf.Lerp(startRot, targetRot, t);
            rect.rotation = Quaternion.Euler(0, 0, rot);

            time += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = finalPos;
        rect.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}
