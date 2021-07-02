using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    [Header("- Physics")]
    [SerializeField] private Collider2D col2D;
    [SerializeField] private Rigidbody2D rb2D;


    private void FixedUpdate()
    {
        for(int i = 0; i < MyStageManager.Instance.terrainInfo.Count; ++i)
        {
            MyTerrain terrain = MyStageManager.Instance.terrainInfo[i];
            Physics2D.IgnoreCollision(col2D, terrain.GetCollider2D(), transform.position.y < terrain.Depth);
        }
    }
}
