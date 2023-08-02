using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [Tooltip("�ǂ�������X�s�[�h")]
    public float ChaseSpeed = 0;

    private Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = new Vector3(0, 0, ChaseSpeed*Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GameOver");    //�Q�[���I�[�o�[����
        }
    }
}