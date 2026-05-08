using UnityEngine;

public class PauseSistem : MonoBehaviour
{

    public bool IsActiveESC = false;
    public bool IsActivePause = false;

    public GameObject PauseUI;
    public CountDown Timer;
    public CameraMove Camera;
    public KanMove Kan;
    public SyakaSyaka Syaka;

    public AudioSource SEPause;

    public AudioClip PauseOn;
    public AudioClip PauseOff;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    public void PressESC()
    {
        if (IsActivePause)//もし現在ポーズ中なら
        {
            PauseUI.transform.position += new Vector3(2000, 0, 0);//非表示
            Time.timeScale = 1f;
            IsActivePause = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Timer.TimerStart();
            Kan.ActiveMove = true;
            Camera.ActiveMove = true;
            Syaka.ActiveSyaka = true;
            Kan.SE.UnPause();
            Syaka.SE.UnPause();
            SEPause.PlayOneShot(PauseOff);
        }
        else
        {
            PauseUI.transform.position -= new Vector3(2000, 0, 0);//ポーズ画面表示
            Time.timeScale = 0f;
            IsActivePause = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Timer.TimerStop();
            Kan.ActiveMove = false;
            Camera.ActiveMove = false;
            Syaka.ActiveSyaka = false;
            Kan.SE.Pause();
            Syaka.SE.Pause();
            SEPause.PlayOneShot(PauseOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsActiveESC)
        {
            PressESC();
        }
    }
}
