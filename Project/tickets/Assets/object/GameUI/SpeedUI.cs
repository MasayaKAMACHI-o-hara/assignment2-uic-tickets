using TMPro;
using UnityEngine;

public class SpeedUI : MonoBehaviour
{
    public KanMove player;   // プレイヤー参照
    public TextMeshProUGUI SpeedLevelText;

    void Update()
    {
        SpeedLevelText.text = "" + player.SpeedLv;
    }
}
