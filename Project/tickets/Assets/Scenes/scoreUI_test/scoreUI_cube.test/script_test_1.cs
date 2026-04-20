using UnityEngine;
using UnityEngine.InputSystem;

public class script_test_1 : MonoBehaviour
{
    public Transform targetTran;

    void Update()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(
        Camera.main,
        targetTran.position + Vector3.up);
    }
}


