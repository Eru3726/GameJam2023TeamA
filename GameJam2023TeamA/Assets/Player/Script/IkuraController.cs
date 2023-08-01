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
    private float OldPosZ;

    private Rigidbody rb;
    public enum IkuraState
    {
        Axis,
        Shot,
        Move,
    }
    private IkuraState NowIkuraState;
    void Start()
    {
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
    }

    private void OnCollisionEnter(Collision col)
    {
        IkuraDamage();
        AxisStandby();
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
        OldPosZ = transform.position.z;
        Vector3 ShotForce = new Vector3(ShotAxisValue*NowShotPower*1.5f, 0,0);
        rb.AddForce(ShotForce);
    }

    private void IkuraDamage()
    {
        float NewPosZ = transform.position.z;
        float damagePersent= NewPosZ - OldPosZ;
        float damage = IkuraHP / 100 * damagePersent;
        DamageBar.value -= damage;
        if (damage == 0) Debug.Log("GameOver");
    }
}
