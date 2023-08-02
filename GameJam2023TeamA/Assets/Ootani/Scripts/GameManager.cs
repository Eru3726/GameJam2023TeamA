using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameOverPanelManager gameOverPanel;
    [SerializeField] Transform canvas;

    int score = 1200;
    int distance=1111111;

    private void Awake()
    {

    }

    public void eatIkura()
    {

        gameOverPanel = Instantiate(gameOverPanel);
        gameOverPanel.transform.SetParent(canvas);
        gameOverPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        gameOverPanel.setValue("’Ç‚¢•t‚©‚ê‚½", score, distance);
    }
}
