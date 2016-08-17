using UnityEngine;

public class GameSession : Singleton<GameSession>
{
	private float time;
	private float currentTimer;

	private void Update()
	{
		time += Time.time;
		currentTimer += Time.deltaTime;

		if (currentTimer >= 5.0f)
		{
			Debug.Log("Spawn visitor");
			currentTimer = 0.0f;
		}
	}
}
