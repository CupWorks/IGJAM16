using System;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class GameSession : Singleton<GameSession>
{
	public GameObject visitorPrefab;
	public uint popularity = 10;
    public uint maxPopularity = 100;
	public float income = 8.50f;
	public float totalIncome = 0.0f;
	public float spawnTime = 1.0f;
	public float incomeTime = 10.0f;
	public float time = 0.0f;
	public float maxTime = 300.0f;

	public VisitorDefinition[] visitorDefinitions = new VisitorDefinition[5];

	private float currentSpawnTimer;
	private float currentIncomeTimer;

	private void Start()
	{
		if(visitorPrefab == null) throw new NullReferenceException();
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
	}

	private void Update()
	{
		time += Time.time;
		currentSpawnTimer += Time.deltaTime;
		currentIncomeTimer += Time.deltaTime;

		if (currentSpawnTimer >= spawnTime)
		{
			var x = UnityRandom.Range(-10.0f, 10.0f);
			Instantiate(visitorPrefab, new Vector3(x, -8.0f, 0.0f), new Quaternion());
			currentSpawnTimer = 0.0f;
		}
		if (currentIncomeTimer >= incomeTime)
		{
			totalIncome += income;
			currentIncomeTimer = 0.0f;
		}
	}

	public void DecreasePopularity(VisitorTypes visitorType)
	{
		popularity -= 1;
		Debug.Log(popularity);
	}

	public void IncreasePopularity(VisitorTypes visitorType)
	{
		popularity += 1;
		Debug.Log(popularity);
	}
}
