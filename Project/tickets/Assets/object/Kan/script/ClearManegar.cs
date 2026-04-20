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
    public GameObject[] ScoreUI;
    public TextMeshProUGUI[] ScoreText;

    public GameObject TitleBuckButton;

    public int test;
    public int Goalscore;
    public int Coinscore;
    public int Timescore;
    public int Totalscore;

    public int[] Mathscore;
    public int[] Showscore = {0,0,0};

    public int Time = 180;
    public int Coin = 0;
    public KanMove Kan;
    public CameraMove Camera;

    IEnumerator GameFinish(int score)
    {
      Kan.ActiveMove = false;
      Camera.ActiveMove = false;

        ClearUI.transform.position -= new Vector3(0, 2000, 0);
        KanUI.SetActive(true);
        Mathscore[0] = score;
        Mathscore[1] = Time;
        Mathscore[2] = Coin;


        for(int i= 0;i<500;i++)
        {
            yield return null;
        }
       

        for(int i= 0; i<3;i++)
        {
             ScoreUI[i].SetActive(true);

            yield return new WaitForSeconds(0.5f); ;

            for(int j= Mathscore[i];j>0;j--)
            {
                Showscore[i]++;
                ScoreText[i].text = Showscore[i] + "pt";
                yield return null;
            }

            for(int j=0;j<100;j++)
            {
                yield return null;
            }
            
        }

        var Mix = Mathscore[0] + Mathscore[1] + Mathscore[2];
        ScoreUI[3].SetActive(true);
        ScoreText[3].text = Mix + "pt";

        for (int j = 0; j < 100; j++)
        {
            yield return null;
        }

        TitleBuckButton.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void BuckTitle()
    {
        SceneManager.LoadScene(0);
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
