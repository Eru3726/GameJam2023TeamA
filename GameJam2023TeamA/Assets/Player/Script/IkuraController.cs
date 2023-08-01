using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkuraController : MonoBehaviour
{

[Tooltip("LeftButtonをアタッチ")]
    [SerializeField] Button leftButton;
[Tooltip("RightBrttonをアタッチ")]
    [SerializeField] Button rightButton;
    private int ShotAxisValue=0;

    public enum IkuraState
    {
        Axis,
        Shot,
        Move,
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        
    }

    public void ShotAxis(int axis)
    {
        ///<summary>左右ボタン用プログラム</summary>
        ///<param name="axis">1=右,-1=左</param>

        ShotAxisValue = axis;
        Button deleteButton = leftButton;
        Button goButton = rightButton;
        if (axis>0)
        {
            deleteButton = rightButton;
            goButton = leftButton;
        }
        deleteButton.gameObject.SetActive(false);
        goButton.interactable = false;
    }
}
