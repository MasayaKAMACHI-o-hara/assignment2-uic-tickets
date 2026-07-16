using UnityEngine;

public class AbilityTimeUIManager : MonoBehaviour
{
    [Header("アビリティタイマーUIのプレハブ")]
    [SerializeField] private AbilityTimeUI abilityTimeUIPrefab;

    [Header("タイマーUIを並べる親")]
    [SerializeField] private Transform timerContainer;

    /// <summary>
    /// 新しいアビリティタイマーUIを生成
    /// </summary>
    public void AddAbility(Sprite abilityIcon)
    {
        // 新しいタイマーUIを生成
        AbilityTimeUI newAbilityUI =
            Instantiate(abilityTimeUIPrefab, timerContainer);

        // このUIだけのタイマーを開始
        newAbilityUI.StartAbility(abilityIcon);
    }
}