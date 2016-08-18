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

	private bool isRunning = false;
	private float currentSpawnTimer = 0.0f;
	private float currentIncomeTimer = 0.0f;

	private void Start()
	{
		if(visitorPrefab == null) throw new NullReferenceException();
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
	}

	private void Update()
	{
		if (isRunning)
		{
			UpdateGameValues();
			UpdateTimer();
		}
	}

	private void UpdateGameValues()
	{
		decreaseSpawnTime = (float)popularity / (float)maxPopularity;
		spawnTime = startSpawnTime * (float)Math.Exp(-decreaseSpawnTime * Math.Pow(time / 60.0f, 2.0f)) + minimalSpawnTime;

		currentIncomeBonus = Math.Max(0, popularity / incomePopularityDivider - 2);
	}

	private void UpdateTimer()
	{
		time += Time.deltaTime;
		currentSpawnTimer += Time.deltaTime;
		currentIncomeTimer += Time.deltaTime;

		if (currentSpawnTimer >= spawnTime)
		{
			SpawnVisitor();
			currentSpawnTimer = 0.0f;
		}

		if (currentIncomeTimer >= incomeTime)
		{
			totalIncome += income + currentIncomeBonus;
			currentIncomeTimer = 0.0f;
		}
	}

	private void SpawnVisitor()
	{
		var spawnRandom = UnityRandom.Range(0, 100);
		var spawnType = VisitorTypes.Cosplayer;
		foreach (var visitorDefinition in visitorDefinitions)
		{
			if (spawnRandom >= visitorDefinition.minSpawnRange && spawnRandom <= visitorDefinition.maxSpawnRange)
			{
				spawnType = visitorDefinition.type;
				break;
			}
		}

		var x = UnityRandom.Range(-10.0f, 10.0f);
		var go = Instantiate(visitorPrefab, new Vector3(x, -8.0f, 0.0f), new Quaternion()) as GameObject;
		go.GetComponent<VisitorController>().visitorType = spawnType;
		go.GetComponent<Renderer>().material.color = visitorDefinitions[(int)spawnType].color;
	}

	public void DecreasePopularity(VisitorTypes visitorType)
	{
		popularity -= visitorDefinitions[(int)visitorType].popularityValue;;
	}

	public void IncreasePopularity(VisitorTypes visitorType)
	{
		popularity += 1;
	}

	public void StartSession()
	{
		isRunning = true;
	}
}
