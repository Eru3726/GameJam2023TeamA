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
        // �V�[���J�ڂ���O�ɍ��N���b�N��������
        if (Input.GetMouseButton(0) && sceneTransitionRights == true) 
        {
            // �X�e�[�W�̃V�[���Ɉړ�����
            FadeManager.Instance.LoadScene(stageSceneName, 1);

            // �V�[���J�ڂ𑽏d�ɂł��Ȃ��悤�ɂ���
            sceneTransitionRights = false;
        }

        switch(state)
        {
            case State.Title:
                if (enterState == true)
                {
                    StartCoroutine(titleDisplay());
                }

                // �V�[���J�ڂ���O�ɍ��N���b�N��������
                if (Input.GetMouseButton(0) && sceneTransitionRights == true)
                {
                    // �X�e�[�W�̃V�[���Ɉړ�����
                    FadeManager.Instance.LoadScene(stageSceneName, 1);

                    // �V�[���J�ڂ𑽏d�ɂł��Ȃ��悤�ɂ���
                    sceneTransitionRights = false;
                }
                break;

            case State.Difficulty:
                if (enterState == true)
                {

                }

                if (Input.GetMouseButton(0) && sceneTransitionRights == true)
                {
                    // �X�e�[�W�̃V�[���Ɉړ�����
                    FadeManager.Instance.LoadScene(stageSceneName, 1);

                    // �V�[���J�ڂ𑽏d�ɂł��Ȃ��悤�ɂ���
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