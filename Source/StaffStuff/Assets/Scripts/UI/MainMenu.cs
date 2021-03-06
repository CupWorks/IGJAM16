﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenu : Singleton<MainMenu>
{
    private List<CanvasRenderer> childRenderer = new List<CanvasRenderer>();
    public GameObject creditScreen;

    protected override void Init()
    {
        childRenderer.AddRange(this.transform.GetComponentsInChildren<CanvasRenderer>());
        if (GameSession.currentGameState == GameState.Intro)
        {
            for (int i = 0; i < childRenderer.Count; i++)
            {
                childRenderer[i].SetAlpha(0);
            }
        }
    }

    public void ShowMenu()
    {
        GameSession.currentGameState = GameState.Startmenu;
        for (int i = 0; i < childRenderer.Count; i++)
        {
            childRenderer[i].SetAlpha(1);
        }
    }

    public void StartGame()
    {
        GameSession.Instance.StartSession();
    }

    public void ReturnFromCredits()
    {
        GameSession.currentGameState = GameState.Startmenu;
		GameSession.Instance.DestroyCreditPuppets();
        gameObject.SetActive(true);
        creditScreen.SetActive(false);
    }

    public void ShowCredits()
    {
        GameSession.currentGameState = GameState.Credits;
		GameSession.Instance.SpawnCreditPuppets();
        gameObject.SetActive(false);
        creditScreen.SetActive(true);
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
