using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Game State
    [SerializeField] GameState gameState;
    public GameState GetGameState() => gameState;
    public GameState SetGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Starting: OnGameStart?.Invoke(); break;
            case GameState.Paused:   OnGamePause?.Invoke(); break;
        }
        return this.gameState = gameState;
    }

    public enum GameState { Starting, Playing, Paused }
    #endregion

    public event Action OnGameStart, OnGamePause, OnGameResume, OnQuit;

    private void Awake()
    {
        #region Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    private void Start()
    {
        SceneChanger.instance.LoadGameSceneAsync();
    }

    private void OnApplicationPause(bool pause) => OnGamePause?.Invoke();
    private void OnApplicationQuit() => OnQuit?.Invoke();
}
