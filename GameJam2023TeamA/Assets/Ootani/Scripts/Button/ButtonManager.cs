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

    // マウスカーソルがボタンと重なると
    public void onPointer()
    {
        // ボタンを少し大きくする
        rectTransform.DOScale(originalScale * 1.2f, 0.5f);

        if (onPointerAction != null)
        {
            onPointerAction();
        }
    }

    // マウスカーソルがボタンから離れると
    public void offPointer()
    {
        // ボタンを元の大きさにする
        rectTransform.DOScale(originalScale, 0.5f);

        if (offPointerAction != null)
        {
            offPointerAction();
        }

    }
}
