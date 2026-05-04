using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    

    [Header("Pause Menu")]
    [SerializeField] private Animator pauseMenu;
    private bool isPaused;
    [Space]
    [Header("Settings Menu")]
    [SerializeField] private Animator settingsMenu;
    [SerializeField] private GameObject settingsMenuObject;
    [SerializeField]private bool isSettings;
    [Space]
    [Header("Audio Clips")]
    [SerializeField]private Settings settings;


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

    public void EndRun()
    { 
        SaveDataController.Instance.current.run = new Run();
        SceneManager.LoadScene("StartMenu");
    }
    public void PauseGame(InputAction.CallbackContext ctx)
    {

        if (ctx.performed)
        {
            Debug.Log("gugyugugugugugug");
            if (isSettings)
            {
                isSettings = false;
                settingsMenu.SetTrigger("erase");
                pauseMenu.SetTrigger("unerase");
            }

            else if (isPaused)
            {
                Debug.Log("unpaused");
                Time.timeScale = 1f;
                isPaused = false;  
                //settingsMenu.SetTrigger("unpause");
                pauseMenu.SetTrigger("unpause");
            }
            
            else
            {
                Debug.Log("paused");
                isPaused = true;
                Debug.Log("affsdfsdsfsdf");
                //settingsMenu.SetTrigger("pause");
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
        if (!isSettings)
        {
            isSettings = true;
            pauseMenu.SetTrigger("erase");
            settingsMenuObject.SetActive(true);
            settingsMenu.SetTrigger("unerase");
        }
        else
        {
            isSettings = false;
            settingsMenu.SetTrigger("erase");
            pauseMenu.SetTrigger("unerase");
        }
    }

   
    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Erase()
    {
        Debug.Log("Erase");
        settings.sfx1.clip = settings.Eraser;
        settings.sfx1.Play();
    }
    public void Unerase()
    { 
        Debug.Log("unerase");
        settings.sfx1.clip = settings.Marker;
        settings.sfx1.Play();
    }
}