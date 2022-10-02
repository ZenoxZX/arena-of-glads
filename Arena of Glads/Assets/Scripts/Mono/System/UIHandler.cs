using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;
    [SerializeField] TitlePanel titlePanel;

    private void Awake()
    {
        #region Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    public void SetTitlePanel(bool value, MonoBehaviour mono = null, float delay = 0) => titlePanel.SetLoadPanel(value, mono, delay);
    public void SetTitlePanelSlider(float value) => titlePanel.UpdateSlider(value);


}

[Serializable]
public class TitlePanel
{
    [SerializeField] GameObject panel;
    [SerializeField] Slider progressSlider;

    public void UpdateSlider(float value) => progressSlider.value = value;

    public void SetLoadPanel(bool value, MonoBehaviour mono = null, float delay = 0)
    {
        if (delay > 0) mono.StartCoroutine(SetLoadPanelIE());
        else panel.SetActive(value);

        IEnumerator SetLoadPanelIE()
        {
            yield return HelpersStatic.GetWaitForSeconds(delay);
            panel.SetActive(value);
        }
    }
}
