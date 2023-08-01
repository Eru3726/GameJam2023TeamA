using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveRock : MonoBehaviour
{
    [HideInInspector]
    public float moveRightBorder = 3f;

    [HideInInspector]
    public float moveLeftBorder = -3f;

    [HideInInspector]
    public float moveSpeed = 5f;

    private int index = 1;
    private Rigidbody rb;

    void Start()
    {
        index = 1;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (this.transform.position.x <= moveLeftBorder) index = 1;
        else if (this.transform.position.x >= moveRightBorder) index = -1;

        rb.velocity = Vector3.right * index * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;
        collision.gameObject.transform.parent = this.transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
