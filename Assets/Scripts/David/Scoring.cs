using System.Collections;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text scoreText; //The text for the amount of points the player has.
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void AddPoints(int points)
    { 
        StartCoroutine(AnimScore(points));//change this to the amount of points being added later
    }
    public IEnumerator AnimScore(int score)
    {
        scoreText.gameObject.SetActive(true);
        scoreText.text = $"+{score}";
        while (scoreText.fontSize < 100)
        {
            scoreText.fontSize += 1;
        }
        yield return new WaitForSeconds(2f);
        while (scoreText.fontSize > 0)
        {
            scoreText.fontSize -= 1;
        }
        scoreText.gameObject.SetActive(false);
    }
}
