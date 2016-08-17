using System;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

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

		if (currentTimer >= 0.5f)
		{
			var x = UnityRandom.Range(-10.0f, 10.0f);
			var go = Instantiate(visitorPrefab, new Vector3(x, -8.0f, 0.0f), new Quaternion());
			currentTimer = 0.0f;
		}
	}
}
