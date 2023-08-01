using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour
{
    // 移動距離のテキスト
    [SerializeField] Text distanceText;
    [SerializeField] string titleSceneName;
    [SerializeField] CanvasGroup buttons;

    RectTransform rectTransform;
    bool sceneTransitionRights = true;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // ボタンが見えなくてもクリック出来てしまうので非アクティブ化する
        buttons.gameObject.SetActive(false);

        StartCoroutine(panelDisplay(120));
    }

    // 移動距離のテキストを更新
    public void distanceTextUpdate(float value)
    {
        distanceText.DOCounter(0, (int)value, 1, true);
    }

    // ゲームオーバーパネルを呼び出す
    IEnumerator panelDisplay(float distance)
    {
        // ゲームオーバーパネルを画面の中心に移動させる
        rectTransform.DOAnchorPosX(0, 1);
        yield return new WaitForSeconds(1);

        // 移動距離のテキストを更新
        distanceTextUpdate(distance);
        yield return new WaitForSeconds(1);

        // ボタンをアクティブ化
        buttons.gameObject.SetActive(true);

        // ボタンの見た目をフェードインする
        buttons.DOFade(1, 0.5f);
    }

    // リトライする
    public void retryGame()
    {
        if (sceneTransitionRights == false) return;

        sceneTransitionRights = false;
        FadeManager.Instance.LoadScene(SceneManager.GetActiveScene().name, 1);
    }

    // タイトルに戻る
    public void backToTitle()
    {
        if (sceneTransitionRights == false) return;

        sceneTransitionRights = false;
        FadeManager.Instance.LoadScene(titleSceneName, 1);
    }
}
