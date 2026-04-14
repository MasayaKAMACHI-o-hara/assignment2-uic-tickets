using UnityEngine;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    [Header("É^ÉCÉgÉãÉçÉS")]
    public GameObject TiteleLogo;
    public GameObject TiteleLogo2;

    public static bool GameNow = false;

    private void Start()
    {
        GameNow = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Onclick()
    {
        GameNow = true;

        TiteleLogo.SetActive(false);
        TiteleLogo2.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameObject.SetActive(false);
    }
}