using UnityEngine;

public class judgingbord_score : MonoBehaviour
{
    public int scoreValue = 10;

    [Header("このゴミ箱のID（全部異なるものにしてｎ）")]
    public string trashID;

    [Header("全体カウント")]
    public static int foundTrashCount = 0;
    public static int maxTrashCount = 12;

    [SerializeField] RectTransform canvasRect;
    [SerializeField] judgingbord_score_UI scoreUIPrefab;
    [SerializeField] ClearManegar clearManegar;

    private judgingbord_score_UI scoreUI;
    private bool hasScored = false;

    void Start()
    {
        if (PlayerPrefs.GetInt(trashID, 0) == 1)
        {
            hasScored = true;
        }

        foundTrashCount = PlayerPrefs.GetInt("FoundTrashCount", 0);

        scoreUI = Instantiate(scoreUIPrefab, canvasRect);
        scoreUI.targetTran = transform;
        scoreUI.SetScore(scoreValue);
        scoreUI.transform.localPosition = new Vector3(0, 100, 0);
        scoreUI.transform.localScale = Vector3.one;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Kan")) return;

        if (clearManegar.IsGameOver == true) return;

        clearManegar.StartCoroutine(clearManegar.GameFinish(scoreValue));

        if (hasScored) return;

        hasScored = true;

        if (PlayerPrefs.GetInt(trashID, 0) == 1) return;
        PlayerPrefs.SetInt(trashID, 1);

        int count = PlayerPrefs.GetInt("FoundTrashCount", 0);
        count++;
        PlayerPrefs.SetInt("FoundTrashCount", count);

        PlayerPrefs.Save();
    }
}