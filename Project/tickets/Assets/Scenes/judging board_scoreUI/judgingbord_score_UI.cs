using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class judgingbord_score_UI : MonoBehaviour
{
    public float rotatespeed = 80f; //回転する速度
    public Transform targetTran; //判定板の位置
    public TextMeshProUGUI scoreText; //UIのテキスト

    public void SetScore(int score) //判定板のスコアを受け取る
    {
        scoreText.text = score.ToString(); //UIに数字(スコア)を表示
    }

    void Update()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(
            Camera.main,
            targetTran.position + Vector3.up); //判定板の上にUIを追従

        transform.Rotate(0, rotatespeed * Time.deltaTime, 0); //UIを回転させる
    }
}