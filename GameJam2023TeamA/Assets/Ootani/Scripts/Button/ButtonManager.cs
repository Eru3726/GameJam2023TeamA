using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    Vector3 originalScale;
    RectTransform rectTransform;

    public delegate void Action();

    public Action onPointerAction;
    public Action offPointerAction;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    // �}�E�X�J�[�\�����{�^���Əd�Ȃ��
    public void onPointer()
    {
        // �{�^���������傫������
        rectTransform.DOScale(originalScale * 1.2f, 0.5f);

        if (onPointerAction != null)
        {
            onPointerAction();
        }
    }

    // �}�E�X�J�[�\�����{�^�����痣����
    public void offPointer()
    {
        // �{�^�������̑傫���ɂ���
        rectTransform.DOScale(originalScale, 0.5f);

        if (offPointerAction != null)
        {
            offPointerAction();
        }

    }
}