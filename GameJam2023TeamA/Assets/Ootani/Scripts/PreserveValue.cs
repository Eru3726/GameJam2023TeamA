using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveValue : MonoBehaviour
{
    static public PreserveValue instance;

    public int score;
    public int distance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
