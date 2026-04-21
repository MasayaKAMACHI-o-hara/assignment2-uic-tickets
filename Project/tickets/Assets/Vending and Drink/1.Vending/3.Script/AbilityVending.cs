using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AbilityVending : MonoBehaviour
{
    [Header("～能力自販機～")]

    [Header("値段")]
    [SerializeField] private int Cost;

    [Header("スピーカー")]
    [SerializeField] private AudioSource audioSource;

    [Header("コイン投入音")]
    [SerializeField] private AudioClip audioClip;

    [Header("～自販機UI～")]

    [Header("UI")]
    [SerializeField] private GameObject AVUI;

    [Header("値段テキスト")]
    [SerializeField] private TextMeshProUGUI CostText;

    [Header("UI背景")]
    [SerializeField] private Image AVUIBG;

    [Header("UIカラー")]
    [SerializeField] private Color NotEnoughColor = new Color(0.7f, 0, 0, 0.7f); // 赤
    [SerializeField] private Color EnoughColor = new Color(0, 0.7f, 0, 0.7f);  // 緑#

    [Header("～選択肢UI～")]

    [Header("スクリプト")]
    [SerializeField] private ChoiceUI ChoiceScript;

    [Header("UI")]
    [SerializeField] private GameObject ChoiceUI;

    [Header("～缶～")]

    [Header("スクリプト")]
    [SerializeField] public KanMove kanMove;

    //缶センサーエリアbool関数
    bool InKanSensorArea;

    //初期設定
    void Start()
    {
        //自販機UIを非表示に
        AVUI.SetActive(false);

        //値段をUIに反映
        CostText.text = Cost + "コイン";

        //選択肢UIを非表示に
        ChoiceUI.SetActive(false);

        //センサーエリア外
        InKanSensorArea = false;

        Debug.Log("コイン枚数：" + kanMove.coin + "枚");

        //カーソル非表示---------------Debug
        Cursor.visible = false;

        // カーソルを画面内で動かせる
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        //センサーエリア内なら
        if (InKanSensorArea)
        {
            //コインが足りれば
            if (Cost <= kanMove.coin)
            {
                //自販機UIを緑に
                AVUIBG.color = EnoughColor;

                //Fキーを押したら
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //自販機UI非表示
                    AVUI.SetActive(false);

                    //コイン投入音再生
                    audioSource.PlayOneShot(audioClip);

                    //選択UIのスピーカーをこの自販機に指定
                    ChoiceScript.audioSource = audioSource;

                    //選択肢UIを表示に
                    ChoiceUI.SetActive(true);

                    //強化を購入したことを伝える
                    ChoiceScript.BuyAbility = true;

                    // カーソル表示
                    Cursor.visible = true;

                    //センサーをオフに
                    InKanSensorArea = false;

                    //センサーを破壊
                    Destroy(gameObject);
                }
            }
            //コインが足りなければ
            else
                //自販機UIを赤に
                AVUIBG.color = NotEnoughColor;
        }
    }
    //センサーに入った時の関数
    private void OnTriggerEnter(Collider Sensor)
    {
        if (Sensor.CompareTag("Kan"))
        {
            Debug.Log("能力自販機に触れた");

            //自販機UI表示
            AVUI.SetActive(true);

            //センサーエリア内
            InKanSensorArea = true;
        }
    }
    //センサーから出た時の関数
    private void OnTriggerExit(Collider Sensor)
    {
        if (Sensor.CompareTag("Kan"))
        {
            Debug.Log("能力自販機を離れた");

            //自販機UI非表示
            AVUI.SetActive(false);

            //センサーエリア外
            InKanSensorArea = false;
        }
    }
}