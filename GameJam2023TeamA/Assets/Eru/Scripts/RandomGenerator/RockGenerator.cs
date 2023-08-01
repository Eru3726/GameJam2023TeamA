using System.Collections;
using System.Linq;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    [SerializeField,Header("石")]
    private GameObject RockObj;

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

    private float centerPos,crPos,clPos;

    private Vector3 genePos = new Vector3(0, 0.5f, 0);

    private int lastSelectedNumber = 0;
    private int[] allNumbers = { 1, 2, 3, 4 };

    void Start()
    {
        centerPos = (rightBorder + leftBorder) / 2;
        clPos = (centerPos + leftBorder) / 2;
        crPos = (rightBorder + centerPos) / 2;
        genePos.z = startPos;
        StartCoroutine(GeneRock());
    }

    private IEnumerator GeneRock()
    {
        int rockPos = PickRandomPos();
        if (rockPos == 1) genePos.x = Random.Range(leftBorder, clPos);
        else if (rockPos == 2) genePos.x = Random.Range(clPos, centerPos);
        else if (rockPos == 3) genePos.x = Random.Range(centerPos, crPos);
        else genePos.x = Random.Range(crPos, rightBorder);

        Instantiate(RockObj, genePos, Quaternion.identity);

        genePos.z += geneDis;
        if (genePos.z >= endPos) yield break;
        else StartCoroutine(GeneRock());
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
