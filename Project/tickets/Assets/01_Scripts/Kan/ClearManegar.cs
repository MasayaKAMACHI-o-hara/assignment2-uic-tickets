using UnityEngine;
using TMPro;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public bool ActiveOver = false;
    
    [Header("効果音")]
    public AudioSource SE;

    public AudioClip SE1;
    public AudioClip SE2; // katakata
    public AudioClip SE3; // Score
    public AudioClip SE4; // TotalScore

    [Header("スコアボーナス")]
    public int ScoreBonusCount = 0;
    public int ScoreBonusPoint = 10000; // !!! OSIRUKO !!!

    //public GameObject BonusUI;
    //public TextMeshProUGUI BonusText;
    //public Image BonusIcon;

    void PlaySE(AudioClip clip)
    {
        if (SE != null && clip != null)
        {
            SE.PlayOneShot(clip);
        }
    }


    public IEnumerator GameFinish(int score)
    {
        Kan.StopKanSound();
        PlaySE(SE1);

        Timer.TimerStop();
        GameUI.SetActive(false);
        Kan.ActiveMove = false;
        Camera.ActiveMove = false;
        syaka.ActiveSyaka = false;
        Pause.IsActiveESC = false;
        Camera.IsActiveClear = true;
        Timer.ShowTimer(false);

        RectTransform clearRect = ClearUI.GetComponent<RectTransform>();
        clearRect.anchoredPosition3D = Vector3.zero;

        KanUI.SetActive(true);

        Mathscore[0] = score;
        Mathscore[1] = Kan.coin * 100;
        Mathscore[2] = (int)(Timer.currentTime * 100);

        int bonusScore = ScoreBonusCount * ScoreBonusPoint;

        for (int i = 0; i < 750; i++)
        {
            yield return null;
        }

        for (int i = 0; i < 3; i++)
        {
            ScoreUI[i].SetActive(true);

            yield return new WaitForSeconds(0.5f);

            if (SE != null && SE2 != null)
            {
                SE.clip = SE2;
                SE.loop = true;
                SE.Play();
            }

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

            ScoreText[i].text = Mathscore[i].ToString("D6") + "pt";

            if (SE != null)
            {
                SE.Stop();
                SE.loop = false;
                SE.clip = null;
            }

            PlaySE(SE3);

            for (int j = 0; j < 200; j++)
            {
                yield return null;
            }
        }

        //if (ScoreBonusCount > 0)
        //{
        //    BonusUI.SetActive(true);

        //    BonusText.text =
        //        "+" + bonusScore.ToString("D6") + "pt";

        //    yield return new WaitForSeconds(1f);
        //}

        var Mix = Mathscore[0] + Mathscore[1] + Mathscore[2] + bonusScore;
        ScoreUI[3].SetActive(true);
        ScoreText[3].text = Mix.ToString("D6") + "pt";

        PlaySE(SE4);

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
        Timer.ShowTimer(false);
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public IEnumerator GameEnd()
    {
        Kan.StopKanSound();
        PlaySE(SE1);

        Debug.Log("a");
        Timer.TimerStop();
        GameUI.SetActive(false);
        Kan.ActiveMove = false;
        Camera.ActiveMove = false;
        syaka.ActiveSyaka = false;
        Pause.IsActiveESC = false;

        RectTransform clearRect = ClearUI.GetComponent<RectTransform>();
        clearRect.anchoredPosition3D = Vector3.zero;

        KanOverUI.SetActive(true);

        for (int i = 0; i < 200; i++)
        {
            Kan.StopKanSound();
            PlaySE(SE1);
        
            Timer.TimerStop();
            GameUI.SetActive(false);
            Kan.ActiveMove = false;
            Camera.ActiveMove = false;
            syaka.ActiveSyaka = false;
            Pause.IsActiveESC = false;
            ActiveOver = true;

            OverUI.transform.position -= new Vector3(0, 2000, 0);
            KanOverUI.SetActive(true);

            for (int j = 0; j < 200; j++)
            {
                yield return null;
            }

            OverBuckButton.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Timer.ShowTimer(true);
        }
        
       
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
