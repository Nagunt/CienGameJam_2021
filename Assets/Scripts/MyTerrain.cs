using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MyTerrain : MonoBehaviour
{
    [SerializeField] private Collider2D col2D;
    [SerializeField] private Transform depthTransform;
    [SerializeField] private TilemapRenderer tileMapRenderer;

    private int depth = int.MaxValue;
    public int Depth
    {
        get
        {
            if (depth == int.MaxValue) depth = (int)depthTransform.position.y;
            return depth;
        }
    }

    public Collider2D GetCollider2D()
    {
        return col2D;
    }

    private void Start()
    {
        tileMapRenderer.sortingOrder = 1000 - Depth;
    }
}
