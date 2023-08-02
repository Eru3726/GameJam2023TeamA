using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour
{
    // �ړ������̃e�L�X�g
    [SerializeField] RectTransform gameOverText;
    [SerializeField] Text causeOfDeathText;
    [SerializeField] CanvasGroup valueText;
    [SerializeField] Text distanceText;
    [SerializeField] Text scoreText;
    [SerializeField] string titleSceneName;
    [SerializeField] CanvasGroup buttons;

    string causeOfDeath;
    int distance;
    int score;

    bool sceneTransitionRights = true;
    bool enterState = false;

    enum State
    {
        GameOver,
        Select,
    }
    State state = State.GameOver;

    private void Start()
    {
        // �{�^���������Ȃ��Ă��N���b�N�o���Ă��܂��̂Ŕ�A�N�e�B�u������
        buttons.gameObject.SetActive(false);
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gameOverText.gameObject.SetActive(true);
        }

        switch(state)
        {
            case State.GameOver:
                if (enterState == true)
                {
                    enterState = false;

                    gameOverText.gameObject.SetActive(true);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    setValue("�ǂ��t���ꂽ", 1200, 111);
                    causeOfDeathText.text = causeOfDeath;
                    StartCoroutine(selectDisplay());
                    state = State.Select;
                }
                break;
        }
    }
    private void LateUpdate()
    {
        enterState = false;
    }

    IEnumerator selectDisplay()
    {
        gameOverText.DOAnchorPosY(380, 1);
        gameOverText.DOScale(gameOverText.localScale * 0.5f, 1);

        yield return new WaitForSeconds(1);
        causeOfDeathText.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        valueText.DOFade(1, 0.5f);

        yield return new WaitForSeconds(1);

        // �ړ������̃e�L�X�g���X�V
        valueTextUpdate();
        yield return new WaitForSeconds(1);

        // �{�^�����A�N�e�B�u��
        buttons.gameObject.SetActive(true);

        // �{�^���̌����ڂ��t�F�[�h�C������
        buttons.DOFade(1, 0.5f);
    }

    // ���l�̍X�V
    public void setValue(string death, int dis, int sco)
    {
        causeOfDeath = death;
        distance = dis;
        score = sco;
    }

    // ���l�̃e�L�X�g���X�V
    public void valueTextUpdate()
    {
        distanceText.DOCounter(0, distance, 1, true);
        scoreText.DOCounter(0, score, 1, true);
    }

    // ���g���C����
    public void retryGame()
    {
        if (sceneTransitionRights == false) return;

        sceneTransitionRights = false;
        FadeManager.Instance.LoadScene(SceneManager.GetActiveScene().name, 1);
    }

    // �^�C�g���ɖ߂�
    public void backToTitle()
    {
        if (sceneTransitionRights == false) return;

        sceneTransitionRights = false;
        FadeManager.Instance.LoadScene(titleSceneName, 1);
    }
}