using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class judgingbord_score_UI : MonoBehaviour
{
    public Transform worldCanvas;

    public float rotatespeed = 80f; // 回転速度
    public Transform targetTran;    // 追従対象
    public TextMeshProUGUI scoreText;
    public CanvasGroup canvasGroup;
    public LayerMask obstacleMask;

    public Vector3 scoreOffset = new Vector3(0, 0.8f, 0);
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if (targetTran == null) return;

        // 位置を頭上に固定
        //transform.position = targetTran.position + new Vector3(0, 0.8f, 0);
        transform.position = targetTran.position + scoreOffset;
        // カメラ方向を向く（反転しない）
        Vector3 lookDir = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(lookDir);

        // 回転
        transform.Rotate(0, rotatespeed * Time.deltaTime, 0);

        // 障害物で透明化
        Vector3 dir = (Camera.main.transform.position - targetTran.position).normalized;
        float dist = Vector3.Distance(Camera.main.transform.position, targetTran.position);

        //if (Physics.Raycast(Camera.main.transform.position, -dir, out RaycastHit hit, dist, obstacleMask))
        //    canvasGroup.alpha = 0;
        //else
        //    canvasGroup.alpha = 1;
    }
}