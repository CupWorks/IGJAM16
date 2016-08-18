using UnityEngine;
using UnityEngine.UI;

public class PopularityUI : MonoBehaviour
{
    private GameSession sessionInstance;
    private Image popularityProgressbar;
    private float fillBarBase;
    private float progressBarHeight;
    private float incomePosX;
    private Text curIncome;

	private void Start ()
    {
        sessionInstance = GameSession.Instance;
        popularityProgressbar = this.transform.GetChild(1).GetComponent<Image>();
        progressBarHeight = popularityProgressbar.rectTransform.rect.height;
        fillBarBase = 0 - progressBarHeight / 2;
        curIncome = this.transform.GetComponentInChildren<Text>();
        incomePosX = curIncome.rectTransform.localPosition.x;
        sessionInstance.OnGameEnd += () => { gameObject.SetActive(false); };
	}
	
	private void Update ()
    {
        float fill = (float)sessionInstance.popularity / sessionInstance.maxPopularity;
        popularityProgressbar.fillAmount = fill;
        curIncome.text = "+ " + sessionInstance.currentIncomeBonus.ToString("0.00") + " €/h";
        curIncome.rectTransform.localPosition = new Vector2(incomePosX, fillBarBase + progressBarHeight * fill);
	}
}
