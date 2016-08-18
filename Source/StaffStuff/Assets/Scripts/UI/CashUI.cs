using UnityEngine;
using UnityEngine.UI;

public class CashUI : MonoBehaviour
{
    private Text earnText;

    private void Start()
    {
        earnText = GetComponent<Text>();
        GameSession.Instance.OnGameEnd += () => { this.gameObject.SetActive(false); };
	}
	
	private void Update ()
    {
        earnText.text = GameSession.Instance.totalIncome.ToString("0.00") + " €";
	}
}
