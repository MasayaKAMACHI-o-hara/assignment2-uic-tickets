using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public int timeLimit = 60; // 制限時間（カウントダウン用）
    public float currentTime;     // 現在の時間

    public bool TimerOn = false;

    public TextMeshProUGUI timerText; // 画面に表示するためのUI

    public ClearManegar ClearUI;

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

    public void AddTime(int time)
    {
        currentTime += time;
    }

    void Update()
    {
        // 赤くなる
        if (currentTime <= 10f)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;

        if (TimerOn)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                TimerOn = false;
                ClearUI.StartCoroutine(ClearUI.GameEnd());
            }

            // UIへの表示（ToStringの"F2"は小数点以下2桁まで出すという意味）
            if (timerText != null)
            {
                int displayTime = Mathf.CeilToInt(currentTime);
                timerText.text = "" + displayTime.ToString("F0");
            }

        }
    }

    // タイマーを残るようにするやつ
    public void ShowTimer(bool isShow)
    {
        gameObject.SetActive(isShow);

    }
}
