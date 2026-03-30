using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Face", menuName = "Scriptable Objects/Face")]
public class Face : ScriptableObject
{
    private DiceScoreCalc diceScoreCalc;

    public Texture2D Texture;

    public int pips, price;

    [TextArea]public string Description;

    public UnityEvent Effect;


    public void Reroll()
    {
        // Reroll this die
    }

    public void Cash(int profit)
    {
        diceScoreCalc.points += profit;
    }
    public void Choice()
    {
        // Get to choose a face on a different die
    }
}
