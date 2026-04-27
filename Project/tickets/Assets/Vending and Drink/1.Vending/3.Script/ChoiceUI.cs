using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;
public class ChoiceUI : MonoBehaviour
{
    [Header("�ʃX�N���v�g")]
    [SerializeField] public KanMove kanMove;

    [Header("�ʃV���J�V���J")]
    [SerializeField] public SyakaSyaka syakaSyaka;

    [Header("�W���[�X�w����")]
    [SerializeField] private AudioClip audioClip;

    [Header("�I��UI")]
    [SerializeField] private GameObject UI;

    [Header("�I����̖��O")]
    [SerializeField] private TextMeshProUGUI Name1;
    [SerializeField] private TextMeshProUGUI Name2;
    [SerializeField] private TextMeshProUGUI Name3;

    [Header("�I����̉摜")]
    [SerializeField] private Image Image1;
    [SerializeField] private Image Image2;
    [SerializeField] private Image Image3;

    [Header("�I����̐��")]
    [SerializeField] private TextMeshProUGUI Explanation1;
    [SerializeField] private TextMeshProUGUI Explanation2;
    [SerializeField] private TextMeshProUGUI Explanation3;

    [Header("�I��UI")]
    [SerializeField] private GameObject Choosing1;
    [SerializeField] private GameObject Choosing2;
    [SerializeField] private GameObject Choosing3;

    [Header("�c�莞��UI")]
    [SerializeField] private AbilityTimeUI abilityTime;

    [Header("�����f�[�^")]
    [SerializeField] public DrinkManager Data;

    //���݉�ʂɕ\������Ă���3�̃f�[�^��ێ����郊�X�g
    private List<UpgradeData> currentDisplayedUpgrades = new List<UpgradeData>();
    private List<AbilityData> currentDisplayedAbilities = new List<AbilityData>();

    //�ǂ̎��̋@�ōw��������
    [NonSerialized] public bool BuyUpgrade = false;
    [NonSerialized] public bool BuyAbility = false;

    //�X�s�[�J�[�̎w��
    [NonSerialized] public AudioSource audioSource;


    //�r���y�A�̒�`�� {A, B} �̂ǂ��炩������I�΂ꂽ��������͏o���Ȃ�
    private readonly int[,] exclusivePairs = { { 2, 3 }, { 6, 7 }, { 8, 9 } };

    //�����ݒ�
    void Start()
    {
        //�I�𒆂��\��
        Choosing1.SetActive(false);
        Choosing2.SetActive(false);
        Choosing3.SetActive(false);
    }
    void Update()
    {
        if (BuyUpgrade)//�������̋@��g�p������
        {
            PickRandomUpgrades();//�����f�[�^������_���ɑI��
            BuyUpgrade = false;//�������̋@�g�p�󋵃��Z�b�g
        }
        if (BuyAbility)//�\�͎��̋@��g�p������
        {
            PickRandomAbilities();//�\�̓f�[�^������_���ɑI��
            BuyAbility = false;//�\�͎��̋@�g�p�󋵃��Z�b�g
        }
    }

