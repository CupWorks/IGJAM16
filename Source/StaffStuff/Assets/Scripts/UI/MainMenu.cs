using UnityEngine;

public class MainMenu : MonoBehaviour
{
	private void Start ()
    {
	
	}
	
	public void StartGame()
    {
        GameSession.Instance.StartSession();
    }
}
