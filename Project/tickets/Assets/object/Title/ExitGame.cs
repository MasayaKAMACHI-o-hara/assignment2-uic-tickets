using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // エディタ用
    #else
        Application.Quit(); // ビルド後用
    #endif
    }
}