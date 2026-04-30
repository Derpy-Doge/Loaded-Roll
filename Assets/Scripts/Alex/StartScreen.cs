using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SaveDataController.Instance.current.run.IsGamePlayed)
        {
            Debug.Log("sojgojsoghjosjg");
            continueButton.SetActive(true);
        }
    }

    public void SwapScenes(string name)
    {

        if (name == "123456789")
        {
            SaveDataController.Instance.current.run = new Run();
            SaveDataController.Instance.current.run.IsGamePlayed = true;
            SceneManager.LoadScene("TestWithInv");
            return;

        }


        SceneManager.LoadScene(name);
        
    }
}