    #region ���������I�o�֐�
    //�����f�[�^��m���Ɛ���Ɋ�Â���3�I��
    public void PickRandomUpgrades()
    {
        //�Œ���K�v�ȃf�[�^�����邩�`�F�b�N
        if (Data.upgradeData.Length < 3) return;

        //�O��̃f�[�^�����
        currentDisplayedUpgrades.Clear();
        List<int> selectedNumbers = new List<int>();

        //3���܂�܂łЂ�����J��Ԃ�
        while (selectedNumbers.Count < 3)
        {
            //�m����������I��
            int candidate = GetUpgradeNumberByProbability();

            //���ɑI�΂�Ă���΂�蒼��
            if (selectedNumbers.Contains(candidate)) continue;

            //����̃y�A�ɂȂ������蒼��
            if (IsExclusiveUpgradePair(candidate, selectedNumbers)) continue;

            //����OK��������I����ɓ����
            selectedNumbers.Add(candidate);
        }

        // �ԍ�����X�N���^�u���I�u�W�F�N�g��R�Â�����
        foreach (int num in selectedNumbers)
        {
            //�����Ǘ���������ԍ�����v���镨��T��
            UpgradeData d = Data.upgradeData.FirstOrDefault(x => x.DrinkNumber == num);
            if (d != null) currentDisplayedUpgrades.Add(d);
        }

        // UI���f
        UpdateUpgradeUI();

        UI.SetActive(true);
        Cursor.visible = true;
    }
    //������m���Ō��肷��֐�
    private int GetUpgradeNumberByProbability()
    {
        //��m���p�����쐬
        int roll1 = UnityEngine.Random.Range(1, 101);

        //20�ȉ��Ȃ��m��
        if (roll1 <= 20)
        {
            // ��m��: 4��
            return 4;
        }
        //20�ȉ��ȊO
        else
        {
            //���E���m���p�����쐬
            int roll2 = UnityEngine.Random.Range(1, 101);

            //40�ȉ��Ȃ璆�m��
            if (roll2 <= 40)
            {
                // ���m��: 3, 5, 7, 9
                int[] mid = { 3, 5, 7, 9 };
                return mid[UnityEngine.Random.Range(0, mid.Length)];
            }
            //40�ȉ��ȊO�Ȃ獂�m��
            else
            {
                // ���m��: 0, 1, 2, 6, 8, 10
                int[] high = { 0, 1, 2, 6, 8, 10 };
                return high[UnityEngine.Random.Range(0, high.Length)];
            }
        }
    }
    //�����y�A�֎~����֐�
    private bool IsExclusiveUpgradePair(int candidate, List<int> currentList)
    {
        //�֎~���X�g��`�F�b�N
        for (int i = 0; i < exclusivePairs.GetLength(0); i++)
        {
            int p1 = exclusivePairs[i, 0]; // �y�A�̕Е�
            int p2 = exclusivePairs[i, 1]; // �y�A�̂���Е�

            //�֎~�y�A��������Ă������蒼��
            if (candidate == p1 && currentList.Contains(p2)) return true;
            if (candidate == p2 && currentList.Contains(p1)) return true;
        }
        return false;//�ǂ̋֎~�y�A�ɂ�Y�����Ȃ����OK
    }
    //����������UI�X�V
    private void UpdateUpgradeUI()
    {
        if (currentDisplayedUpgrades.Count < 3) return;

        Name1.text = currentDisplayedUpgrades[0].Name;
        Image1.sprite = currentDisplayedUpgrades[0].Image;
        Explanation1.text = currentDisplayedUpgrades[0].Explanation;

        Name2.text = currentDisplayedUpgrades[1].Name;
        Image2.sprite = currentDisplayedUpgrades[1].Image;
        Explanation2.text = currentDisplayedUpgrades[1].Explanation;

        Name3.text = currentDisplayedUpgrades[2].Name;
        Image3.sprite = currentDisplayedUpgrades[2].Image;
        Explanation3.text = currentDisplayedUpgrades[2].Explanation;
    }
    #endregion

