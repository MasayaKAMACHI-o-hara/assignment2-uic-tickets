using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;
public class ChoiceUI : MonoBehaviour
{
    [Header("ジュース購入音")]
    [SerializeField] private AudioClip audioClip;

    [Header("選択UI")]
    [SerializeField] private GameObject UI;

    [Header("選択肢の名前")]
    [SerializeField] private TextMeshProUGUI Name1;
    [SerializeField] private TextMeshProUGUI Name2;
    [SerializeField] private TextMeshProUGUI Name3;

    [Header("選択肢の画像")]
    [SerializeField] private Image Image1;
    [SerializeField] private Image Image2;
    [SerializeField] private Image Image3;

    [Header("選択肢の説明")]
    [SerializeField] private TextMeshProUGUI Explanation1;
    [SerializeField] private TextMeshProUGUI Explanation2;
    [SerializeField] private TextMeshProUGUI Explanation3;

    [Header("選択中UI")]
    [SerializeField] private GameObject Choosing1;
    [SerializeField] private GameObject Choosing2;
    [SerializeField] private GameObject Choosing3;

    [Header("飲料データ")]
    [SerializeField] public DrinkManager Data;

    //現在画面に表示されている3つのデータを保持するリスト
    private List<UpgradeData> currentDisplayedUpgrades = new List<UpgradeData>();
    private List<AbilityData> currentDisplayedAbilities = new List<AbilityData>();

    //どの自販機で購入したか
    [NonSerialized] public bool BuyUpgrade = false;
    [NonSerialized] public bool BuyAbility = false;

    //スピーカーの指定
    [NonSerialized] public AudioSource audioSource;


    //排他ペアの定義で {A, B} のどちらか一方が選ばれたらもう一方は出さない
    private readonly int[,] exclusivePairs = { { 2, 3 }, { 6, 7 }, { 8, 9 } };

    //初期設定
    void Start()
    {
        //選択中を非表示
        Choosing1.SetActive(false);
        Choosing2.SetActive(false);
        Choosing3.SetActive(false);
    }
    void Update()
    {
        if (BuyUpgrade)//強化自販機を使用したら
        {
            PickRandomUpgrades();//強化データをランダムに選ぶ
            BuyUpgrade = false;//強化自販機使用状況リセット
        }
        if (BuyAbility)//能力自販機を使用したら
        {
            PickRandomAbilities();//能力データをランダムに選ぶ
            BuyAbility = false;//能力自販機使用状況リセット
        }
    }

    #region 強化飲料選出関数
    //強化データを確率と制約に基づいて3つ選ぶ
    public void PickRandomUpgrades()
    {
        //最低限必要なデータがあるかチェック
        if (Data.upgradeData.Length < 3) return;

        //前回のデータを消す
        currentDisplayedUpgrades.Clear();
        List<int> selectedNumbers = new List<int>();

        //3つ決まるまでひたすら繰り返す
        while (selectedNumbers.Count < 3)
        {
            //確率から候補を一つ選ぶ
            int candidate = GetUpgradeNumberByProbability();

            //既に選ばれていればやり直し
            if (selectedNumbers.Contains(candidate)) continue;

            //特定のペアになったらやり直し
            if (IsExclusiveUpgradePair(candidate, selectedNumbers)) continue;

            //両方OKだったら選択肢に入れる
            selectedNumbers.Add(candidate);
        }

        // 番号からスクリタブルオブジェクトを紐づけする
        foreach (int num in selectedNumbers)
        {
            //飲料管理から飲料番号が一致する物を探す
            UpgradeData d = Data.upgradeData.FirstOrDefault(x => x.DrinkNumber == num);
            if (d != null) currentDisplayedUpgrades.Add(d);
        }

        // UI反映
        UpdateUpgradeUI();

        UI.SetActive(true);
        Cursor.visible = true;
    }
    //強化を確率で決定する関数
    private int GetUpgradeNumberByProbability()
    {
        //低確率用乱数作成
        int roll1 = UnityEngine.Random.Range(1, 101);

        //20以下なら低確率
        if (roll1 <= 20)
        {
            // 低確率: 4番
            return 4;
        }
        //20以下以外
        else
        {
            //中・高確率用乱数作成
            int roll2 = UnityEngine.Random.Range(1, 101);

            //40以下なら中確率
            if (roll2 <= 40)
            {
                // 中確率: 3, 5, 7, 9
                int[] mid = { 3, 5, 7, 9 };
                return mid[UnityEngine.Random.Range(0, mid.Length)];
            }
            //40以下以外なら高確率
            else
            {
                // 高確率: 0, 1, 2, 6, 8, 10
                int[] high = { 0, 1, 2, 6, 8, 10 };
                return high[UnityEngine.Random.Range(0, high.Length)];
            }
        }
    }
    //強化ペア禁止判定関数
    private bool IsExclusiveUpgradePair(int candidate, List<int> currentList)
    {
        //禁止リストをチェック
        for (int i = 0; i < exclusivePairs.GetLength(0); i++)
        {
            int p1 = exclusivePairs[i, 0]; // ペアの片方
            int p2 = exclusivePairs[i, 1]; // ペアのもう片方

            //禁止ペアがそろっていたらやり直し
            if (candidate == p1 && currentList.Contains(p2)) return true;
            if (candidate == p2 && currentList.Contains(p1)) return true;
        }
        return false;//どの禁止ペアにも該当しなければOK
    }
    //強化飲料でUI更新
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

