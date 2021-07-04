using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MyUIManager_Select : MonoBehaviour
{
    [SerializeField]
    private Sprite[] firstNode;
    [SerializeField]
    private Sprite[] secondNode;
    [SerializeField]
    private Sprite[] thirdNode;

    [SerializeField] private Image image_Fade;
    [SerializeField] private Image image_Background;
    [SerializeField] private Image image_Clear;
    [SerializeField] private GameObject[] nodeButtons;

    private Sequence mapSequence;
    private void Start()
    {
        Tweener fadeTweener = image_Fade.DOColor(new Color(0, 0, 0, 0), 0.5f).
            OnStart(() =>
            {
                image_Fade.gameObject.SetActive(true);
                image_Fade.raycastTarget = false;
                image_Fade.color = new Color(0, 0, 0, 1);
            });
        if (MyGameManager.LastClearStageIndex < 3)
        {
            int animationIndex = 0;
            for (int i = 0; i < MyGameManager.LastClearStageIndex; ++i)
            {
                nodeButtons[i].SetActive(true);
            }
            for (int i = MyGameManager.LastClearStageIndex; i < nodeButtons.Length; ++i)
            {
                nodeButtons[i].SetActive(false);
            }

            switch (MyGameManager.LastClearStageIndex)
            {
                case 0:
                    {
                        mapSequence = DOTween.Sequence();
                        mapSequence.
                            OnStart(() =>
                            {
                                image_Background.sprite = firstNode[0];
                                animationIndex = 0;
                            }).
                            AppendInterval(1f / (firstNode.Length - 1)).
                            AppendCallback(() =>
                            {
                                image_Background.sprite = firstNode[++animationIndex];
                            }).
                            OnComplete(() =>
                            {
                                nodeButtons[MyGameManager.LastClearStageIndex].SetActive(true);
                            }).
                            SetLoops(firstNode.Length - 1);
                        break;
                    }
                case 1:
                    {
                        mapSequence = DOTween.Sequence();
                        mapSequence.
                            OnStart(() =>
                            {
                                image_Background.sprite = secondNode[0];
                                animationIndex = 0;
                            }).
                            AppendInterval(1f / (secondNode.Length - 1)).
                            AppendCallback(() =>
                            {
                                image_Background.sprite = secondNode[++animationIndex];
                            }).
                            OnComplete(() =>
                            {
                                nodeButtons[MyGameManager.LastClearStageIndex].SetActive(true);
                            }).
                            SetLoops(secondNode.Length - 1);
                        break;
                    }
                case 2:
                    {
                        mapSequence = DOTween.Sequence();
                        mapSequence.
                            OnStart(() =>
                            {
                                image_Background.sprite = thirdNode[0];
                                animationIndex = 0;
                            }).
                            AppendInterval(1f / (thirdNode.Length - 1)).
                            AppendCallback(() =>
                            {
                                image_Background.sprite = thirdNode[++animationIndex];
                            }).
                            OnComplete(() =>
                            {
                                nodeButtons[MyGameManager.LastClearStageIndex].SetActive(true);
                            }).
                            SetLoops(thirdNode.Length - 1);
                        break;
                    }
            }
        }
        else
        {
            // 클리어 화면 띄우기
            image_Clear.gameObject.SetActive(true);
        }
    }


    public void OnClick_Stage(int buttonIndex)
    {
        MyGameManager.StageIndex = buttonIndex;
        Tweener fadeTweener = image_Fade.DOColor(new Color(0, 0, 0, 1), 1f).
            OnStart(() =>
            {
                if (mapSequence.IsActive())
                {
                    mapSequence.Kill();
                }
                image_Fade.gameObject.SetActive(true);
                image_Fade.raycastTarget = true;
                image_Fade.color = new Color(0, 0, 0, 0);
            }).
            OnComplete(() =>
            {
                SceneManager.LoadScene("StageScene");
            });
    }
}
