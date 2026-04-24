using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
    public Canvas worldCanvas;        // ← UI 用 Canvas（PauseUI ではない）
    public GameObject scoreUIPrefab;  // ← Score UI の Prefab

    public void CreateScoreUI(Transform target, int score)
    {
        var ui = Instantiate(scoreUIPrefab, worldCanvas.transform, true);

        var script = ui.GetComponent<judgingbord_score_UI>();
        script.targetTran = target;
        script.SetScore(score);
    }
}