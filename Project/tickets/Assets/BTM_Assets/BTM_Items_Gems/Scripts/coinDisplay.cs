using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public KanMove player;   // プレイヤー参照
    public TextMeshProUGUI coinText;

    void Update()
    {
        coinText.text = "" + player.coin;
    }
}