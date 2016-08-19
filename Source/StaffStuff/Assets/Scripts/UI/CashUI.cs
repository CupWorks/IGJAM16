using UnityEngine;
using UnityEngine.UI;

public class CashUI : MonoBehaviour
{
    private Text earnText;
    private Image incomeProgressBar;
    private const float maxIncome = 500f;
    private float fillBarBase;
    private float progressBarHeight;
    private float incomePosX;
    private GameSession sessionInst;

    private void Start()
    {
        earnText = this.transform.GetChild(0).GetComponent<Text>();
        incomePosX = earnText.rectTransform.localPosition.x;
        incomeProgressBar = this.transform.GetChild(1).GetComponent<Image>();
        progressBarHeight = incomeProgressBar.rectTransform.rect.height;
        fillBarBase = 0 - progressBarHeight / 2;
        sessionInst = GameSession.Instance;
        sessionInst.OnGameEnd += () => { this.gameObject.SetActive(false); };
	}
	
	private void Update()
    {
        float fill = sessionInst.totalIncome / maxIncome;
        incomeProgressBar.fillAmount = fill;
        earnText.text = sessionInst.totalIncome.ToString("0.00") + " €";
        earnText.rectTransform.localPosition = new Vector3(incomePosX, fillBarBase + progressBarHeight * fill);
	}
}
