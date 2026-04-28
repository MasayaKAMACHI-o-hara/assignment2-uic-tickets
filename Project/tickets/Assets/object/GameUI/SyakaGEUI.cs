using TMPro;
using UnityEngine;

public class SyakaGEUI : MonoBehaviour
{
    public SyakaSyaka player;   // プレイヤー参照
    public TextMeshProUGUI SyakaGELevelText;

    void Update()
    {
        SyakaGELevelText.text = "" + player.GetSyakaLv;
    }
}
