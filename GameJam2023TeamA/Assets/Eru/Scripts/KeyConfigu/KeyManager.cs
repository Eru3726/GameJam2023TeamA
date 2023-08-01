using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference pause;

    [SerializeField]
    private GameObject keyCan,padCan;

    [SerializeField] 
    private RebindSaveManager rsm;

    private bool nowFlg = false;

    private void Awake()
    {
        rsm.Load();
        keyCan.SetActive(false);
        padCan.SetActive(false);
        pause.action.Enable();
        nowFlg = false;
    }

    void Update()
    {
        if (pause.action.triggered)
        {
            keyCan.SetActive(true);
            nowFlg = true;
        }
        else if (nowFlg)
        {
            keyCan.SetActive(false);
            padCan.SetActive(false);
            nowFlg = false;
        }
    }

    public void KeyBoardCanvas()
    {
        keyCan.SetActive(true);
        padCan.SetActive(false);
    }

    public void GamePadCanvas()
    {
        padCan.SetActive(true);
        keyCan.SetActive(false);
    }
}
