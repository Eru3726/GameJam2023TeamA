using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("カメラの高さ")]
    [SerializeField] float CameraPrusY;
    [Tooltip("プレイヤーからどの程度離れるか")]
    [SerializeField] float CameraMinasZ;
    [Tooltip("どの程度傾けて映すか")]
    [SerializeField] float CameraEulerX;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.localEulerAngles = new Vector3(CameraEulerX, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float posX = player.transform.position.x;
        float posZ = player.transform.position.z;
        transform.position = new Vector3(posX, CameraPrusY, posZ + CameraMinasZ);
    }
}
