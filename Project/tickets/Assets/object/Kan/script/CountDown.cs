using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public int timeLimit = 60; // 制限時間（カウントダウン用）
    public float currentTime;     // 現在の時間

    public bool TimerOn = false;

    public TextMeshProUGUI timerText; // 画面に表示するためのUI

    void Start()
    {

        currentTime = timeLimit;
    }

    public void TimerStart()
    {
        TimerOn = true;
    }

    public void TimerStop()
    {
        TimerOn = false;
    }

    void Update()
    {
        if (TimerOn)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                // ここに「タイムアップ！」の処理を書けるよ
            }

            // UIへの表示（ToStringの"F2"は小数点以下2桁まで出すという意味）
            if (timerText != null)
            {
                timerText.text = "" + currentTime.ToString("F0"); ;
            }

        }

    }
}
