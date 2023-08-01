using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour
{
    // �ړ������̃e�L�X�g
    [SerializeField] Text distanceText;
    [SerializeField] string titleSceneName;
    [SerializeField] CanvasGroup buttons;

    RectTransform rectTransform;
    bool sceneTransitionRights = true;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // �{�^���������Ȃ��Ă��N���b�N�o���Ă��܂��̂Ŕ�A�N�e�B�u������
        buttons.gameObject.SetActive(false);

        StartCoroutine(panelDisplay(120));
    }

    // �ړ������̃e�L�X�g���X�V
    public void distanceTextUpdate(float value)
    {
        distanceText.DOCounter(0, (int)value, 1, true);
    }

    // �Q�[���I�[�o�[�p�l�����Ăяo��
    IEnumerator panelDisplay(float distance)
    {
        // �Q�[���I�[�o�[�p�l������ʂ̒��S�Ɉړ�������
        rectTransform.DOAnchorPosX(0, 1);
        yield return new WaitForSeconds(1);

        // �ړ������̃e�L�X�g���X�V
        distanceTextUpdate(distance);
        yield return new WaitForSeconds(1);

        // �{�^�����A�N�e�B�u��
        buttons.gameObject.SetActive(true);

        // �{�^���̌����ڂ��t�F�[�h�C������
        buttons.DOFade(1, 0.5f);
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