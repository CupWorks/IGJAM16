using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreEntryPrompt : MonoBehaviour {

    public Text endgameMsg;
    public GameObject inputField;

	private void Start ()
    {
        GameSession.Instance.highscoreEntryPrompt = this;
	}

    public void SetEndgameMessage(string msg)
    {
        endgameMsg.gameObject.SetActive(true);
        this.endgameMsg.text = msg;
        inputField.SetActive(true);
    }
}
