using UnityEngine;

public class PauseSistem : MonoBehaviour
{

    private bool IsActiveESC = false;
    private bool IsActivePause = false;

    public GameObject PauseUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void canPause()
    {
        IsActiveESC = true;
    }

    public void cantPause()
    {
        IsActiveESC = false;
    }


    public void PressESC()
    {
        if(IsActivePause)//もし現在ポーズ中なら
        {
            PauseUI.transform.position += new Vector3(2000, 0, 0);//ポーズ画面表示
            Time.timeScale = 1f;
            IsActivePause = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            PauseUI.transform.position -= new Vector3(2000,0,0);//非表示
            Time.timeScale = 0f;
            IsActivePause = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && IsActiveESC)
        {
            PressESC();
        }


    }
}
