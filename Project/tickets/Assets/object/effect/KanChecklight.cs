using UnityEngine;

public class HideIfFar : MonoBehaviour
{
    public Transform target;   // 判定の対象（缶）
    public float visibleDistance = 50f; // 表示される距離

    Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // 距離の判定
        float distance = Vector3.Distance(transform.position, target.position);

        // 一定の距離に達しているかの判定
        bool shouldShow = distance <= visibleDistance;

        // 非表示
        foreach (var r in renderers)
        {
            r.enabled = shouldShow;
        }
    }
}