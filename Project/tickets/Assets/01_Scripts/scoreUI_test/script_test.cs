using UnityEngine;
using UnityEngine.UI;

public class script_test : MonoBehaviour
{
    public Transform targetTran;

    void Update()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(
        Camera.main,
        targetTran.position + Vector3.up);
    }
}