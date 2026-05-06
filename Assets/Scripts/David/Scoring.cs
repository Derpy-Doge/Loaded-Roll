using System.Collections;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text scoreText; //The text for the amount of points the player has.
    private GameObject addedPointsPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        addedPointsPrefab = Resources.Load<GameObject>("Prefabs/AddedPoints");
    }

    public void AddPoints(int points)
    { 
        Debug.Log("gshgosnogjsogjosj");
        StartCoroutine(AnimScore(points));//change this to the amount of points being added later
    }
    public IEnumerator AnimScore(int score)
    {
        GameObject paul = Instantiate(addedPointsPrefab, scoreText.transform);
        

        paul.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-100f,100f), Random.Range(-300f, 0f));
        
        
        TMPro.TMP_Text scoreTextCopy = paul.GetComponent<TMPro.TMP_Text>();
        paul.SetActive(true);
        scoreTextCopy.text = $"+{score}";
        while (scoreTextCopy.fontSize < 100)
        {
            scoreTextCopy.fontSize += 1;
        }
        yield return new WaitForSeconds(2f);
        while (scoreTextCopy.fontSize > 0)
        {
            scoreTextCopy.fontSize -= 1;
        }
        Destroy(paul);
    }
}
