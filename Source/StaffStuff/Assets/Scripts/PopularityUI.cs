using UnityEngine;
using UnityEngine.UI;

public class PopularityUI : MonoBehaviour
{
    private GameSession sessionInstance;
    private Image popularityProgressbar;

	private void Start ()
    {
        sessionInstance = GameSession.Instance;
        popularityProgressbar = GetComponent<Image>();
	}
	
	private void Update ()
    {
        popularityProgressbar.fillAmount = (float)sessionInstance.popularity / sessionInstance.maxPopularity;
	}
}
