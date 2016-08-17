using System;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class GameSession : Singleton<GameSession>
{
	public GameObject visitorPrefab;
	public int popularity = 10;
    public int maxPopularity = 100;
	public float income = 8.50f;
	public float incomeBonus = 1.0f;
	public int incomePopularityDivider = 5;
	public float currentIncomeBonus = 0.0f;
	public float totalIncome = 0.0f;
	public float incomeTime = 10.0f;
	public float time = 0.0f;
	public float maxTime = 300.0f;
	public float spawnTime = 1.0f;
	public float startSpawnTime = 3.0f;
	public float minimalSpawnTime = 0.25f;
	public float decreaseSpawnTime = -0.25f;

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
		time += Time.deltaTime;
		currentSpawnTimer += Time.deltaTime;
		currentIncomeTimer += Time.deltaTime;

		decreaseSpawnTime = (float)popularity / (float)maxPopularity;
		spawnTime = startSpawnTime * (float)Math.Exp(-decreaseSpawnTime * Math.Pow(time / 60.0f, 2.0f)) + minimalSpawnTime;
		if (currentSpawnTimer >= spawnTime)
		{
			var x = UnityRandom.Range(-10.0f, 10.0f);
			Instantiate(visitorPrefab, new Vector3(x, -8.0f, 0.0f), new Quaternion());
			currentSpawnTimer = 0.0f;
		}

		currentIncomeBonus = Math.Max(0, popularity / incomePopularityDivider - 2);
		if (currentIncomeTimer >= incomeTime)
		{
			totalIncome += income + currentIncomeBonus;
			currentIncomeTimer = 0.0f;
		}
	}

	public void DecreasePopularity(VisitorTypes visitorType)
	{
		popularity -= visitorDefinitions[(int)visitorType].popularityValue;;
	}

	public void IncreasePopularity(VisitorTypes visitorType)
	{
		popularity += 1;
	}
}
