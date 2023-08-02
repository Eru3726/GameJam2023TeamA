using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TitleManager : MonoBehaviour
{
    [SerializeField] CanvasGroup titleNameText;
    [SerializeField] CanvasGroup startText;
    [SerializeField] CanvasGroup difficultyTexts;
    [SerializeField] string stageSceneName;
    bool sceneTransitionRights = false;
    bool enterState = false;

    enum State
    {
        Title,
        Difficulty,
    }
    State state = State.Title;

    private void Start()
    {
        enterState = true;
    }

    private void Update()
    {
        // シーン遷移する前に左クリックをしたら
        if (Input.GetMouseButton(0) && sceneTransitionRights == true) 
        {
            // ステージのシーンに移動する
            FadeManager.Instance.LoadScene(stageSceneName, 1);

            // シーン遷移を多重にできないようにする
            sceneTransitionRights = false;
        }

        switch(state)
        {
            case State.Title:
                if (enterState == true)
                {
                    StartCoroutine(titleDisplay());
                }

                // シーン遷移する前に左クリックをしたら
                if (Input.GetMouseButton(0) && sceneTransitionRights == true)
                {
                    // ステージのシーンに移動する
                    FadeManager.Instance.LoadScene(stageSceneName, 1);

                    // シーン遷移を多重にできないようにする
                    sceneTransitionRights = false;
                }
                break;

            case State.Difficulty:
                if (enterState == true)
                {

                }

                if (Input.GetMouseButton(0) && sceneTransitionRights == true)
                {
                    // ステージのシーンに移動する
                    FadeManager.Instance.LoadScene(stageSceneName, 1);

                    // シーン遷移を多重にできないようにする
                    sceneTransitionRights = false;
                }

                break;
        }
    }

    IEnumerator titleDisplay()
    {
        titleNameText.DOFade(1, 0.5f);
        yield return new WaitForSeconds(1);
        startText.DOFade(1, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        sceneTransitionRights = true;
    }

    void difficultyDisplay()
    {
        startText.gameObject.SetActive(false);


    }

    private void LateUpdate()
    {
        enterState = false;
    }
}
