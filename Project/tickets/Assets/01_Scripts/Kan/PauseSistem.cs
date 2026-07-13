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

    public AudioSource carSound;

    public Vector2 ShowPosition = Vector2.zero;
    public Vector2 HidePosition = new Vector2(5000f, 0f);

    private RectTransform pauseRect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseRect = PauseUI.GetComponent<RectTransform>();
    }


    public void PressESC()
    {
        if (IsActivePause)//もし現在ポーズ中なら
        {
            pauseRect.anchoredPosition = HidePosition;
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
            pauseRect.anchoredPosition = ShowPosition;
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
