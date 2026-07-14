using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class judgingbord_score : MonoBehaviour
{
    // このオブジェクト（ゴミ箱など）にヒットしたときに獲得できるスコア
    public int scoreValue = 10;

    [Header("このゴミ箱の固有ID（全て異なるものにしてください）")]
    public string trashID;

    [Header("ゴミのカウント状況（全スクリプトで共有する静的変数）")]
    public static int foundTrashCount = 0; // 見つかったゴミの総数
    public static int maxTrashCount = 12;  // ゴミの最大数（クリア条件などに使用）

    [SerializeField] RectTransform canvasRect;            // UIを表示するキャンバスのRectTransform
    [SerializeField] judgingbord_score_UI scoreUIPrefab; // 頭上などに表示するスコアUIのプレハブ
    [SerializeField] ClearManegar clearManegar;          // ゲームクリアを管理するマネージャー

    public Vector3 scoreOffset = new Vector3(0, 0.8f, 0);

    private judgingbord_score_UI scoreUI; // 生成したスコアUIのインスタンス保持用
    private bool hasScored = false;       // すでにスコア（カウント）加算済みかどうかを判定するフラグ

    void Start()
    {
        // Debug.Log("ゴミ箱側:" + scoreOffset); // 座標ログ

        // 1. セーブデータ（PlayerPrefs）から、このゴミ箱がすでに発見済みか確認
        if (PlayerPrefs.GetInt(trashID, 0) == 1)
        {
            hasScored = true; // 発見済みならフラグを立てる
            
        }

        // 2. セーブデータから現在のゴミの総発見数を読み込む
        foundTrashCount = PlayerPrefs.GetInt("FoundTrashCount", 0);

        // 3. スコア表示用UIを生成し、初期設定を行う
        scoreUI = Instantiate(scoreUIPrefab, canvasRect);
        scoreUI.targetTran = transform;               // UIの追従対象に自分自身を設定

        scoreUI.scoreOffset = scoreOffset; // ←追加

        scoreUI.SetScore(scoreValue);                 // 表示するスコアの値を設定
        //scoreUI.transform.localPosition = new Vector3(0, 100, 0); // 位置の初期化（少し上にずらす）
        scoreUI.transform.localScale = Vector3.one;   // サイズを1倍に設定

        if (hasScored)//もしすでに見つけたごみ箱なら
        {
            TextMeshProUGUI a = scoreUI.gameObject.GetComponent<TextMeshProUGUI>();
            
            a.color = new Color(1f, 0.15f, 0.34f);//設定した色の文字にする
        }
    }

    // 別のオブジェクトが衝突（接触）したときに呼ばれる関数
    void OnCollisionEnter(Collision collision)
    {   
        // 衝突したオブジェクトのタグが "Kan"（缶）でなければ、何もし処理をせず返す
        if (!collision.gameObject.CompareTag("Kan")) return;

        // 缶が当たったら、成否に関わらず毎回クリアマネージャーのゲーム終了（スコア加算）コルーチンを呼び出す
        clearManegar.StartCoroutine(clearManegar.GameFinish(scoreValue));

        // すでにこのゴミ箱でカウント加算処理が終わっている（過去のプレイ含む）なら、これ以降の処理をしない
        if (hasScored) return;

        // 加算フラグを true にして、1回のプレイ中に何度もカウントされないようにする
        hasScored = true;

        // セーブデータ上で既に発見済（値が1）なら、多重加算を防ぐためにここで処理を返す
        if (PlayerPrefs.GetInt(trashID, 0) == 1) return;
        
        // セーブデータに「このゴミ箱を発見した（値1）」として保存
        PlayerPrefs.SetInt(trashID, 1);

        // セーブデータから現在の総発見数を読み込み、1増やして再保存する
        int count = PlayerPrefs.GetInt("FoundTrashCount", 0);
        count++;
        PlayerPrefs.SetInt("FoundTrashCount", count);

        // 変更されたセーブデータをディスクに強制保存（書き込み）する
        PlayerPrefs.Save();
    }
}