using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    public const int LobyScene = 0;
    public const int GameScene = 1;

    public static event Action OnSceneChanged, OnGameSceneLoaded, OnLobySceneLoaded;

    private void Awake()
    {
        #region Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    public void LoadLobySceneAsync() => LoadFunc(LobyScene, OnLobySceneLoaded);
    public void LoadGameSceneAsync() => LoadFunc(GameScene, OnGameSceneLoaded);
    public void LoadFunc(int sceneIndex, Action onLoad = null)
    {
        StartCoroutine(LoadGameSceneAsyncIE());

        IEnumerator LoadGameSceneAsyncIE()
        {
            yield return null;
            UIHandler.instance.SetTitlePanel(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                UIHandler.instance.SetTitlePanelSlider(operation.progress / .9f);

                Debug.Log(operation.progress);
                yield return null;
            }

            OnSceneChanged?.Invoke();
            onLoad?.Invoke();
            UIHandler.instance.SetTitlePanel(false, this, 0.5f);
            yield return null;
        }
    }
}
