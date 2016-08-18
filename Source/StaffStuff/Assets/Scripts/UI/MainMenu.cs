using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Singleton<MainMenu>
{
	public void StartGame()
    {
        GameSession.Instance.StartSession();
    }

    public void ShowPauseMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void UnpauseMenu()
    {
        this.transform.GetChild(1).GetComponent<Button>().onClick.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
