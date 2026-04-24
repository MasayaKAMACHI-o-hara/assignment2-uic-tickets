using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AbilityVending : MonoBehaviour
{
    [Header("�`���̋@�`")]

    [Header("�l�i")]
    [SerializeField] private int Cost;

    [Header("�X�s�[�J�[")]
    [SerializeField] private AudioSource audioSource;

    [Header("�R�C��������")]
    [SerializeField] private AudioClip audioClip;

    [Header("�`���̋@UI�`")]

    [Header("UI")]
    [SerializeField] private GameObject AVUI;

    [Header("�l�i�e�L�X�g")]
    [SerializeField] private TextMeshProUGUI CostText;

    [Header("UI�w�i")]
    [SerializeField] private Image AVUIBG;

    [Header("UI�J���[")]
    [SerializeField] private Color NotEnoughColor = new Color(0.7f, 0, 0, 0.7f); // ��
    [SerializeField] private Color EnoughColor = new Color(0, 0.7f, 0, 0.7f);  // ��#

    [Header("�`�I���UI�`")]

    [Header("�X�N���v�g")]
    [SerializeField] private ChoiceUI ChoiceScript;

    [Header("UI")]
    [SerializeField] private GameObject ChoiceUI;

    [Header("�`�ʁ`")]

    [Header("�X�N���v�g")]
    [SerializeField] public KanMove kanMove;

    [Header("�V���J�V���J")]
    [SerializeField] public SyakaSyaka syakaSyaka;

    //�ʃZ���T�[�G���Abool�֐�
    bool InKanSensorArea;

    //�����ݒ�
    void Start()
    {
        //���̋@UI���\����
        AVUI.SetActive(false);

        //�l�i��UI�ɔ��f
        CostText.text = Cost + "円";

        //�I���UI���\����
        ChoiceUI.SetActive(false);

        //�Z���T�[�G���A�O
        InKanSensorArea = false;

        //�J�[�\����\��---------------Debug
        Cursor.visible = false;

        // �J�[�\�����ʓ�œ�������
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        //�Z���T�[�G���A��Ȃ�
        if (InKanSensorArea)
        {
            //�R�C����������
            if (Cost <= kanMove.coin)
            {
                //���̋@UI��΂�
                AVUIBG.color = EnoughColor;

                //F�L�[���������
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //�ʒ�~
                    kanMove.ActiveMove = false;
                    syakaSyaka.ActiveSyaka = false;

                    //�R�C���������Đ�
                    audioSource.PlayOneShot(audioClip);

                    //�R�C������
                    kanMove.coin -= Cost;

                    //�I��UI�̃X�s�[�J�[����̎��̋@�Ɏw��
                    ChoiceScript.audioSource = audioSource;

                    //������w���������Ƃ�`����
                    ChoiceScript.BuyUpgrade = true;

                    // �J�[�\���\��
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    //���̋@UI��\��
                    AVUI.SetActive(false);

                    //�I���UI��\����
                    ChoiceUI.SetActive(true);

                    //�Z���T�[��I�t�ɂ��Ĕj��
                    InKanSensorArea = false;
                    Destroy(gameObject);
                }
            }
            //�R�C��������Ȃ����
            else
                //���̋@UI��Ԃ�
                AVUIBG.color = NotEnoughColor;
        }
    }
    //�Z���T�[�ɓ��������̊֐�
    private void OnTriggerEnter(Collider Sensor)
    {
        if (Sensor.CompareTag("Kan"))
        {
            //���̋@UI�\��
            AVUI.SetActive(true);

            //�Z���T�[�G���A��
            InKanSensorArea = true;
        }
    }
    //�Z���T�[����o�����̊֐�
    private void OnTriggerExit(Collider Sensor)
    {
        if (Sensor.CompareTag("Kan"))
        {
            //���̋@UI��\��
            AVUI.SetActive(false);

            //�Z���T�[�G���A�O
            InKanSensorArea = false;
        }
    }
}