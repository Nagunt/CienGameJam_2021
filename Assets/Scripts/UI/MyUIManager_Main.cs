using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;




public class MyUIManager_Main : MonoBehaviour
{
    [SerializeField] private Image image_Fade;
    public void OnClick_Start()
    {
        Tweener fadeTweener = image_Fade.DOColor(new Color(0, 0, 0, 1), 1f).
            OnStart(() =>
            {
                image_Fade.raycastTarget = true;
                image_Fade.color = new Color(0, 0, 0, 0);
            }).
            OnComplete(() =>
            {
                SceneManager.LoadScene("SelectScene");
            });
    }

    public void OnClick_Settings()
    {

    }

    public void OnClick_Exit()
    {
        Application.Quit();
    }
}
