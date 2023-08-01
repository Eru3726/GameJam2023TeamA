using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TitleManager : MonoBehaviour
{
    [SerializeField] CanvasGroup titleNameText;
    [SerializeField] CanvasGroup startText;
    [SerializeField] string stageSceneName;
    bool sceneTransitionRights = false;

    private void Start()
    {
        StartCoroutine(titleDisplay());
    }


    private void Update()
    {
        if (Input.GetMouseButton(0) && sceneTransitionRights == true) 
        {
            FadeManager.Instance.LoadScene(stageSceneName, 1);

            sceneTransitionRights = false;
        }
    }

    IEnumerator titleDisplay()
    {
        titleNameText.DOFade(1, 0.5f);
        yield return new WaitForSeconds(1);
        startText.DOFade(1, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        sceneTransitionRights = true;
    }

}
