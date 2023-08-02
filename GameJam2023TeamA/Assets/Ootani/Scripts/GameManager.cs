using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameOverPanelManager gameOverPanel;
    [SerializeField] Transform canvas;
    [SerializeField] GameObject Enemy;

    [SerializeField]
    private GameObject player,GameUI;

    AudioSource audio;
    public AudioClip normalBGM;
    public AudioClip cautionBGM;

    public int addScore;
    public int score = 0;
    public int distance = 0;
    public float playerEnemyDistance;

    public float addScoreInterval;
    float addScoreTimer = 0;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        playerEnemyDistance = (player.transform.position.z - Enemy.transform.position.z);

        if (audio.clip != cautionBGM && playerEnemyDistance <= 20)
        {
            audio.clip = cautionBGM;
            audio.Play();
        }
        if (audio.clip != normalBGM && playerEnemyDistance > 20)
        {
            audio.clip = normalBGM;
            audio.Play();
        }
    }

    public void eatIkura()
    {
        gameOverPanel = Instantiate(gameOverPanel);
        gameOverPanel.transform.SetParent(canvas);
        gameOverPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        player.SetActive(false);
        GameUI.SetActive(false);

        gameOverPanel.setValue("’Ç‚¢•t‚©‚ê‚½", score, distance);
    }

    public void addScoreExe()
    {
        addScoreTimer += Time.deltaTime;

        if (addScoreTimer > addScoreInterval)
        {
            addScoreTimer = 0;

            score += addScore;
        }
    }
}
