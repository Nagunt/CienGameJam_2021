using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MyUIManager_Stage : MonoBehaviour
{

    public static MyUIManager_Stage Instance { get; private set; } = null;
    [SerializeField] private Image image_Fade;
    [SerializeField] private GameObject ui_Pause;
    [SerializeField] private GameObject ui_Clear;
    [SerializeField] private GameObject[] ui_Health;
    [SerializeField] private Text ui_StageName;
    [SerializeField] private Text ui_StagePurpose;

    private void Start()
    {
        Tweener fadeTweener = image_Fade.DOColor(new Color(0, 0, 0, 0), 1f).
            OnStart(() =>
            {
                image_Fade.gameObject.SetActive(true);
                image_Fade.raycastTarget = false;
                image_Fade.color = new Color(0, 0, 0, 1);
            });
    }
    public void OnClick_Main()
    {
        Tweener fadeTweener = image_Fade.DOColor(new Color(0, 0, 0, 1), 1f).
            OnStart(() =>
            {
                image_Fade.gameObject.SetActive(true);
                image_Fade.raycastTarget = true;
                image_Fade.color = new Color(0, 0, 0, 0);
            }).
            OnComplete(() =>
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("MainScene");
            }).SetUpdate(true);
    }

    public void OnClick_Resume()
    {
        if (!MyGameManager.Instance.GameState)
        {
            MyGameManager.Instance.GameState = true;
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

    public void SetUI_Clear()
    {
        if (MyGameManager.Instance.GameState)
        {
            MyGameManager.Instance.GameState = false;
            Time.timeScale = 0;
            ui_Clear.SetActive(true);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MyGameManager.Instance.GameState)
            {
                MyGameManager.Instance.GameState = false;
                Time.timeScale = 0;
                ui_Pause.SetActive(true);
            }
        }
    }
}
