using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField]private Animator pauseMenu;
    [SerializeField]private RectTransform pauseMenuMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (isPaused)
        //    {
        //        Time.timeScale = 1f;
        //        isPaused = false;
        //        pauseMenu.SetActive(false);

        //    }
        //    else
        //    {
        //        Time.timeScale = 0f;
        //        isPaused = true;
        //    }
        //}
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {

        if (ctx.performed)
        {
            Debug.Log("gugyugugugugugug");
            if (isPaused)
            {
                Debug.Log("unpaused");
                Time.timeScale = 1f;
                isPaused = false;
                pauseMenu.SetTrigger("unpaused");
                pauseMenuMenu.anchoredPosition = new Vector2(0, -1500);
            }
            else
            {
                Debug.Log("paused");
                isPaused = true;
                Debug.Log("affsdfsdsfsdf");
                pauseMenu.SetTrigger("paused");
                pauseMenuMenu.anchoredPosition = Vector2.zero;


            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

    }

}