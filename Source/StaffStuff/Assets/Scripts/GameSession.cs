using System;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using UnityEngine.SceneManagement;

public delegate void GameEndEventHandler();

public class GameSession : Singleton<GameSession>
{
	private const string winMsg = "Your Stall is the most popular of the Convention!";
	private const string loseMsg = "You're fired!";

	public GameObject visitorPrefab;
	public GameObject playerPrefab;
	public GameObject puppetPrefab;
	public GameObject introPrefab;
    [HideInInspector]
    public HighScoreEntryPrompt highscoreEntryPrompt;
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
	public VisitorSpriteDefinition playerOneSprites;
	public VisitorSpriteDefinition playerTwoSprites;

    public event GameEndEventHandler OnGameEnd = () => { };

	public static GameState currentGameState = GameState.Intro;
	private float currentSpawnTimer = 0.0f;
	private float currentIncomeTimer = 0.0f;

	private void Start()
	{
		if (visitorPrefab == null) throw new NullReferenceException();
		if (playerPrefab == null) throw new NullReferenceException();
		if (puppetPrefab == null) throw new NullReferenceException();
		if (introPrefab == null) throw new NullReferenceException();
		SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        OnGameEnd += PauseSession;
		if (currentGameState == GameState.Intro) {
			ShowInto ();
		}
	}

	private void Update()
	{
		if (IsRunning())
		{
			UpdateGameValues();
			UpdateTimer();
            UpdateVictoryConditions();
		}

        if (Input.GetButtonDown("Pause"))
        {
            if (IsRunning())
            {
                PauseSession();
                MainMenu.Instance.ShowPauseMenu();
            }
            else
            {
                MainMenu.Instance.UnpauseMenu();
            }
        }
    }

	private void UpdateGameValues()
	{
		decreaseSpawnTime = (float)popularity / (float)maxPopularity;
		spawnTime = startSpawnTime * Mathf.Exp(-decreaseSpawnTime * Mathf.Pow(time / 60.0f, 2.0f)) + minimalSpawnTime;

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

    private void UpdateVictoryConditions()
    {
        if (time >= maxTime || popularity <= 0)
        {
            OnGameEnd();
            highscoreEntryPrompt.SetEndgameMessage(popularity <= 0 ? loseMsg : winMsg);
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

		var x = UnityRandom.Range(-20.0f, 20.0f);
		var y = -8.0f;
		if (x < -15.0f || x > 15.0f)
		{
			UnityRandom.Range(-8.0f, -3.0f);
			x = Math.Max(-16.0f, x);
			x = Math.Min(16.0f, x);
		}
		var go = Instantiate(visitorPrefab, new Vector3(x, y, 10.0f), new Quaternion()) as GameObject;
		var goc = go.GetComponent<VisitorController>();
		goc.visitorType = spawnType;
		var spriteDefinitionIndex = UnityRandom.Range(0, visitorDefinitions[(int)spawnType].SpriteDefinitions.Length);
		var spriteDefinition = visitorDefinitions[(int)spawnType].SpriteDefinitions[spriteDefinitionIndex];
		goc.SetSpriteDefinition(spriteDefinition);
	}

	public void DecreasePopularity(VisitorTypes visitorType)
	{
		popularity -= visitorDefinitions[(int)visitorType].popularityValue;
	}

	public void IncreasePopularity(VisitorTypes visitorType)
	{
		popularity += visitorDefinitions[(int)visitorType].popularityValue;
	}

	public void ShowInto()
	{
		Instantiate(introPrefab);
	}

	public void StartSession()
	{
        if (currentGameState == GameState.Startmenu)
        {
			var p1 = Instantiate(playerPrefab, new Vector3(-8.0f, -2.0f, 0.0f), Quaternion.identity) as GameObject;
			var p1c = p1.GetComponent<PlayerController>();
            p1c.currentPlayer = Players.P1;
			p1c.SetSpriteDefinition(playerOneSprites);
			var p2 = Instantiate(playerPrefab, new Vector3(8.0f, -2.0f, 0.0f), Quaternion.identity) as GameObject;
			var p2c = p2.GetComponent<PlayerController>();
			p2c.currentPlayer = Players.P2;
			p2c.SetSpriteDefinition(playerTwoSprites);
        }
        currentGameState = GameState.Running;
        Time.timeScale = 1;
	}

	public void PauseSession()
	{
        currentGameState = GameState.Pausemenu;
        Time.timeScale = 0;
	}

	public bool IsRunning()
	{
        return currentGameState == GameState.Running;
	}

	private void SpawnCreditPuppets()
	{
		const float space = 1.75f;
		var sprites = GetAllCharacterSprits();
		var x = ((sprites.Count * space) / -2.0f) + (space / 2);
		foreach (var sprite in sprites)
		{
			var puppet = Instantiate(puppetPrefab, new Vector3(x, -2.0f, 0.0f), Quaternion.identity) as GameObject;
			puppet.GetComponent<SpriteRenderer>().sprite = sprite;
			x += space;
		}
	}

	private List<Sprite> GetAllCharacterSprits()
	{
		var list = new List<Sprite>();
		list.Add(playerOneSprites.down);
		foreach (var visitorDefinition in visitorDefinitions)
		{
			foreach (var sd in visitorDefinition.SpriteDefinitions)
			{
				list.Add(sd.down);
			}
		}
		list.Add(playerTwoSprites.down);

		return list;
	}
}
