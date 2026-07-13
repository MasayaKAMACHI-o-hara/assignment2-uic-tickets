using UnityEngine;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    [Header("�^�C�g�����S")]
    public GameObject TiteleLogo;

    [Header("�Q�[��UI")]
    public GameObject GameUI;

    public static bool GameNow = false;

    public KanMove Kan;
    public CameraMove Camera;
    public PauseSistem Pause;
    public CountDown Timer;
    public SyakaSyaka Syaka;

    //public GameObject gamesetumei;

    public GameObject StartBuckBottown_1;
    public GameObject StartBuckBottown_2;
    public GameObject left_Bottown;
    public GameObject right_Bottown;


    public void Start()
    {
        //        Cursor.visible = true;
        //        Cursor.lockState = CursorLockMode.None;
    }


    public void Onclick()
    {
        SoundManager.StopBGM_Title();
        SoundManager.PlayBGM_Game();

        Kan.ActiveMove = true;
        Camera.ActiveMove = true;
        Pause.IsActiveESC = true;

        GameNow = true;
        TiteleLogo.SetActive(false);
        GameUI.SetActive(true);
        Syaka.ActiveSyaka = true;
        Timer.ShowTimer(true);

        Timer.TimerStart();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameObject.SetActive(false);
    }
    public RectTransform gamesetumei;

    private int page = 0; // 現在のページ番号
    private const int PAGE_WIDTH = 5000; // ページ1枚分の移動量

    public void Onclick_description()
    {
        page = 0; // 最初のページへ
        ShowPage();
    }

    public void OnClick_Right()
    {
        page++;
        ShowPage();
    }

    public void OnClick_Left()
    {
        page--;
        if (page < 0) page = 0; // マイナスページに行かないように
        ShowPage();
    }

    private void ShowPage()
    {
        // ページ番号 × ページ幅 で位置を決める
        gamesetumei.localPosition = new Vector3(page * -PAGE_WIDTH, 0, 0);
    }

    public void OnClick_titleBuck()
    {
        // ページ番号をリセット
        page = 0;

        // ゲーム説明UIを元の座標に戻す
        gamesetumei.localPosition = new Vector3(2000, 2000, 0);

        // タイトルUIを表示
        StartBuckBottown_1.SetActive(true);
        StartBuckBottown_2.SetActive(true);

        // ページめくりボタンは非表示
        //left_Bottown.SetActive(false);
        //right_Bottown.SetActive(false);
        
    }
}