    #region 能力飲料選出関数
    //能力データを確率に基づいて3つ選ぶ
    public void PickRandomAbilities()
    {
        //最低限必要なデータがあるかチェック
        if (Data.abilityData.Length < 3) return;

        //前回のデータを消す
        currentDisplayedAbilities.Clear();
        List<int> selectedNumbers = new List<int>();

        //3つ決まるまでひたすら繰り返す
        while (selectedNumbers.Count < 3)
        {
            //確率から候補を一つ選ぶ
            int candidate = GetAbilityNumberByProbability();

            //既に選ばれていればやり直し
            if (selectedNumbers.Contains(candidate)) continue;

            //OKだったら選択肢に入れる
            selectedNumbers.Add(candidate);
        }

        // 番号からスクリタブルオブジェクトを紐づけする
        foreach (int num in selectedNumbers)
        {
            //飲料管理から飲料番号が一致する物を探す
            AbilityData d = Data.abilityData.FirstOrDefault(x => x.DrinkNumber == num);
            if (d != null) currentDisplayedAbilities.Add(d);
        }

        // UI反映
        UpdateAbilityUI();

        UI.SetActive(true);
        Cursor.visible = true;
    }
    //能力を確率で決定する関数
    private int GetAbilityNumberByProbability()
    {
        //低確率用乱数作成
        int roll = UnityEngine.Random.Range(1, 101);

        //20以下なら低確率
        if (roll <= 20)
        {
            // 低確率: 6番
            return 6;
        }
        //20以下以外なら高確率
        else
        {

            // 高確率: 0, 1, 2, 3, 4, 5
            int[] high = { 0, 1, 2, 3, 4, 5 };
            return high[UnityEngine.Random.Range(0, high.Length)];

        }
    }
    //能力飲料でUI更新
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

    #region イベントトリガー・ボタン関数
    //選択肢1にカーソルが乗った時の関数
    public void Choosing1PointerEnter()
    {
        //選択中表示
        Choosing1.SetActive(true);
    }
    //選択肢1のカーソルが降りた時の関数
    public void Choosing1PointerExit()
    {
        //選択中非表示
        Choosing1.SetActive(false);
    }
    //選択肢1ボタン関数
    public void Choices1Button()
    {
        Debug.Log("選択 : "+ Name1.text);
        //UIを閉じる
        CloseUI();
    }

    //選択肢2にカーソルが乗った時の関数
    public void Choosing2PointerEnter()
    {
        //選択中表示
        Choosing2.SetActive(true);
    }
    //選択肢2のカーソルが降りた時の関数
    public void Choosing2PointerExit()
    {
        //選択中非表示
        Choosing2.SetActive(false);
    }
    //選択肢2ボタン関数
    public void Choices2Button()
    {
        Debug.Log("選択 : "+ Name2.text);
        //UIを閉じる
        CloseUI();
    }

    //選択肢3にカーソルが乗った時の関数
    public void Choosing3PointerEnter()
    {
        //選択中表示
        Choosing3.SetActive(true);
    }
    //選択肢3のカーソルが降りた時の関数
    public void Choosing3PointerExit()
    {
        //選択中非表示
        Choosing3.SetActive(false);
    }
    //選択肢3ボタン関数
    public void Choices3Button()
    {
        Debug.Log("選択 : "+ Name3.text);
        //UIを閉じる
        CloseUI();
    }
    #endregion

    //UIを閉じる関数
    private void CloseUI()
    {
        //ジュース購入音再生
        audioSource.PlayOneShot(audioClip);

        //選択中非表示
        Choosing1.SetActive(false);
        Choosing2.SetActive(false);
        Choosing3.SetActive(false);

        //カーソル非表示
        Cursor.visible = false;

        //UI非表示
        UI.SetActive(false);
    }
}