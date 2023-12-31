using System.Collections;
using System.Linq;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    [SerializeField,Header("��")]
    private GameObject RockObj;

    [SerializeField, Header("�T�u�␶���t���O")]
    private bool subRock = true;

    [SerializeField,Header("��������")]
    private float geneDis = 7.5f;

    [SerializeField, Header("�����E�͈�")]
    private float rightBorder = -3f;

    [SerializeField, Header("�������͈�")]
    private float leftBorder = 3f;

    [SerializeField, Header("�����J�n�ʒu")]
    private float startPos = 0f;

    [SerializeField, Header("�����I���ʒu")]
    private float endPos = 50f;

    [SerializeField, Header("�����␶����"), Range(0f, 100f)]
    private float moveProbability = 20f;

    [SerializeField, Header("������E�͈�")]
    private float moveRightBorder = 3;

    [SerializeField, Header("�����⍶�͈�")]
    private float moveLeftBorder = -3f;

    [SerializeField, Header("�����⑬�x")]
    private float moveSpeed = 5f;

    [SerializeField,Header("����␶����"), Range(0f, 100f)]
    private float iceProbability = 20f;

    [SerializeField, Header("�����̕����}�e���A��")]
    private PhysicMaterial icePhysicMaterial;

    [SerializeField, Header("�����̃}�e���A��")]
    private Material iceMaterial;

    [SerializeField, Header("�Ŋ␶����"), Range(0f, 100f)]
    private float poisonProbability = 20f;

    [SerializeField, Header("�Ń_���[�W")]
    private int poisonDamage = 1;

    [SerializeField, Header("�őҋ@����")]
    private float poisonDamageInterval = 0.1f;

    [SerializeField, Header("�Ŋ�̃}�e���A��")]
    private Material poisonMaterial;

    [HideInInspector]
    public static bool hard = false;

    private float centerPos,crPos,clPos, prevLane,prevPosX;

    private Vector3 genePos = new Vector3(0, 0, 0);

    private int lastSelectedNumber = 0;
    private int[] allNumbers = { 1, 2, 3, 4 };

    void Start()
    {
        if (hard) HardGame();
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

    private void HardGame()
    {
        iceProbability *= 2;
        moveProbability *= 2;
        poisonProbability *= 2;
        geneDis += 2f;
        subRock = false;
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
