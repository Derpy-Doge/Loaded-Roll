using UnityEngine;
using UnityEngine.InputSystem;

public class SpeedUp : MonoBehaviour
{
    public InputAction Speedup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Speedup.triggered)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    private void OnEnable()
    {
         Speedup.Enable();
    }
}
