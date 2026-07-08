using UnityEngine;
using UnityEngine.UI;

public class script_test2 : MonoBehaviour
{
    public Transform targetTran;
    public Text scoreText;   // Å© í«â¡ÅIUI ÇÃ Text ÇéQè∆Ç∑ÇÈ

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    void Update()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(
            Camera.main,
            targetTran.position + Vector3.up);
    }
}
