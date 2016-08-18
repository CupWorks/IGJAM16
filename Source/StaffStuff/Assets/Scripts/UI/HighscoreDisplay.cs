using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    private Highscore highscore;
    public GameObject highscoreEntryPrefab;
    public float firstEntryHeight = 420;

    public void EnterHighscoreEntry(string entryName)
    {
        highscore = Highscore.GetHighscore();
        highscore.AddNewHighscoreEntry(entryName, GameSession.Instance.totalIncome);

        for (int i = 0; i < highscore.highscoreEntries.Count; i++)
        {
            GameObject score = Instantiate(highscoreEntryPrefab);
            score.transform.SetParent(this.transform);
            score.GetComponent<RectTransform>().localPosition = new Vector3(0, firstEntryHeight + 100 * i, 0);
            SetEntryValues(highscore.highscoreEntries[i], score);
        }
    }

    private void SetEntryValues(HighscoreEntry entry, GameObject score)
    {
        score.transform.GetChild(0).GetComponentInChildren<Text>().text = entry.entryName;
        score.transform.GetChild(1).GetComponentInChildren<Text>().text = entry.totalPoints.ToString("0.00") + " €";
    }
}
