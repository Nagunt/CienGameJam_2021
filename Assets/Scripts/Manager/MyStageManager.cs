using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyStageManager : MonoBehaviour
{
    public static MyStageManager Instance { get; private set; } = null;

    [SerializeField]
    private Transform terrain;
    public List<MyTerrain> terrainInfo;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        List<MyTerrain> newInfo = new List<MyTerrain>(terrain.childCount);
        for(int i = 0; i < terrain.childCount; ++i)
        {
            newInfo.Add(terrain.GetChild(i).GetComponent<MyTerrain>());
        }
        terrainInfo = newInfo.OrderBy(x => x.Depth).ToList();
    }
}
