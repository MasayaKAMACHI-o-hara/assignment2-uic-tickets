using UnityEngine;
using TMPro;

public class TrashCountTitleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;

    private const string CountSaveKey = "FoundTrashCount";

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        int foundCount = PlayerPrefs.GetInt(CountSaveKey, 0);

        if (countText != null)
        {
            countText.text = foundCount + " / " + 13;
        }
    }
}