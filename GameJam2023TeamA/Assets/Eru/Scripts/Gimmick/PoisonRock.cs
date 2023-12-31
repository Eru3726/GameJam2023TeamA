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
            Debug.Log("よびださ");
            if (time > 0f) return;
            //ダメージ処理
            var hp = collision.gameObject.GetComponent<IkuraController>();
            hp.IkuraDamage(damage);
            time = damageInterval;
        }
    }
}
