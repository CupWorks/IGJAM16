using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    private Highscore highscore;
    public GameObject highscoreEntryPrefab;
    public float firstEntryHeight = 350;
    private float distBetweenEntries = 5;

    //Called by Inputfield.OnEndEdit from HighscoreEntryPrompt
    public void EnterHighscoreEntry(string entryName)
    {
        highscore = Highscore.GetHighscore();
        highscore.AddNewHighscoreEntry(entryName, GameSession.Instance.totalIncome);
        CreateEntryObjects();
    }

    private void CreateEntryObjects()
    {
        int amountOfEntries = Mathf.Min(highscore.highscoreEntries.Count, 10);
        for (int i = 0; i < amountOfEntries; i++)
        {
            GameObject score = Instantiate(highscoreEntryPrefab);
            score.transform.SetParent(this.transform);
            RectTransform rectTrans = score.GetComponent<RectTransform>();
            rectTrans.localPosition = new Vector3(0, firstEntryHeight - rectTrans.rect.height * i - distBetweenEntries * i, 0);
            rectTrans.localScale = Vector3.one;
            SetEntryValues(highscore.highscoreEntries[i], score);
        }
    }

    public void ShowHighscore()
    {
        highscore = Highscore.GetHighscore();
        CreateEntryObjects();
    }

    //Called by BackToMenu-OnHit()
    public void RestartGame()
    {
        GameSession.currentGameState = GameState.Startmenu;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    private void SetEntryValues(HighscoreEntry entry, GameObject score)
    {
        score.transform.GetChild(0).GetComponentInChildren<Text>().text = entry.entryName;
        score.transform.GetChild(1).GetComponentInChildren<Text>().text = entry.totalPoints.ToString("0.00") + " €";
    }
}
