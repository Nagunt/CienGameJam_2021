using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MyUIManager_Select : MonoBehaviour
{
    [SerializeField] private Image image_Fade;
    private void Start()
    {
        Tweener fadeTweener = image_Fade.DOColor(new Color(0, 0, 0, 0), 1f).
            OnStart(() =>
            {
                image_Fade.raycastTarget = false;
                image_Fade.color = new Color(0, 0, 0, 1);
            });
    }


    public void OnClick_Stage(int buttonIndex)
    {
        MyStageManager.StageIndex = buttonIndex;
        SceneManager.LoadScene("StageScene");
    }
}
