using UnityEngine;

public class judgingboard_script2 : MonoBehaviour
{
    public int scoreValue = 10; // ゴミ箱ごとのスコア

    [SerializeField]
    RectTransform canvasRect;

    [SerializeField]
    script_test2 scoreUIPrefab; // ← UIプレハブ

    private bool hasScored = false;
    private script_test2 scoreUI;

    void OnCollisionEnter(Collision collision)
    {
        if (hasScored) return;

        if (collision.gameObject.CompareTag("can"))
        {
            Debug.Log("Goal! スコア: " + scoreValue);

            // UIをゴミ箱の上に生成
            scoreUI = Instantiate(scoreUIPrefab, canvasRect);
            scoreUI.targetTran = transform; // ← ゴミ箱の位置を追従！

            scoreUI.SetScore(scoreValue);

            hasScored = true;
        }
    }
}
