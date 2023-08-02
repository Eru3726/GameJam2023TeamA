using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text distanceText;
    [SerializeField] Text finishScoreText;

    int score = PreserveValue.instance.score;
    int distance = PreserveValue.instance.distance;

    private void Start()
    {
        StartCoroutine(resultDisplay());
    }

    IEnumerator resultDisplay()
    {
        yield return new WaitForSeconds(1);
        scoreText.DOCounter(0, score, 1);
        yield return new WaitForSeconds(1);
        distanceText.DOCounter(0, distance, 1);
        yield return new WaitForSeconds(1);
        finishScoreText.DOCounter(0, score * distance, 1);

    }
}
