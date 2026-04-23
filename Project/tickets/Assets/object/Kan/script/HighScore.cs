using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    // ハイスコア取得
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    // ハイスコア保存
    public static void SaveHighScore(int score)
    {
        int highScore = GetHighScore();

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    // ハイスコア表示
    void Start()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "" + GetHighScore().ToString("D6");
        }
    }

    // リセットしたい時用
    public static void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
    }
}