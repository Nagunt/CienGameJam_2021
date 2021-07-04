using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyGameManager : MonoBehaviour
{
    public static int Score = 0;
    public static int StageIndex = 0;
    public static int LastClearStageIndex = 0;
    public static MyGameManager Instance { get; private set; } = null;

    private Sequence timeSequence;

    [SerializeField]
    private GameObject[] stage;

    private int timeCount;
    public int TimeCount
    {
        get
        {
            return timeCount;
        }
        set
        {
            timeCount = value;
            MyUIManager_Stage.Instance.SetUI_Time(value);
            if (!timeSequence.IsActive())
            {
                timeSequence = DOTween.Sequence();
                timeSequence.
                    AppendInterval(1f).
                    AppendCallback(() => TimeCount--).
                    SetLoops(value);
            }
            if(timeCount <= 0)
            {
                timeSequence.Kill();
                MyUIManager_Stage.Instance.SetUI_GameOver();
                // 게임 오버
            }
        }
    }

    private int enemyCount;
    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }
        set
        {
            enemyCount = value;
            MyUIManager_Stage.Instance.SetUI_EnemyCount(value);
            if (enemyCount <= 0)
            {
                // 스테이지 클리어
                Score += (TimeCount > 150) ? 15000 : TimeCount * 100;
                LastClearStageIndex = StageIndex;
                MyUIManager_Stage.Instance.SetUI_Clear();


                
            }
        }
    }

    public bool GameState
    {
        get; set;
    }

    private void Awake()
    {
        Instance = this;

        if(0 < StageIndex && StageIndex <= stage.Length)
        {
            Instantiate(stage[StageIndex - 1], transform);
        }
    }

    private void Start()
    {
        GameState = true;
        Time.timeScale = 1;
    }
}
