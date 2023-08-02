using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameOverPanelManager gameOverPanel;
    [SerializeField] Transform canvas;

    [SerializeField]
    private GameObject player,GameUI;

    int score = 1200;
    int distance=1111111;

    public void eatIkura()
    {
        gameOverPanel = Instantiate(gameOverPanel);
        gameOverPanel.transform.SetParent(canvas);
        gameOverPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        player.SetActive(false);
        GameUI.SetActive(false);

        gameOverPanel.setValue("’Ç‚¢•t‚©‚ê‚½", score, distance);
    }
}
