using TMPro;
using UnityEngine;

public class JumpUI : MonoBehaviour
{
    public KanMove player;   // プレイヤー参照
    public TextMeshProUGUI JumpLevelText;

    void Update()
    {
        JumpLevelText.text = "" + player.JumpLv;
    }
}
