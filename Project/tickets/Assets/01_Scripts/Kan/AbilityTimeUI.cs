using UnityEngine;
using UnityEngine.UI;

public class AbilityTimeUI : MonoBehaviour
{
    [Header("ゲージ")]
    [SerializeField] private Image gaugeFill;

    [Header("中央アイコン")]
    [SerializeField] private Image iconImage;

    [Header("アビリティ時間")]
    [SerializeField] private float maxTime = 30f;

    private float currentTime;
    private bool isCounting;

    private void Awake()
    {
        currentTime = maxTime;
        isCounting = false;

        if (gaugeFill != null)
        {
            gaugeFill.fillAmount = 1f;
            gaugeFill.enabled = false;
        }

        if (iconImage != null)
        {
            iconImage.enabled = false;
        }
    }

    private void Update()
    {
        if (!isCounting)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isCounting = false;

            Destroy(gameObject);
            return;
        }

        if (gaugeFill != null)
        {
            gaugeFill.fillAmount = currentTime / maxTime;
        }
    }

    // アビリティタイマー開始
    public void StartAbility(Sprite abilityIcon)
    {
        currentTime = maxTime;
        isCounting = true;

        if (gaugeFill != null)
        {
            gaugeFill.fillAmount = 1f;
            gaugeFill.enabled = true;
        }

        if (iconImage != null)
        {
            iconImage.sprite = abilityIcon;
            iconImage.enabled = true;
        }
    }
}