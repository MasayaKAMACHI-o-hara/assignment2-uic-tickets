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

    public void Onclick()
    {

        Kan.ActiveMove = true;
        Camera.ActiveMove = true;
        Pause.canPause();

        GameNow = true;
        TiteleLogo.SetActive(false);
        GameUI.SetActive(true);

        Timer.TimerStart();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameObject.SetActive(false);
    }
}

