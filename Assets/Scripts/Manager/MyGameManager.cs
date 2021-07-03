using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static int StageIndex;
    public static MyGameManager Instance { get; private set; } = null;


    [SerializeField]
    private GameObject[] stage;


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
            if (enemyCount <= 0)
            {
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
