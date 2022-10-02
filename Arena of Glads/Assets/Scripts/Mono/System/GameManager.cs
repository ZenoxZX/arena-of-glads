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
    public GameState SetGameState(GameState gameState) => this.gameState = gameState;
    public enum GameState { Starting, Playing, Paused, ConnectionError }
    #endregion

    public event Action OnGameStart, OnGamePause, OnGameResume, OnConnectionError, OnQuit;

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
}
