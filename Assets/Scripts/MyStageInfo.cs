using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStageInfo : MonoBehaviour
{
    [Header("- Stage Info")]
    [SerializeField] private string stageName;
    [SerializeField] private string stagePurpose;
    void Start()
    {
        MyUIManager_Stage.Instance.SetUI_StageInfo(stageName, stagePurpose);
    }
}
