using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("�J�����̍���")]
    [SerializeField] float CameraPrusY;
    [Tooltip("�v���C���[����ǂ̒��x����邩")]
    [SerializeField] float CameraMinasZ;
    [Tooltip("�ǂ̒��x�X���ĉf����")]
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