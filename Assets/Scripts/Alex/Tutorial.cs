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


    public TutorialType GetTutorialType()
    {
        return tutorialType;
    }

    void Start()
    {
        if (tutorialTypes == null)
        {
            tutorialTypes = System.Enum.GetValues(typeof(TutorialType)).Cast<TutorialType>().ToList();
            title = GameManager.Instance.tutorialTitle;
            description = GameManager.Instance.tutorialDescription;
            message = GameManager.Instance.tutorialMessage;

        }

        if (SaveDataController.Instance.current.Settings.ShowTutorial)
        {
            this.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
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
        message.SetActive(false);
        tutorialTypes.Remove(tutorialType);
        TutorialPlayed = null;
        this.enabled = false;

    }
}
