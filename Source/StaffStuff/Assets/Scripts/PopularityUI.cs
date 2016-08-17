using UnityEngine;
using UnityEngine.UI;

public class PopularityUI : MonoBehaviour
{
    private GameSession sessionInstance;
    private Image popularityProgressbar;
    private Text curIncome;

	private void Start ()
    {
        sessionInstance = GameSession.Instance;
        popularityProgressbar = GetComponent<Image>();
        curIncome = GetComponentInChildren<Text>();
	}
	
	private void Update ()
    {
        popularityProgressbar.fillAmount = (float)sessionInstance.popularity / sessionInstance.maxPopularity;
        curIncome.text = "+ " + sessionInstance.currentIncomeBonus.ToString("0.00") + " €/h";
	}
}
