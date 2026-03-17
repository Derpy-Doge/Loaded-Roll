using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Face", menuName = "Scriptable Objects/Face")]
public class Face : ScriptableObject
{   
    public Texture2D Texture;

    public int pips, price;

    [TextArea]public string Description;

    public UnityEvent Effect;
}
