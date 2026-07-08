using UnityEngine;

public class judgingboard_script : MonoBehaviour
{
    public string judgingboard = "can"; // 当たり判定板タグ
    public int scoreValue = 10;

    private bool hasScored = false;

    void OnCollisionEnter(Collision collision)
    {
        if (hasScored) return; // 既にスコアが表示されていたらログを出さない
        
        if (collision.gameObject.CompareTag("can"))
        {
            Debug.Log("Goal!スコア: " + scoreValue); // judging boardテスト
            hasScored = true;
        }
    }
}
