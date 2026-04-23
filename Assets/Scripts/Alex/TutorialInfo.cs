using UnityEngine;


[CreateAssetMenu(fileName = "Face", menuName = "Scriptable Objects/TutorialInfo")]
public class TutorialInfo : ScriptableObject
{
    [TextArea]public string Title;
    [TextArea]public string Description;
}
