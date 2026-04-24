using UnityEngine;
using UnityEngine.UI;

public class AbilityTimeUI : MonoBehaviour
{
    [Header("ゲージ")]
    public Image gaugeFill;

    [Header("中央アイコン")]
    public Image iconImage;

    [Header("アビリティ時間")]
    public float maxTime = 30f;

    private float currentTime;
    private bool isCounting = false;

    void Start()
    {
        currentTime = maxTime;

        if (gaugeFill != null)
        {
            gaugeFill.fillAmount = 1f;
            gaugeFill.enabled = false;
        }

        if (iconImage != null)
            iconImage.enabled = false;
    }

    void Update()
    {
        if (isCounting)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isCounting = false;

                if (gaugeFill != null)
                    gaugeFill.enabled = false;

                if (iconImage != null)
                    iconImage.enabled = false;
            }

            if (gaugeFill != null)
                gaugeFill.fillAmount = currentTime / maxTime;
        }
    }

    // アビリティ30秒開始
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