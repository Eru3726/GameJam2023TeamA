using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private GameObject player;
    [SerializeField] private float Startpos;
    [SerializeField] private float Goalpos;
    [SerializeField] private GameObject Goal;
    void Start()
    {
        progressSlider.value = 0;
        if (Goal)
        {
            Goalpos = Goal.transform.position.z;
        }
        Startpos = player.transform.position.z;
    }

    void Update()
    {
        float perMax = Goalpos - Startpos;
        float progressdis = perMax - (Goalpos - player.transform.position.z);
        progressSlider.value = progressdis / perMax;
    }
}
