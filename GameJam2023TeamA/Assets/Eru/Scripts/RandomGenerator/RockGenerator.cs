using System.Collections;
using System.Linq;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    [SerializeField,Header("石")]
    private GameObject RockObj;

    [SerializeField, Header("サブ岩生成フラグ")]
    private bool subRock = true;

    [SerializeField,Header("生成距離")]
    private float geneDis = 7.5f;

    [SerializeField, Header("生成右範囲")]
    private float rightBorder = -3f;

    [SerializeField, Header("生成左範囲")]
    private float leftBorder = 3f;

    [SerializeField, Header("生成開始位置")]
    private float startPos = 0f;

    [SerializeField, Header("生成終了位置")]
    private float endPos = 50f;

    [SerializeField, Header("動く岩生成率"), Range(0f, 100f)]
    private float moveProbability = 20f;

    [SerializeField, Header("動く岩右範囲")]
    private float moveRightBorder = 3;

    [SerializeField, Header("動く岩左範囲")]
    private float moveLeftBorder = -3f;

    [SerializeField, Header("動く岩速度")]
    private float moveSpeed = 5f;

    [SerializeField,Header("滑る岩生成率"), Range(0f, 100f)]
    private float iceProbability = 20f;

    [SerializeField, Header("滑る岩の物理マテリアル")]
    private PhysicMaterial icePhysicMaterial;

    [SerializeField, Header("滑る岩のマテリアル")]
    private Material iceMaterial;

    [SerializeField, Header("毒岩生成率"), Range(0f, 100f)]
    private float poisonProbability = 20f;

    [SerializeField, Header("毒ダメージ")]
    private int poisonDamage = 1;

    [SerializeField, Header("毒待機時間")]
    private float poisonDamageInterval = 0.1f;

    [SerializeField, Header("毒岩のマテリアル")]
    private Material poisonMaterial;

    [HideInInspector]
    public static bool hard = false;

    private float centerPos,crPos,clPos, prevLane,prevPosX;

    private Vector3 genePos = new Vector3(0, 0, 0);

    private int lastSelectedNumber = 0;
    private int[] allNumbers = { 1, 2, 3, 4 };

    void Start()
    {
        Application.targetFrameRate = 60;
        centerPos = (rightBorder + leftBorder) / 2;
        clPos = (centerPos + leftBorder) / 2;
        crPos = (rightBorder + centerPos) / 2;
        genePos.z = startPos;
        prevLane = 2;
        prevPosX = 0;
        StartCoroutine(GeneRock());
    }

    private IEnumerator GeneRock()
    {
        int rockPos = PickRandomPos();
        if (rockPos == 1) genePos.x = Random.Range(leftBorder, clPos);
        else if (rockPos == 2) genePos.x = Random.Range(clPos, centerPos);
        else if (rockPos == 3) genePos.x = Random.Range(centerPos, crPos);
        else genePos.x = Random.Range(crPos, rightBorder);

        GameObject rock = Instantiate(RockObj, genePos, Quaternion.identity);
        rock.transform.parent = this.transform;
        GimmickRock(rock);

        if (subRock)
        {
            int rand = Random.Range(0, 2);
            if (prevLane == 1) genePos.x = Random.Range(clPos, centerPos);
            else if (prevLane == 2)
            {
                if (rand == 0) genePos.x = Random.Range(leftBorder, clPos);
                else genePos.x = Random.Range(centerPos, crPos);
            }
            else if (prevLane == 3)
            {
                if (rand == 0) genePos.x = Random.Range(clPos, centerPos);
                else genePos.x = Random.Range(crPos, rightBorder);
            }
            else genePos.x = Random.Range(centerPos, crPos);

            genePos.z += geneDis / 2;

            ShiftRock(rockPos);

            rock = Instantiate(RockObj, genePos, Quaternion.identity);
            rock.transform.parent = this.transform;
            GimmickRock(rock);
        }
        genePos.z += geneDis;

        prevLane = rockPos;
        if (genePos.z >= endPos) yield break;
        else StartCoroutine(GeneRock());
    }

    private void ShiftRock(int rockPos)
    {
        if (genePos.x < prevPosX + 2f && genePos.x > prevPosX - 2f)
        {
            if (rockPos == 1) genePos.x += 2f;
            else if (rockPos == 2 || rockPos == 3)
            {
                int rand = Random.Range(0, 2);
                if (rand == 0) genePos.x -= 2f;
                else genePos.x = genePos.x += 2f;
            }
            else genePos.x = genePos.x -= 2f;
        }
    }

    private void GimmickRock(GameObject rock)
    {
        float rand = Random.Range(0.0f, 100.0f);
        if(rand <= moveProbability)
        {
            MoveGimmick(rock);
            return;
        }

        rand = Random.Range(0.0f, 100.0f);
        if(rand <= iceProbability)
        {
            IceGimmick(rock);
        }

        rand = Random.Range(0.0f, 100.0f);
        if (rand <= poisonProbability)
        {
            PoisonGimmick(rock);
        }
    }

    private void MoveGimmick(GameObject rock)
    {
        var mr = rock.AddComponent<MoveRock>();
        mr.moveRightBorder = this.moveRightBorder;
        mr.moveLeftBorder = this.moveLeftBorder;
        mr.moveSpeed = this.moveSpeed;
    }

    private void IceGimmick(GameObject rock)
    {
        rock.GetComponent<SphereCollider>().material = this.icePhysicMaterial;
        rock.GetComponentInChildren<MeshRenderer>().material = this.iceMaterial;
        rock.gameObject.tag = "Ice";
    }

    private void PoisonGimmick(GameObject rock)
    {
        var pr = rock.AddComponent<PoisonRock>();
        pr.damage = this.poisonDamage;
        pr.damageInterval = this.poisonDamageInterval;
        rock.GetComponentInChildren<MeshRenderer>().material = this.poisonMaterial;
    }

    private int PickRandomPos()
    {
        int[] availableNumbers = allNumbers.Where(number => number != lastSelectedNumber).ToArray();

        if (lastSelectedNumber == 0) availableNumbers = allNumbers;

        int randomIndex = Random.Range(0, availableNumbers.Length);
        int selectedNumber = availableNumbers[randomIndex];

        lastSelectedNumber = selectedNumber;

        return selectedNumber;
    }
}