    #region �\�͈����I�o�֐�
    //�\�̓f�[�^��m���Ɋ�Â���3�I��
    public void PickRandomAbilities()
    {
        //�Œ���K�v�ȃf�[�^�����邩�`�F�b�N
        if (Data.abilityData.Length < 3) return;

        //�O��̃f�[�^�����
        currentDisplayedAbilities.Clear();
        List<int> selectedNumbers = new List<int>();

        //3���܂�܂łЂ�����J��Ԃ�
        while (selectedNumbers.Count < 3)
        {
            //�m����������I��
            int candidate = GetAbilityNumberByProbability();

            //���ɑI�΂�Ă���΂�蒼��
            if (selectedNumbers.Contains(candidate)) continue;

            //OK��������I����ɓ����
            selectedNumbers.Add(candidate);
        }

        // �ԍ�����X�N���^�u���I�u�W�F�N�g��R�Â�����
        foreach (int num in selectedNumbers)
        {
            //�����Ǘ���������ԍ�����v���镨��T��
            AbilityData d = Data.abilityData.FirstOrDefault(x => x.DrinkNumber == num);
            if (d != null) currentDisplayedAbilities.Add(d);
        }

        // UI���f
        UpdateAbilityUI();

        UI.SetActive(true);
        Cursor.visible = true;
    }
    //�\�͂�m���Ō��肷��֐�
    private int GetAbilityNumberByProbability()
    {
        //��m���p�����쐬
        int roll = UnityEngine.Random.Range(1, 101);

        //20�ȉ��Ȃ��m��
        if (roll <= 20)
        {
            // ��m��: 6��
            return 6;
        }
        //20�ȉ��ȊO�Ȃ獂�m��
        else
        {

            // ���m��: 0, 1, 2, 3, 4, 5
            int[] high = { 0, 1, 2, 3, 4, 5 };
            return high[UnityEngine.Random.Range(0, high.Length)];

        }
    }
    //�\�͈�����UI�X�V
    private void UpdateAbilityUI()
    {
        if (currentDisplayedAbilities.Count < 3) return;

        Name1.text = currentDisplayedAbilities[0].Name;
        Image1.sprite = currentDisplayedAbilities[0].Image;
        Explanation1.text = currentDisplayedAbilities[0].Explanation;

        Name2.text = currentDisplayedAbilities[1].Name;
        Image2.sprite = currentDisplayedAbilities[1].Image;
        Explanation2.text = currentDisplayedAbilities[1].Explanation;

        Name3.text = currentDisplayedAbilities[2].Name;
        Image3.sprite = currentDisplayedAbilities[2].Image;
        Explanation3.text = currentDisplayedAbilities[2].Explanation;
    }
    #endregion

    #region �C�x���g�g���K�[�E�{�^���֐�
    //�I���1�ɃJ�[�\������������̊֐�
    public void Choosing1PointerEnter()
    {
        //�I�𒆕\��
        Choosing1.SetActive(true);
    }
    //�I���1�̃J�[�\�����~�肽���̊֐�
    public void Choosing1PointerExit()
    {
        //�I�𒆔�\��
        Choosing1.SetActive(false);
    }
    //�I���1�{�^���֐�
    public void Choices1Button()
    {
        Debug.Log("�I�� : " + Name1.text);
        abilityTime.StartAbility(Image1.sprite);
        //UI�����
        CloseUI();
    }

    //�I���2�ɃJ�[�\������������̊֐�
    public void Choosing2PointerEnter()
    {
        //�I�𒆕\��
        Choosing2.SetActive(true);
    }
    //�I���2�̃J�[�\�����~�肽���̊֐�
    public void Choosing2PointerExit()
    {
        //�I�𒆔�\��
        Choosing2.SetActive(false);
    }
    //�I���2�{�^���֐�
    public void Choices2Button()
    {
        Debug.Log("�I�� : " + Name2.text);
        abilityTime.StartAbility(Image2.sprite);
        //UI�����
        CloseUI();
    }

    //�I���3�ɃJ�[�\������������̊֐�
    public void Choosing3PointerEnter()
    {
        //�I�𒆕\��
        Choosing3.SetActive(true);
    }
    //�I���3�̃J�[�\�����~�肽���̊֐�
    public void Choosing3PointerExit()
    {
        //�I�𒆔�\��
        Choosing3.SetActive(false);
    }
    //�I���3�{�^���֐�
    public void Choices3Button()
    {
        Debug.Log("�I�� : " + Name3.text);
        abilityTime.StartAbility(Image3.sprite);
        //UI�����
        CloseUI();
    }
    #endregion

    //UI�����֐�
    private void CloseUI()
    {
        //�W���[�X�w�����Đ�
        audioSource.PlayOneShot(audioClip);

        //�ʓ���ĊJ
        kanMove.ActiveMove = true;
        syakaSyaka.ActiveSyaka = true;

        //�I�𒆔�\��
        Choosing1.SetActive(false);
        Choosing2.SetActive(false);
        Choosing3.SetActive(false);

        //�J�[�\����\��
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //UI��\��
        UI.SetActive(false);
    }
}