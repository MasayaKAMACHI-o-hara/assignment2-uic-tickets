using UnityEngine;
using System.Collections;

public class HideIfFar : MonoBehaviour
{
    public Transform target;            // 監視の対象（主役）
    public float visibleDistance = 50f; // 表示させておく距離

    Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // 距離の判定
        float distance = Vector3.Distance(transform.position, target.position);

        // 規定の距離に達しているかの判定
        bool shouldShow = distance <= visibleDistance;

        // 表示・非表示の切り替え
        foreach (var r in renderers)
        {
            r.enabled = shouldShow;
        }
    }

    public void ExtendVisibleDistance(float extraDistance, float duration)
    {
        StartCoroutine(ExtendDistanceCoroutine(extraDistance, duration));
    }

    private IEnumerator ExtendDistanceCoroutine(float extraDistance, float duration)
    {
        float originalDistance = visibleDistance;
        visibleDistance += extraDistance;
        yield return new WaitForSeconds(duration);
        visibleDistance = originalDistance;
    }

    // ExtendVisibleDistance(9999f, 60f); 範囲を広げたい場合はこれを呼び出してください。1分後に効果は消滅します。
}