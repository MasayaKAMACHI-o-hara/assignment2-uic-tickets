using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    // �n�C�X�R�A�擾
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    // �n�C�X�R�A�ۑ�
    public static void SaveHighScore(int score)
    {
        int highScore = GetHighScore();

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    // �n�C�X�R�A�\��
    void Start()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "" + GetHighScore().ToString("D6");
        }
    }

    // ���Z�b�g���������p
    public static void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
    }
}