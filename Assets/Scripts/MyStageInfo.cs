using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStageInfo : MonoBehaviour
{
    [Header("- Stage Info")]
    [SerializeField] private string stageName;
    [SerializeField] private string stagePurpose;
    [SerializeField] private int timeCount;
    void Start()
    {
        MyGameManager.Instance.TimeCount = timeCount;
        MyUIManager_Stage.Instance.SetUI_StageInfo(stageName, stagePurpose);
    }
}
