using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AbilityVending : MonoBehaviour
{
    [Header("ï¿½`ï¿½ï¿½ï¿½Ì‹@ï¿½`")]

    [Header("ï¿½lï¿½i")]
    [SerializeField] private int Cost;

    [Header("ï¿½Xï¿½sï¿½[ï¿½Jï¿½[")]
    [SerializeField] private AudioSource audioSource;

    [Header("ï¿½Rï¿½Cï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½")]
    [SerializeField] private AudioClip audioClip;

    [Header("ï¿½`ï¿½ï¿½ï¿½Ì‹@UIï¿½`")]

    [Header("UI")]
    [SerializeField] private GameObject AVUI;

    [Header("ï¿½lï¿½iï¿½eï¿½Lï¿½Xï¿½g")]
    [SerializeField] private TextMeshProUGUI CostText;

    [Header("UIï¿½wï¿½i")]
    [SerializeField] private Image AVUIBG;

    [Header("UIï¿½Jï¿½ï¿½ï¿½[")]
    [SerializeField] private Color NotEnoughColor = new Color(0.7f, 0, 0, 0.7f); // ï¿½ï¿½
    [SerializeField] private Color EnoughColor = new Color(0, 0.7f, 0, 0.7f);  // ï¿½ï¿½#

    [Header("ï¿½`ï¿½Iï¿½ï¿½ï¿½UIï¿½`")]

    [Header("ï¿½Xï¿½Nï¿½ï¿½ï¿½vï¿½g")]
    [SerializeField] private ChoiceUI ChoiceScript;

    [Header("UI")]
    [SerializeField] private GameObject ChoiceUI;

    [Header("ï¿½`ï¿½Ê`")]

    [Header("ï¿½Xï¿½Nï¿½ï¿½ï¿½vï¿½g")]
    [SerializeField] public KanMove kanMove;

    [Header("ï¿½Vï¿½ï¿½ï¿½Jï¿½Vï¿½ï¿½ï¿½J")]
    [SerializeField] public SyakaSyaka syakaSyaka;

<<<<<<< Updated upstream
    //ï¿½ÊƒZï¿½ï¿½ï¿½Tï¿½[ï¿½Gï¿½ï¿½ï¿½Aboolï¿½Öï¿½
=======
    [Header("ƒ|[ƒY")]
    [SerializeField] public PauseSistem pauseSistem;

    [Header("§ŒÀŠÔ")]
    [SerializeField] public CountDown countDown;

    //ŠÊƒZƒ“ƒT[ƒGƒŠƒAboolŠÖ”
>>>>>>> Stashed changes
    bool InKanSensorArea;

    //ï¿½ï¿½ï¿½ï¿½ï¿½İ’ï¿½
    void Start()
    {
        //ï¿½ï¿½ï¿½Ì‹@UIï¿½ï¿½ï¿½\ï¿½ï¿½ï¿½ï¿½
        AVUI.SetActive(false);

        //ï¿½lï¿½iï¿½ï¿½UIï¿½É”ï¿½ï¿½f
        CostText.text = Cost + "å††";

        //ï¿½Iï¿½ï¿½ï¿½UIï¿½ï¿½ï¿½\ï¿½ï¿½ï¿½ï¿½
        ChoiceUI.SetActive(false);

        //ï¿½Zï¿½ï¿½ï¿½Tï¿½[ï¿½Gï¿½ï¿½ï¿½Aï¿½O
        InKanSensorArea = false;

        //ï¿½Jï¿½[ï¿½\ï¿½ï¿½ï¿½ï¿½\ï¿½ï¿½---------------Debug
        Cursor.visible = false;

        // ï¿½Jï¿½[ï¿½\ï¿½ï¿½ï¿½ï¿½ï¿½Ê“ï¿½Å“ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        //ï¿½Zï¿½ï¿½ï¿½Tï¿½[ï¿½Gï¿½ï¿½ï¿½Aï¿½ï¿½È‚ï¿½
        if (InKanSensorArea)
        {
            //ï¿½Rï¿½Cï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
            if (Cost <= kanMove.coin)
            {
                //ï¿½ï¿½ï¿½Ì‹@UIï¿½ï¿½Î‚ï¿½
                AVUIBG.color = EnoughColor;

                //Fï¿½Lï¿½[ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //ï¿½Ê’ï¿½~
                    kanMove.ActiveMove = false;
                    syakaSyaka.ActiveSyaka = false;
                    pauseSistem.IsActiveESC = false;
                    countDown.TimerOn = false;

                    //ï¿½Rï¿½Cï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Äï¿½
                    audioSource.PlayOneShot(audioClip);

                    //ï¿½Rï¿½Cï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
                    kanMove.coin -= Cost;

                    //ï¿½Iï¿½ï¿½UIï¿½ÌƒXï¿½sï¿½[ï¿½Jï¿½[ï¿½ï¿½ï¿½ï¿½Ìï¿½ï¿½Ì‹@ï¿½Éwï¿½ï¿½
                    ChoiceScript.audioSource = audioSource;

<<<<<<< Updated upstream
                    //ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½wï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ‚ï¿½`ï¿½ï¿½ï¿½ï¿½
                    ChoiceScript.BuyUpgrade = true;
=======
                    //‹­‰»‚ğw“ü‚µ‚½‚±‚Æ‚ğ“`‚¦‚é
                    ChoiceScript.BuyAbility();
>>>>>>> Stashed changes

                    // ï¿½Jï¿½[ï¿½\ï¿½ï¿½ï¿½\ï¿½ï¿½
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    //ï¿½ï¿½ï¿½Ì‹@UIï¿½ï¿½\ï¿½ï¿½
                    AVUI.SetActive(false);

                    //ï¿½Iï¿½ï¿½ï¿½UIï¿½ï¿½\ï¿½ï¿½ï¿½ï¿½
                    ChoiceUI.SetActive(true);

                    //ï¿½Zï¿½ï¿½ï¿½Tï¿½[ï¿½ï¿½Iï¿½tï¿½É‚ï¿½ï¿½Ä”jï¿½ï¿½
                    InKanSensorArea = false;
                    Destroy(gameObject);
                }
            }
            //ï¿½Rï¿½Cï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½È‚ï¿½ï¿½ï¿½ï¿½
            else
                //ï¿½ï¿½ï¿½Ì‹@UIï¿½ï¿½Ô‚ï¿½
                AVUIBG.color = NotEnoughColor;
        }
    }
    //ï¿½Zï¿½ï¿½ï¿½Tï¿½[ï¿½É“ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ÌŠÖï¿½
    private void OnTriggerEnter(Collider Sensor)
    {
        if (Sensor.CompareTag("Kan"))
        {
            //ï¿½ï¿½ï¿½Ì‹@UIï¿½\ï¿½ï¿½
            AVUI.SetActive(true);

            //ï¿½Zï¿½ï¿½ï¿½Tï¿½[ï¿½Gï¿½ï¿½ï¿½Aï¿½ï¿½
            InKanSensorArea = true;
        }
    }
    //ï¿½Zï¿½ï¿½ï¿½Tï¿½[ï¿½ï¿½ï¿½ï¿½oï¿½ï¿½ï¿½ï¿½ï¿½ÌŠÖï¿½
    private void OnTriggerExit(Collider Sensor)
    {
        if (Sensor.CompareTag("Kan"))
        {
            //ï¿½ï¿½ï¿½Ì‹@UIï¿½ï¿½\ï¿½ï¿½
            AVUI.SetActive(false);

            //ï¿½Zï¿½ï¿½ï¿½Tï¿½[ï¿½Gï¿½ï¿½ï¿½Aï¿½O
            InKanSensorArea = false;
        }
    }
}