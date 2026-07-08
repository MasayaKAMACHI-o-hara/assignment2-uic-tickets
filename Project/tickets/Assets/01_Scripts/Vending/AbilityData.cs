using UnityEngine;
[CreateAssetMenu(menuName = "Drink Data/Ability")]
public class AbilityData : ScriptableObject
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