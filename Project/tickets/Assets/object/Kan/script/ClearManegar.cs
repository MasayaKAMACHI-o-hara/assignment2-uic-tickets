using UnityEngine;
using TMPro;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using Unity.Mathematics;
using UnityEngine.SceneManagement;


public class ClearManegar : MonoBehaviour
{
    public GameObject ClearUI;

    public GameObject KanUI;
    public GameObject KanOverUI;
    public GameObject GameUI;
    public GameObject OverUI;
    public GameObject[] ScoreUI;
    public TextMeshProUGUI[] ScoreText;

    public GameObject TitleBuckButton;
    public GameObject OverBuckButton;

    public int test;
    public int Goalscore;
    public int Coinscore;
    public int Timescore;
    public int Totalscore;

    public int[] Mathscore;
    public int[] Showscore = { 0, 0, 0 };

    public int Coin = 0;
    public int time = 180;
    public KanMove Kan;
    public CameraMove Camera;
    public PauseSistem Pause;
    public CountDown Timer;
    public SyakaSyaka syaka;

    public IEnumerator GameFinish(int score)
    {
        Timer.TimerStop();
        GameUI.SetActive(false);
        Kan.ActiveMove = false;
        Camera.ActiveMove = false;
        syaka.ActiveSyaka = false;
        Pause.IsActiveESC = false;
        Camera.IsActiveClear = true;

        ClearUI.transform.position -= new Vector3(0, 2000, 0);
        KanUI.SetActive(true);
        Mathscore[0] = score;
        Mathscore[1] = Kan.coin * 100;
        Mathscore[2] = (int)(Timer.currentTime * 100);


        for (int i = 0; i < 750; i++)
        {
            yield return null;
        }


        for (int i = 0; i < 3; i++)
        {
            ScoreUI[i].SetActive(true);

            yield return new WaitForSeconds(0.5f);

            for (int j = Mathscore[i]; j > 0; j--)
            {
                Showscore[i]++;
                ScoreText[i].text = Showscore[i].ToString("D6") + "pt";
                yield return null;

                if (j > 100)
                {
                    Showscore[i] += 100;
                    ScoreText[i].text = Showscore[i].ToString("D6") + "pt";
                    j -= 100;
                    yield return null;
                }

                if (j > 10000)
                {
                    Showscore[i] += 1000;
                    ScoreText[i].text = Showscore[i].ToString("D6") + "pt";
                    j -= 1000;
                    yield return null;
                }
            }

            for (int j = 0; j < 200; j++)
            {
                yield return null;
            }

        }

        var Mix = Mathscore[0] + Mathscore[1] + Mathscore[2];
        ScoreUI[3].SetActive(true);
        ScoreText[3].text = Mix.ToString("D6") + "pt";

        // �n�C�X�R�A�ۑ�
        HighScore.SaveHighScore(Mix);

        for (int j = 0; j < 200; j++)
        {
            yield return null;
        }

        TitleBuckButton.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void BuckTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public IEnumerator GameEnd()
    {
        Timer.TimerStop();
        GameUI.SetActive(false);
        Kan.ActiveMove = false;
        Camera.ActiveMove = false;
        syaka.ActiveSyaka = false;
        Pause.IsActiveESC = false;

        OverUI.transform.position -= new Vector3(0, 2000, 0);
        KanOverUI.SetActive(true);

        for(int i = 0;i < 200;i++)
        {
            yield return null;
        }

        OverBuckButton.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(GameFinish(test));
        }
    }
}
