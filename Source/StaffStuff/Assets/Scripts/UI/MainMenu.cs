using UnityEngine;

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
}
