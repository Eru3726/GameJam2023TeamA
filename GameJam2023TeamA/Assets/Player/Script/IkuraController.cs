using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkuraController : MonoBehaviour
{
    Vector3 startPos;
[Tooltip("LeftButtonをアタッチ")]
    [SerializeField] Button leftButton;
[Tooltip("RightBrttonをアタッチ")]
    [SerializeField] Button rightButton;
    private int ShotAxisValue=0;

    [Tooltip("PowerBarをアタッチ")]
    [SerializeField] private Slider PowerBar;

    [Tooltip("最大発射パワーの設定")]
    [SerializeField][Min(0)] private float MaxShotPower;
    private float NowShotPower=0;
    public float RivarSpeed;

    [Tooltip("DamageBarをアタッチ")]
    [SerializeField] private Slider DamageBar;
    [Tooltip("HPの最大値をアタッチ")]
    [SerializeField][Min(1)]private float IkuraHP;
    private Vector3 OldPos;
    private Transform NearStone=null;
    public float stonea;

    private Rigidbody rb;
    private Animator animator;
    private int animeState=0;
    public enum IkuraState
    {
        None,
        Axis,
        Shot,
        Move,
        Wall,
    }
    private IkuraState NowIkuraState;
    void Start()
    {
        startPos = transform.position;
        NowIkuraState = IkuraState.Axis;
        DamageBar.maxValue=IkuraHP;
        DamageBar.value = IkuraHP;
        PowerBar.maxValue = MaxShotPower;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(NowIkuraState==IkuraState.Shot)
        {
            PowerChange();
            if(Input.GetMouseButtonDown(0))
            {
                ShotIkura();
            }
            if(Input.GetMouseButtonDown(1))
            {
                AxisStandby();
            }
        }
        if(NowIkuraState==IkuraState.Move)
        {
            rb.AddForce(0, 0,RivarSpeed/50);
        }
        if(NowIkuraState==IkuraState.Wall&&NearStone!=null)
        {
            Vector3 PLpos = transform.position;
            Vector3 NSpos = NearStone.transform.position;
            if(PLpos!=NSpos)
            {
                float moveX = NSpos.x - PLpos.x;
                moveX = Mathf.Clamp(moveX,-10,10);
                float moveZ = NSpos.z - PLpos.z;
                moveZ = Mathf.Clamp(moveZ, 0,20);
                rb.AddForce(moveX,0,moveZ);
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            IkuraHeel();
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            transform.position = startPos;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="Stone")
        {
            IkuraDamage();
            AxisStandby();
        }
        else
        {
            StartCoroutine(StoneSearch());
        }
    }

    private void AxisStandby()
    {
        rb.velocity = Vector3.zero;
        NowShotPower = 0;
        PowerBar.value = 0;
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        leftButton.interactable = true;
        rightButton.interactable = true;
        NowIkuraState = IkuraState.Axis;
    }

    public void ShotAxis(int axis)
    {
        ///<summary>左右ボタン用プログラム</summary>
        ///<param name="axis">1=右,-1=左</param>

        ShotAxisValue = axis;
        Button deleteButton = rightButton;
        Button goButton = leftButton;
        if (axis>0)
        {
            deleteButton = leftButton;
            goButton = rightButton;
        }
        deleteButton.gameObject.SetActive(false);
        goButton.interactable = false;
        NowIkuraState = IkuraState.Shot;
    }

    private void PowerChange()
    {
        NowShotPower += MaxShotPower / 100;
        if(NowShotPower>MaxShotPower){NowShotPower = 0;}
        PowerBar.value = NowShotPower;
    }

    private void ShotIkura()
    {
        NowIkuraState = IkuraState.Move;
        OldPos = transform.position;
        Vector3 ShotForce = new Vector3(ShotAxisValue*NowShotPower*1.5f, 0,0);
        rb.AddForce(ShotForce);
    }

    private void IkuraDamage()
    {
        Vector3 NewPosZ = transform.position;
        float damagePersent=Vector3.Distance( NewPosZ , OldPos);
        float damage = IkuraHP / 100 * damagePersent;
        DamageBar.value -= damage;
        if (DamageBar.value <= 0) Debug.Log("GameOver");
    }

    public void IkuraHeel()
    {
        float MaxHP = DamageBar.maxValue;
        IkuraHP = MaxHP;
        DamageBar.value = MaxHP;
    }

    private IEnumerator StoneSearch()
    {
        rb.velocity = Vector3.zero;
        float posZ = transform.position.z;

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Stone");
        if (targets.Length == 1) NearStone=targets[0].transform;
        else
        {
            NearStone= null;
            float NearZ = 1000f;
            foreach (GameObject target in targets)
            {
                // 前回計測したオブジェクトよりも近くにあれば記録
                float z = target.transform.position.z-stonea;
                if (z - posZ >= 0&&NearZ>z-posZ)
                {
                    NearStone = target.transform;
                    NearZ = z-posZ;
                }
            }
        }
        NowIkuraState = IkuraState.None;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(2f);
        NowIkuraState = IkuraState.Wall;
    }

    public void AnimationChange()
    {
        animeState++;
        Debug.Log("ここにアニメーション遷移を書く");
    }
}
