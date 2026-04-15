using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    

    [Header("Pause Menu")]
    [SerializeField] private Animator pauseMenu;
    private bool isPaused;
    [Space]
    [Header("Settings Menu")]
    [SerializeField] private Animator settingsMenu;
    private bool isSettings;

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
            if (isSettings)
            {
                isSettings = false;
                settingsMenu.SetTrigger("unpause");
            }

            else if (isPaused)
            {
                Debug.Log("unpaused");
                Time.timeScale = 1f;
                isPaused = false;
                pauseMenu.SetTrigger("unpause");
            }
            
            else
            {
                Debug.Log("paused");
                isPaused = true;
                Debug.Log("affsdfsdsfsdf");
                pauseMenu.SetTrigger("pause");


            }
        }
    }

    public void ResumeGame()
    {

            Debug.Log("resume");
            Time.timeScale = 1f;
            isPaused = false;
            pauseMenu.SetTrigger("unpause");
    }

    public void OpenSettings()
    {
            settingsMenu.SetTrigger("pause");
            isSettings = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}