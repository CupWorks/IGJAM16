using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Image timeOverlay;
    private GameSession sessionInstance;

	private void Start ()
    {
        timeOverlay = GetComponent<Image>();
        sessionInstance = GameSession.Instance;
        sessionInstance.OnGameEnd += () => { this.gameObject.SetActive(false); };
	}
	
    private void Update()
    {
        timeOverlay.fillAmount = sessionInstance.time / sessionInstance.maxTime;
    }
}
