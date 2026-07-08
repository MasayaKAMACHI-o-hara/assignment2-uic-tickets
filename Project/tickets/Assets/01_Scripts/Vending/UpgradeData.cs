using UnityEngine;
[CreateAssetMenu(menuName = "Drink Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    [Header("ˆù—¿”Ô†")]
    [SerializeField] public int DrinkNumber;
    [Header("ˆù—¿–¼")]
    [TextArea] public string Name;
    [Header("‰æ‘œ")]
    public Sprite Image;
    [Header("à–¾")]
    [TextArea] public string Explanation;
}