using UnityEngine;

public class PoisonRock : MonoBehaviour
{
    [HideInInspector]
    public int damage = 1;

    [HideInInspector]
    public float damageInterval = 0.1f;

    private float time = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (time < 0f) time -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (time > 0f) return;
            //ƒ_ƒ[ƒWˆ—

            time = damageInterval;
        }
    }
}
