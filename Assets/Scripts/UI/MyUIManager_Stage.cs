using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUIManager_Stage : MonoBehaviour
{
    public static MyUIManager_Stage Instance { get; private set; } = null;
    [SerializeField] private GameObject ui_Pause;
    [SerializeField] private GameObject[] ui_Health;
    [SerializeField] private Text ui_StageName;
    [SerializeField] private Text ui_StagePurpose;

    public void OnClick_Main()
    {

    }

    public void OnClick_Resume()
    {
        if (!MyStageManager.Instance.GameState)
        {
            MyStageManager.Instance.GameState = true;
            Time.timeScale = 1;
            ui_Pause.SetActive(false);
        }
    }

    public void SetUI_Health(int _value)
    {
        if(0 <= _value && _value <= ui_Health.Length)
        {
            for(int i = 0; i < _value - 1 && i < ui_Health.Length; ++i)
            {
                ui_Health[i].SetActive(true);
            }
            for(int i = _value; i < ui_Health.Length; ++i)
            {
                ui_Health[i].SetActive(false);
            }
        }
    }

    public void SetUI_StageInfo(string _name, string _content)
    {
        ui_StageName.text = _name;
        ui_StagePurpose.text = _content;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MyStageManager.Instance.GameState)
            {
                MyStageManager.Instance.GameState = false;
                Time.timeScale = 0;
                ui_Pause.SetActive(true);
            }
        }
    }
}
