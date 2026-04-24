using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour, IPointerEnterHandler
{

    public enum TutorialType
    {
        Inventory,
    }

    public static Tutorial TutorialPlayed; //Last opened tutorial
    private static List<TutorialType> tutorialTypes; //A list of all tutorials

    private static TMPro.TMP_Text title; 
    private static TMPro.TMP_Text description; 
    private static GameObject message; //The GO with the tutorialStuff 

    [SerializeField] private TutorialType tutorialType; //The type this tutorial is
    [SerializeField] private TutorialInfo info;

    private bool checkedSaveData = false;
    
    private bool openTutorial = false;



    public TutorialType GetTutorialType()
    {
        return tutorialType;
    }

    void Start()
    {
        if (tutorialTypes == null)
        {
            tutorialTypes = System.Enum.GetValues(typeof(TutorialType)).Cast<TutorialType>().ToList();
            this.LateStart(() => {foreach(var tType in SaveDataController.Instance.current.Settings.ClosedTutorials){tutorialTypes.Remove((TutorialType) tType);}}); //
            title = GameManager.Instance.tutorialTitle;
            description = GameManager.Instance.tutorialDescription;
            message = GameManager.Instance.tutorialMessage;

        }


        this.LateStart(() => {
            if (!SaveDataController.Instance.current.Settings.ShowTutorial || SaveDataController.Instance.current.Settings.ClosedTutorials.Contains((int) tutorialType))
            {
                this.enabled = false;
            }  
            else if (openTutorial)
            {
                gameObject.SendMessage(nameof(HandleInteration));
            }}); //Checks if tutorial is turned off in save data 
            checkedSaveData = true; 

    }

    private void CheckSaveData()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (checkedSaveData = false)
        {
            openTutorial = true;
            return;
        }

        HandleInteration();
    }

    public void HandleInteration()
    {
        if (tutorialTypes.Contains(tutorialType) && (TutorialPlayed == null || TutorialPlayed.GetTutorialType() != tutorialType)) //Checks that the tutorial message hasnt already been gone through by the player, and its not already spawned
        {
            TutorialPlayed = this;
            title.text = info.Title;
            description.text = info.Description;
            message.SetActive(true);
        }
    }

    public void DeleteTutorial()
    {
        SaveDataController.Instance.current.Settings.ClosedTutorials.Add((int) tutorialType);
        message.SetActive(false);
        tutorialTypes.Remove(tutorialType);
        TutorialPlayed = null;
        this.enabled = false;

    }
}
