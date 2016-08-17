using System;
using UnityEngine;

public class GameSession : Singleton<GameSession>
{
	public GameObject visitorPrefab;

	private float time;
	private float currentTimer;

	private void Start()
	{
		if(visitorPrefab == null) throw new NullReferenceException();
	}

	private void Update()
	{
		time += Time.time;
		currentTimer += Time.deltaTime;

		if (currentTimer >= 5.0f)
		{
			var go = Instantiate(visitorPrefab);
			Debug.Log(go.name);
			currentTimer = 0.0f;
		}
	}
}
