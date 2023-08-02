using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkuraController : MonoBehaviour
{
    Vector3 startPos;
    float standardPosY;
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
    private Transform MoveRock = null;
    public float StoneAuto;

    [Tooltip("IkuraChanをアタッチ")]
    [SerializeField] SphereCollider IkuraCol;
    private Rigidbody rb;

    private Animator animator;
    private int animeState=0;

    [Tooltip("WouldCanvasをアタッチ")]
    [SerializeField] private Canvas canvas;
    [Tooltip("MainCameraをアタッチ")]
    [SerializeField] private Camera mainCamera;
    [Tooltip("BackCameraをアタッチ")]
    [SerializeField] private Camera BackCamera;

    private bool MoveStoneSwitch = false;
    public enum IkuraState
    {
        None,
        Axis,
        Shot,
        Move,
        Wall,
    }
    private IkuraState NowIkuraState;

    [SerializeField]GameManager manager;
    void Start()
    {
        startPos = transform.position;
        NowIkuraState = IkuraState.Axis;
        DamageBar.maxValue=IkuraHP;
        DamageBar.value = IkuraHP;
        PowerBar.maxValue = MaxShotPower;
        rb = GetComponent<Rigidbody>();
        BackMonitorOff();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nowPos = transform.position;
        nowPos.y = standardPosY;
        transform.position = nowPos;
        if (MoveStoneSwitch)
        {
            Vector3 MSpos = MoveRock.transform.position;
            MSpos += new Vector3(0, 0, -1.5f);
            transform.position = MSpos;
        }
        //動く岩にくっついている時

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
        //ショット状態の時

        if(NowIkuraState==IkuraState.Move)
        {
            rb.AddForce(0, 0,RivarSpeed/50);
        }
        //動いている時

        if(NowIkuraState==IkuraState.Wall&&NearStone!=null)
        {
            Vector3 PLpos = transform.position;
            Vector3 NSpos = NearStone.transform.position;
            NSpos += new Vector3(0, 0, StoneAuto);
            if(PLpos!=NSpos)
            {
                float moveX = NSpos.x - PLpos.x;
                moveX = Mathf.Clamp(moveX,-0.07f,0.07f);
                float moveZ = NSpos.z - PLpos.z;
                moveZ = Mathf.Clamp(moveZ, 0,0.05f);
                transform.position += new Vector3(moveX, 0, moveZ);
                //rb.AddForce(moveX,0,moveZ);
            }
        }
        //壁に当たった時
        
        //以下、デバッグ用
        if(Input.GetKeyDown(KeyCode.Return))
        {
            IkuraHeel();
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            transform.position = startPos;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            BackMonitorOn();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            BackMonitorOff();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        

        if (col.gameObject.tag=="Stone")
        {
            //if(col.gameObject.GetComponent<Material>())
            //{
            //    if(col.gameObject.GetComponent<Material>().name=="Cyan")
            //    {

            //    }
            //}

            IkuraDamage();
            AxisStandby();
            
            if (col.gameObject.GetComponent<MoveRock>())
            {
                MoveRock = col.transform;
                MoveStoneSwitch = true;
            }
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
    //発射体制に偏移

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
    //発射方向を決定するボタン用プログラム

    private void PowerChange()
    {
        NowShotPower += MaxShotPower / 100;
        if(NowShotPower>MaxShotPower){NowShotPower = 0;}
        PowerBar.value = NowShotPower;
    }
    //発射パワーの計算

    private void ShotIkura()
    {
        MoveStoneSwitch = false;
        NowIkuraState = IkuraState.Move;
        OldPos = transform.position;
        Vector3 ShotForce = new Vector3(ShotAxisValue*NowShotPower*1.5f, 0,0);
        rb.AddForce(ShotForce);
    }
    //イクラ発射

    private void IkuraDamage()
    {
        Vector3 NewPosZ = transform.position;
        float damagePersent=Vector3.Distance( NewPosZ , OldPos);
        float damage = IkuraHP / 100 * damagePersent;
        DamageBar.value -= damage;
        if (DamageBar.value <= 0) Debug.Log("GameOver");
    }
    //ダメージ処理

    public void IkuraHeel()
    {
        float MaxHP = DamageBar.maxValue;
        IkuraHP = MaxHP;
        DamageBar.value = MaxHP;
    }
    //回復処理

    private IEnumerator StoneSearch()
    {
        NoShot();
        rb.velocity = Vector3.zero;
        float posZ = transform.position.z;
        //NearStone = null;

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Stone");
        if (targets==null)
        {
            AxisStandby();
            yield break;
        }
        //岩が無ければ終了する

        if (targets.Length == 1) NearStone = targets[0].transform;
        //ステージに岩が１つのみなら、その岩で決定する
        else
        {
            float NearZ = 1000f;
            foreach (GameObject target in targets)
            {
                // 前回計測したオブジェクトよりも近くにあれば記録
                float z = target.transform.position.z - StoneAuto;
                if (z - posZ >= 0 && NearZ > z - posZ)
                {
                    NearStone = target.transform;
                    NearZ = z - posZ;
                }
            }
        }
        //ステージに岩が複数あるなら、距離を探査する
        NowIkuraState = IkuraState.None;
        rb.velocity = Vector3.zero;
        if (NearStone == null)
        {
            AxisStandby();
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(2f);
            NowIkuraState = IkuraState.Wall;
        }
    }
    //一番近い岩を探査する

    private void NoShot()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
    }

    public void AnimationChange()
    {
        animeState++;
        Debug.Log("ここにアニメーション遷移を書く");
    }

    private void BackMonitorOn()
    {
        canvas.enabled = false;
        mainCamera.enabled = false;
        BackCamera.enabled = true;
    }
    //背後モニターをオンにする

    private void BackMonitorOff()
    {
        canvas.enabled = true;
        mainCamera.enabled = true;
        BackCamera.enabled = false;
    }
    //背後モニターをオフにする

    private void OnTriggerEnter(Collider col)
    {
        IkuraDead();
        DamageBar.value =0;
        Debug.Log("食べられちゃったギョ…");
        manager.eatIkura();
    }

    private void IkuraDead()
    {
        leftButton.gameObject.SetActive(false) ;
        rightButton.gameObject.SetActive(false);
        PowerBar.gameObject.SetActive(false);
        DamageBar.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
