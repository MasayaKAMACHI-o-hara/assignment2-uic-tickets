using UnityEngine;
using UnityEngine.UI;

public class Settings_1 : MonoBehaviour
{
    public RectTransform settings;

    public void OpenSettings()
    {
        settings.anchoredPosition = new Vector2(4000, 0);
    }
}
