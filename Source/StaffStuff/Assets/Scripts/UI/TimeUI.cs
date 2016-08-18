using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Text timeText;
    private GameSession sessionInstance;

	private void Start ()
    {
        timeText = GetComponentInChildren<Text>();
        sessionInstance = GameSession.Instance;
        sessionInstance.OnGameEnd += () => { this.gameObject.SetActive(false); };
	}
	
    private void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds(sessionInstance.maxTime - sessionInstance.time);

        timeText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }
}
