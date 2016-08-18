using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    private Highscore highscore;
    public GameObject highscoreEntryPrefab;
    public float firstEntryHeight = 420;

    //Called by Inputfield.OnEndEdit from HighscoreEntryPrompt
    public void EnterHighscoreEntry(string entryName)
    {
        highscore = Highscore.GetHighscore();
        highscore.AddNewHighscoreEntry(entryName, GameSession.Instance.totalIncome);
        CreateEntryObjects();
    }

    private void CreateEntryObjects()
    {
        for (int i = 0; i < highscore.highscoreEntries.Count; i++)
        {
            GameObject score = Instantiate(highscoreEntryPrefab);
            score.transform.SetParent(this.transform);
            RectTransform rectTrans = score.GetComponent<RectTransform>();
            rectTrans.localPosition = new Vector3(0, firstEntryHeight - rectTrans.rect.height * i, 0);
            rectTrans.localScale = Vector3.one;
            SetEntryValues(highscore.highscoreEntries[i], score);
        }
    }

    public void ShowHighscore()
    {
        highscore = Highscore.GetHighscore();
        CreateEntryObjects();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    private void SetEntryValues(HighscoreEntry entry, GameObject score)
    {
        score.transform.GetChild(0).GetComponentInChildren<Text>().text = entry.entryName;
        score.transform.GetChild(1).GetComponentInChildren<Text>().text = entry.totalPoints.ToString("0.00") + " €";
    }
}
