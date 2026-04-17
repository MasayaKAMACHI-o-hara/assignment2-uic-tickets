using UnityEngine;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    [Header("タイトルロゴ")]
    public GameObject TiteleLogo;

    [Header("ゲームUI")]
    public GameObject GameUI;

    public static bool GameNow = false;

    public void Onclick()
    {
        GameNow = true;
        TiteleLogo.SetActive(false);
        GameUI.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameObject.SetActive(false);
    }
}

