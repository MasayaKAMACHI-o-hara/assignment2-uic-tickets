using UnityEngine;

public class judgingbord_score : MonoBehaviour
{
    public int scoreValue = 10;

    [SerializeField] RectTransform canvasRect;
    [SerializeField] judgingbord_score_UI scoreUIPrefab;

    [SerializeField] ClearManegar clearManegar;

    private judgingbord_score_UI scoreUI;
    private bool hasScored = false;

    void Start()
    {
        scoreUI = Instantiate(scoreUIPrefab, canvasRect);
        scoreUI.targetTran = transform;
        scoreUI.SetScore(scoreValue);
        scoreUI.transform.localPosition = new Vector3(0, 100, 0);
        scoreUI.transform.localScale = Vector3.one;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasScored) return;

        if (collision.gameObject.CompareTag("Kan"))
        {
            hasScored = true;

            clearManegar.StartCoroutine(clearManegar.GameFinish(scoreValue));
        }
    }
}