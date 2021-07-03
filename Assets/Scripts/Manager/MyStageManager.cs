using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyStageManager : MonoBehaviour
{
    public static int StageIndex;
    public static MyStageManager Instance { get; private set; } = null;

    [SerializeField]
    private Transform terrain;
    [SerializeField]
    private Transform movingPlatform;
    public List<MyTerrain> terrainInfo;
    public List<MyMovingPlatform> movingPlatforms;

    public bool GameState
    {
        get; set;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.Log(StageIndex + " 스테이지 시작");
        GameState = true;
        // 여기서 StageIndex 따라서 다른 Prefab 로드 하면 될듯.
        List<MyTerrain> newInfo = new List<MyTerrain>(terrain.childCount);
        for(int i = 0; i < terrain.childCount; ++i)
        {
            newInfo.Add(terrain.GetChild(i).GetComponent<MyTerrain>());
        }
        terrainInfo = newInfo.OrderBy(x => x.Depth).ToList();

        List<MyMovingPlatform> newPlatformInfo = new List<MyMovingPlatform>(movingPlatform.childCount);
        for (int i = 0; i < movingPlatform.childCount; ++i)
        {
            newPlatformInfo.Add(movingPlatform.GetChild(i).GetComponent<MyMovingPlatform>());
        }
        movingPlatforms = newPlatformInfo;
    }
}
