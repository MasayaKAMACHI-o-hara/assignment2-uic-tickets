using UnityEngine;

public class judgingbord_score : MonoBehaviour
{
    public int scoreValue = 10; // 変更NG＿元々の表記(スコア・ログ)

    [SerializeField]
    RectTransform canvasRect; //Canvasを指定する変数

    [SerializeField]
    judgingbord_score_UI scoreUIPrefab; //UIprefab

    private judgingbord_score_UI scoreUI; //UIの変数

    private bool hasScored = false;

    void Start()
    {
        scoreUI = Instantiate(scoreUIPrefab, canvasRect);//ゲーム開始時にUIを表示

        scoreUI.targetTran = transform; //UIが追従する対象(判定板)

        scoreUI.SetScore(scoreValue); //UIに渡すスコア値

        scoreUI.transform.localPosition = Vector3.zero;
        scoreUI.transform.localScale = Vector3.one;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasScored) return;

        if (collision.gameObject.CompareTag("Kan")) //canが入ったとき
        {
            Debug.Log("Goal! スコア: " + scoreValue); //スコアログが出る

            hasScored = true;
        }
    }
}